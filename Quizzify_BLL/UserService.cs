using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Quizzify_API.Models;
using Quizzify_BLL;
using Quizzify_BLL.DTO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Quizzify_API
{
    public class UserService
    {
        private readonly IMapper mapper;
        private readonly IMemoryCache _cache;
        private readonly Random random = new Random();
        private readonly QuizzifyDbContext db;
        private readonly UserDAL userDAL;
        public UserService(QuizzifyDbContext _db, IMemoryCache cache, UserDAL userDAL)
        {
            var mapConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Organisation, OrganisationDTO>();
                cfg.CreateMap<OrganisationDTO, Organisation>();
                cfg.CreateMap<UserProfile, UserProfileDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            });
            mapper = mapConfig.CreateMapper();
            _cache = cache;
            this.userDAL = userDAL;
        }
        public OrganisationDTO GetOrganisationByName(string organisationName)
        {
            Organisation organisation = userDAL.GetOrganisationByName(organisationName);
            OrganisationDTO organisationDTO = mapper.Map<OrganisationDTO>(organisation);
            return organisationDTO;
        }
        public bool RegisterNewUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            user.Password = HashPassword(userDTO.Password);
            bool result = userDAL.RegisterNewUser(user);
            return result;
        }
        public List<OrganisationDTO> GetOrganisations()
        {
            List<Organisation> organisations = userDAL.GetOrganisation();
            List<OrganisationDTO> organisationDTOs = mapper.Map<List<OrganisationDTO>>(organisations);
            return organisationDTOs;
        }
        public int AddOrganisationName(string organisationName)
        {
            int id = userDAL.AddOrganisationName(organisationName);
            return id;
        }
        public bool DoesUserExist(string email)
        {
            return userDAL.DoesUserExist(email);
        }
        public List<string> GetAdminEmailsByOrganisation(string organisationName)
        {
            OrganisationDTO organisation = GetOrganisationByName(organisationName);
            Organisation organisation1 = mapper.Map<Organisation>(organisation);
            return userDAL.GetAdminEmailsByOrganisation(organisation1.Id);
        }
        public UserDTO Login(string email, string password)
        {
            User loginUser = userDAL.Login(email, password);

            if (loginUser != null)
            {
                UserDTO userDTO = new UserDTO
                {
                    Id = loginUser.Id,
                    EmailId = loginUser.EmailId,
                    Name = loginUser.Name,
                    RoleId = loginUser.RoleId,
                    OrganisationId = loginUser.OrganisationId,
                    IsApproved = loginUser.IsApproved,
                };
                return userDTO;
            }
            else
            {
                throw new InvalidOperationException("Invalid EmailId or Password.");
            }
        }
        public UserProfileDTO GetUserProfile(int userId)
        {
           
            UserProfile userProfile = userDAL.GetUserProfile(userId);
            UserProfileDTO userProfileDTO = mapper.Map<UserProfileDTO>(userProfile);
            return userProfileDTO;
        }
        public void UpdateUserProfile(int id, UserProfileDTO userProfileDTO)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.Name = userProfileDTO.Name;
            userProfile.PhoneNumber = userProfileDTO.PhoneNumber;
            userDAL.UpdateUserDetails(id, userProfile);
        }
        public void ToggleAccount(int userId)
        {
            userDAL.ToggleAccountStatus(userId);
        }
        public ChangePasswordDTO VerifyPassword(int userId, string password)
        {
            User user = userDAL.VerifyPassword(userId, password);

            if (user != null)
            {
               ChangePasswordDTO userDTO = new ChangePasswordDTO
                {
                    Id = user.Id,
                    Password = user.Password
                };
                return userDTO;
            }
            else
            {
                throw new InvalidOperationException("Incorrect Password.");
            }
        }
        public void ChangePassword(int id, ChangePasswordDTO changePasswordDTO)
        {
            ChangePassword chnagePassword = new ChangePassword();
            chnagePassword.NewPassword = changePasswordDTO.NewPassword;
            userDAL.ChangePassword(id, chnagePassword);
        }
        public void SendOTP(string email)
        {
            int otp = random.Next(100000, 999999);

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
            _cache.Set(email, otp.ToString(), cacheEntryOptions);

            SendEmail(email, $"Your OTP for password reset: {otp}\n Your OTP is valid for 10 minutes. If it expires, you can request a resend");
        }
        public bool VerifyOTP(string email, string otp)
        {
            if (_cache.TryGetValue(email, out string storedOTP))
            {
                if (storedOTP == otp)
                {
                    // OTP verified successfully, remove it from the cache
                    _cache.Remove(email);
                    return true;
                }
            }
            return false;
        }
        public void UpdatePassword(string email, string newPassword)
        {
            var user = db.Users.FirstOrDefault(u => u.EmailId == email);

            if (user != null)
            {
                user.Password = HashPassword(newPassword);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Email not found");
            }
        }
        private void SendEmail(string to, string body)
        {
            try
            {
                string smtpServer = Config.SmtpServer;
                int port = Config.Port;
                bool enableSsl = Config.EnableSsl;
                string username = Config.Username;
                string password = Config.Password;
                string senderEmail = Config.SenderEmail;

                // Configure SMTP client
                SmtpClient client = new SmtpClient(smtpServer);
                client.Port = port;
                client.EnableSsl = enableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("yourEmail", "yourPassword");

                // Create and send email
                MailMessage mailMessage = new MailMessage("sender's email", to, "Password Reset OTP", body);
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle email sending failure
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                byte[] truncatedBytes = new byte[8];
                Array.Copy(bytes, truncatedBytes, 8);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < truncatedBytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
