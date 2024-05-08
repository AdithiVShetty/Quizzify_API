using AutoMapper;
using Quizzify_DAL.ModelClass;

namespace Quizzify_DAL.DAL
{
    public class UserDAL
    {
        private readonly IMapper mapper;

        private QuizzifyDbContext context;
        public UserDAL(QuizzifyDbContext context)
        {
            this.context = context;
        }
        public bool RegisterNewUser(User user)
        {
            context.Users.Add(user);
            int result = context.SaveChanges();
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
            Organisation organisation = context.Organisations.FirstOrDefault(o => o.Name == organisationName);
            return organisation;
        }
        public List<Organisation> GetOrganisation()
        {
            return context.Organisations.ToList();
        }
        public int AddOrganisationName(string OrganisationName)
        {
            Organisation organisation = new Organisation
            {
                Name = OrganisationName
            };
            context.Organisations.Add(organisation);
            context.SaveChanges();
            return organisation.Id;
        }
        public bool UpdatePassword(string email, string newPassword)
        {
            var user = context.Users.FirstOrDefault(u => u.EmailId == email);
            if (user != null)
            {
                user.Password = newPassword;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DoesUserExist(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.EmailId == email);
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
            Role role = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var adminEmails = (from user in context.Users
                               where user.OrganisationId == id && user.RoleId == role.Id
                               select user.EmailId).ToList();

            return adminEmails;
        }
        public UserProfile GetUserProfile(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            var organization = context.Organisations.FirstOrDefault(o => o.Id == user.OrganisationId);
            var role = context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            UserProfile userProfile = new UserProfile();
            userProfile.Id = id;
            userProfile.Name = user.Name;
            userProfile.EmailId = user.EmailId;
            userProfile.PhoneNumber = user.PhoneNumber;
            userProfile.OrganisationName = organization.Name;
            userProfile.RoleName = role.Name;
            return userProfile;
        }
    }
}
