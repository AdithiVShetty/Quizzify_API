using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Quizzify_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Quizzify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserService userService;
        private readonly IConfiguration _configuration;

        public UserController(UserService _userService, IConfiguration configuration)
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
            userService = _userService;
            _configuration = configuration;
        }
        private string GenerateJwtToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.EmailId),
                // Add more claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpiryInDays"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("login")]
        public IActionResult PostLogin([FromBody] UserModel login)
        {
            try
            {
                UserDTO loggedInUser = userService.Login(login.EmailId, login.Password);

                if (loggedInUser == null)
                {
                    return Unauthorized();
                }
                var token = GenerateJwtToken(loggedInUser);

                return Ok(new { token, userType = GetRoleName(loggedInUser.RoleId), userId = loggedInUser.Id, userName = loggedInUser.Name, isApproved = loggedInUser.IsApproved, organisationId = loggedInUser.OrganisationId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterForm")]
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
                    PhoneNumber = userProfile.PhoneNumber,
                    
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
                bool result = userService.RegisterNewUser(userDTO);
                if (result)
                {
                    List<string> adminEmails = userService.GetAdminEmailsByOrganisation(userProfile.OrganisationName);

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
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("Gaurav.Tripathi@triconinfotech.com", "Secure@15$%"),
                EnableSsl = true
            };

            string body =
                "Dear Admin,<br><br>" +
              "A new user has registered with the following details:<br>" +
              $"Name: {Name}<br>" +
              $"Email: {EmailId}<br>" +
              $"Organisation: {OrganisationName}<br><br>" +
              "Please take necessary action.<br><br>" +
              "Regards,<br>" +
              "Quizzify Team";

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("Gaurav.Tripathi@triconinfotech.com"),
                Subject = "New User Registration",
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.Body = $"<div style='color: #000;'>{mailMessage.Body}</div>";
            mailMessage.To.Add(adminEmail);

            smtpClient.Send(mailMessage);
        }

        [Authorize]
        [HttpGet("userprofile/{userId}")]
        public IActionResult GetUserProfile(int userId)
        {
            // Get the user ID from the JWT token
            //var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            //{
            //    return Unauthorized(); // Invalid or missing user ID in the token
            //}

            UserProfileDTO userProfileDTO = userService.GetUserProfile(userId);
            UserProfileModel userProfileModel = mapper.Map<UserProfileModel>(userProfileDTO);
            return Ok(userProfileModel);
        }

        [Authorize]
        [HttpPut("userprofile/{userId}")]
        public IActionResult UpdateUserProfile(int userId, UserProfileModel userProfileModel)
        {

            UserProfileDTO userProfileDTO = new UserProfileDTO();
            userProfileDTO.Name = userProfileModel.Name;
            userProfileDTO.PhoneNumber = userProfileModel.PhoneNumber;
            userService.UpdateUserProfile(userId, userProfileDTO);

            return Ok("Profile updated successfully.");
        }

        [Authorize]
        [HttpPut("toggleAccount/{userId}")]
        public IActionResult ToggleAccountStatus(int userId)
        {
            userService.ToggleAccount(userId);
            var account = userService.GetUserProfile(userId);
            string status = account.IsActive ? "Activate" : "Deactivate";

            return Ok(status);
        }

        [Authorize]
        [HttpPost("VerifyPassword/{userId}")]
        public IActionResult VerifyPassword(int userId, ChangePasswordModel verifyPassword)
        {
            try
            {
                ChangePasswordDTO user = userService.VerifyPassword(userId, verifyPassword.Password);

                return Ok("Password is verified");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("ChangePassword/{userId}")]
        public IActionResult ChangePassword(int userId, ChangePasswordModel changePasswordModel)
        {
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
            changePasswordDTO.NewPassword = changePasswordModel.NewPassword;
            if (changePasswordModel.NewPassword != changePasswordModel.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }
            userService.ChangePassword(userId, changePasswordDTO);           
            return Ok("Password updated successfully.");
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("token"); // Clear JWT token from cookies

                return Ok(new { message = "Logout successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "admin";
                case 2:
                    return "creator";
                case 3:
                    return "user";
                default:
                    return "user";
            }
        }

    }
}