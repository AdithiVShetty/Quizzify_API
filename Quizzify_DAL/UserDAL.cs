using System.Security.Cryptography;
using System.Text;
using Quizzify_BLL.DTO;

namespace Quizzify_BLL
{
    public class UserDAL
    {
        private QuizzifyDbContext db;
        public UserDAL(QuizzifyDbContext context)
        {
            this.db = context;
        }
        public bool RegisterNewUser(User user)
        {
            db.Users.Add(user);
            int result = db.SaveChanges();
            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Organisation GetOrganisationByName(string organisationName)
        {
            Organisation organisation = db.Organisations.FirstOrDefault(o => o.Name == organisationName);
            return organisation;
        }
        public List<Organisation> GetOrganisation()
        {
            return db.Organisations.ToList();
        }
        public int AddOrganisationName(string OrganisationName)
        {
            Organisation organisation = new Organisation
            {
                Name = OrganisationName
            };
            db.Organisations.Add(organisation);
            db.SaveChanges();
            return organisation.Id;
        }
        public User Login(string email, string password)
        {
            string hashedPassword = HashPassword(password);
            string normalPassword = password;

            return db.Users.FirstOrDefault(u => u.EmailId == email && (u.Password == hashedPassword || u.Password == normalPassword));
        }
        public bool UpdatePassword(string email, string newPassword)
        {
            var user = db.Users.FirstOrDefault(u => u.EmailId == email);
            if (user != null)
            {
                user.Password = newPassword;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DoesUserExist(string email)
        {
            var user = db.Users.FirstOrDefault(u => u.EmailId == email);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> GetAdminEmailsByOrganisation(int id)
        {
            Role role = db.Roles.FirstOrDefault(r => r.Name == "Admin");
            var adminEmails = (from user in db.Users
                               where user.OrganisationId == id && user.RoleId == role.Id
                               select user.EmailId).ToList();

            return adminEmails;
        }
        public UserProfile GetUserProfile(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            var organization = db.Organisations.FirstOrDefault(o => o.Id == user.OrganisationId);
            var role = db.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            UserProfile userProfile = new UserProfile();
            userProfile.Id = id;
            userProfile.Name = user.Name;
            userProfile.EmailId = user.EmailId;
            userProfile.PhoneNumber = user.PhoneNumber;
            userProfile.OrganisationName = organization.Name;
            userProfile.Role= role.Name;
            userProfile.IsActive = user.IsActive;
            return userProfile;
        }
        public bool UpdateUserDetails(int id, UserProfile user)
        {
            var existingUser = db.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.PhoneNumber = user.PhoneNumber;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public User VerifyPassword(int userId, string password)
        {
            string hashedPassword = HashPassword(password);
            string normalPassword = password;

            return db.Users.FirstOrDefault(u => u.Id == userId && (u.Password == hashedPassword || u.Password == normalPassword));
        }
        public bool ChangePassword(int userId, ChangePassword passwordChange)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Password = HashPassword(passwordChange.NewPassword);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ToggleAccountStatus(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                db.SaveChanges();
            }
        }
        public User GetUserById(int userId)
        {
            return db.Users.FirstOrDefault(c => c.Id == userId);
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
