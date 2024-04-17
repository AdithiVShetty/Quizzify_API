using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;
using Quizzify_BLL;
using System.Net.Mail;
using System.Net;

namespace Quizzify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        public UserController()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserModel>();
                cfg.CreateMap<UserModel, UserDTO>();
                cfg.CreateMap<OrganisationDTO, OrganisationModel>();
                cfg.CreateMap<OrganisationModel, OrganisationDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfileModel>();
                cfg.CreateMap<UserProfileModel, UserProfileDTO>();
            });
            mapper = mapConfig.CreateMapper();
        }
        UserService userService = new UserService();

        

        [HttpPost("login")]
        public IActionResult PostLogin([FromBody] UserModel login)
        {
            try
            {
                UserDTO loggedInUser = userService.Login(login.EmailId, login.Password);

                if (loggedInUser.RoleId == 1)
                {
                    return Ok(new { userType = "admin", userId = loggedInUser.Id, userName = loggedInUser.Name, isApproved = loggedInUser.IsApproved });
                }
                else if (loggedInUser.RoleId == 2)
                {
                    return Ok(new { userType = "creator", userId = loggedInUser.Id, userName = loggedInUser.Name, isApproved = loggedInUser.IsApproved });
                }
                else
                {
                    return Ok(new { userType = "user", userId = loggedInUser.Id, userName = loggedInUser.Name, isApproved = loggedInUser.IsApproved });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("RegisterForm")]
       // [EnableCors("AllowReactApp")]
        public IActionResult RegisterNewUser(String Name, String EmailId, String Password, String OrganisationName
          , string PhoneNo)
        {
            try
            {
                bool IsUserAlreadyExist = userService.DoesUserExist(EmailId);
                if (IsUserAlreadyExist)
                {
                    return Ok("EmailId already exist....!! Try Loging In");
                }
                var userDTO = new UserDTO
                {
                    Name = Name,
                    EmailId = EmailId,
                    Password = Password,
                    PhoneNumber = PhoneNo
                };
                OrganisationDTO Organisation = userService.GetOrganisationByName(OrganisationName);
                if (Organisation == null)
                {
                    int id = userService.AddOrganisationName(OrganisationName);
                    userDTO.OrganisationId = id;
                }
                else
                {
                    userDTO.OrganisationId = Organisation.Id;
                }

                //userDTO.IsActive = true;
                //userDTO.RoleId = 3;
                bool result = userService.RegisterNewUser(userDTO);
                if (result)
                {
                    List<string> adminEmails = userService.GetAdminEmailsByOrganisation(OrganisationName);

                    // Send email to each admin
                    foreach (var adminEmail in adminEmails)
                    {
                        SendNewUserAlertToAdmin(adminEmail, Name, EmailId, OrganisationName);
                    }
                    return Ok("Data Inserted");
                }
                else
                {
                    return BadRequest("Something went wrong try again ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetOrganisation")]
        public List<OrganisationModel> GetOrganisation()
        {
            List<OrganisationDTO> organisationDTO = userService.GetOrganisations();
            List<OrganisationModel> organisationModels = mapper.Map<List<OrganisationModel>>(organisationDTO);
            return organisationModels;

        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                //string EmailID = email;

                // Generate OTP (You can use any logic to generate OTP)
                Random random = new Random();
                string otp = random.Next(100000, 999999).ToString();

                // Send OTP to email
                SendEmail(email, otp);

                // Store the OTP in session
                HttpContext.Session.SetString("OTP", otp); // Assuming you're using ASP.NET Core and have session configured


                return Ok(new { message = "OTP sent successfully", otp });
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to send OTP: " + ex.Message);
            }
        }

        private void SendEmail(string email, string otp)
        {
            // Configure SMTP client (Update with your SMTP server details)
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("xyz@gmail.com", "Xyz@123"),
                EnableSsl = true
            };

            // Create email message
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("xyz@gmail.com"),
                Subject = "OTP for Password Reset",
                Body = $"Your OTP is: {otp}"
            };

            mailMessage.To.Add(email);

            // Send email
            smtpClient.Send(mailMessage);
        }

        private void SendNewUserAlertToAdmin(string adminEmail, string Name, string EmailId, string OrganisationName)
        {
            // Configure SMTP client (Update with your SMTP server details)
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("xyz@gmail.com", "Xyz@123"),
                EnableSsl = true
            };
            //string body = "Dear Admin,\n\n" +
            //                "A new user has registered with the following details:\n" +
            //                $"Name: {Name}\n" +
            //                $"Email: {EmailId}\n" +
            //                $"Organisation: {OrganisationName}\n\n" +
            //                "Please take necessary action.\n\n" +
            //                "Regards,\n" +
            //                "Quizzify Team";
            string body =
                "Dear Admin,<br><br>" +
              "A new user has registered with the following details:<br>" +
              $"Name: {Name}<br>" +
              $"Email: {EmailId}<br>" +
              $"Organisation: {OrganisationName}<br><br>" +
              "Please take necessary action.<br><br>" +
              "Regards,<br>" +
              "Quizzify Team";
            // Create email message
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("xyz@gmail.com"),
                Subject = "New User Registration",
                Body =  body,
                IsBodyHtml = true
            };
            mailMessage.Body = $"<div style='color: #000;'>{mailMessage.Body}</div>";
            mailMessage.To.Add(adminEmail);

            // Send email
            smtpClient.Send(mailMessage);
        }

        [HttpPost("validateOTP")]
        public IActionResult VerifyOTP(string otp)
        {
            string storedOTP = HttpContext.Session.GetString("OTP");

            if (otp == storedOTP)
            {
                return Ok("Valid OTP");
            }
            else
            {
                return BadRequest("Invalid OTP");
            }
        }

        [HttpPost("resetpassword")]
        public IActionResult ResetPassword(string email, string newPassword)
        {
            try
            {
                //string email = data.email;
                //string newPassword = data.newPassword;

                if (userService.UpdatePassword(email, newPassword))
                {
                    return Ok("Password updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to reset password");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to reset password: " + ex.Message);
            }
        }
        [HttpGet("userprofile")]
        public IActionResult GetUserProfile(int userId)
        {
            UserProfileDTO userProfileDTO = userService.GetUserProfile(userId);
            UserProfileModel userProfileModel = mapper.Map<UserProfileModel>(userProfileDTO);
            return Ok(userProfileModel);

        }
    }
}
