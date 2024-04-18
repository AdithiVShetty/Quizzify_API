using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;
using Quizzify_BLL;
using System.Net.Mail;
using System.Net;
using Quizzify_API.Requests;
using Quizzify_DAL;

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
        public IActionResult RegisterNewUser([FromBody] UserProfileModel userProfile)
        {
            try
            {
                bool IsUserAlreadyExist = userService.DoesUserExist(userProfile.EmailId);
                if (IsUserAlreadyExist)
                {
                    return Ok("EmailId already exist....!! Try Loging In");
                }
                var userDTO = new UserDTO
                {
                    Name = userProfile.Name,
                    EmailId = userProfile.EmailId,
                    Password = userProfile.Password,
                    PhoneNumber = userProfile.PhoneNumber
                };
                string organisationName = userProfile.OrganisationName;
                OrganisationDTO organisationDTO = userService.GetOrganisationByName(organisationName);
                OrganisationModel Organisation = mapper.Map<OrganisationModel>(organisationDTO);
                if (Organisation == null)
                {
                    int id = userService.AddOrganisationName(userProfile.OrganisationName);
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
                    List<string> adminEmails = userService.GetAdminEmailsByOrganisation(userProfile.OrganisationName);

                    // Send email to each admin
                    foreach (var adminEmail in adminEmails)
                    {
                        SendNewUserAlertToAdmin(adminEmail, userProfile.Name, userProfile.EmailId, userProfile.OrganisationName);
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

       

    
        [HttpGet("userprofile")]
        public IActionResult GetUserProfile(int userId)
        {
            UserProfileDTO userProfileDTO = userService.GetUserProfile(userId);
            UserProfileModel userProfileModel = mapper.Map<UserProfileModel>(userProfileDTO);
            return Ok(userProfileModel);

        }
    }
}
