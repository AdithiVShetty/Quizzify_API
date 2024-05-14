using Microsoft.EntityFrameworkCore;
using Quizzify_BLL.DTO;

namespace Quizzify_BLL
{
    public class UserUpdateDAL
    {
        private QuizzifyDbContext context;
        public UserUpdateDAL(QuizzifyDbContext context)
        {
            this.context = context;
        }
        public List<User> GetAllUsers()
        {
            return context.Users
                .Include(u => u.Role)
                .Include(u => u.Organisation)
                .ToList();
        }
        public List<Role> GetAllRoles()
        {
            return context.Roles.ToList();
        }
        public bool UpdateUserApprovalStatus(int userId, bool isApproved)
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                user.IsApproved = isApproved;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        //public void UpdateUserApprovalStatus(int userId)
        //{
        //    var user = context.Users.FirstOrDefault(u => u.Id == userId);
        //    if (user != null)
        //    {
        //        user.IsApproved = !user.IsActive;
        //        context.SaveChanges();
        //    }
        //}
        public bool UpdateUser(int userId, string newEmail, string newRole)
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                // Update the email if a new email is provided
                if (!string.IsNullOrEmpty(newEmail))
                {
                    user.EmailId = newEmail;
                }

                // Update the role if a new role is provided
                if (!string.IsNullOrEmpty(newRole))
                {
                    var role = context.Roles.FirstOrDefault(r => r.Name == newRole);
                    if (role != null)
                    {
                        user.Role = role;
                    }
                    else
                    {
                        throw new ArgumentException("Role not found.", nameof(newRole));
                    }
                }

                context.SaveChanges();
                return true;
            }

            return false;
        }
        //public bool UpdateUser(int userId, UserProfile user)
        //{
        //    var existinguser = context.Users.Find(userId);
        //    if (existinguser != null)
        //    {
        //        // Update the email if provided in the JSON body
        //        if (!string.IsNullOrEmpty(user.EmailId))
        //        {
        //            user.EmailId = user.EmailId;
        //        }

        //        // Update the role if provided in the JSON body
        //        if (!string.IsNullOrEmpty(user.Role))
        //        {
        //            var role = context.Roles.FirstOrDefault(r => r.Name == user.Role);
        //            if (role != null)
        //            {
        //                existinguser.Role = role;
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Role not found.", nameof(user.Role));
        //            }
        //        }

        //        context.SaveChanges();
        //        return true;
        //    }

        //    return false;
        //}


        public bool AddFeedback(Feedback feedback)
        {
            context.Feedbacks.Add(feedback);
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
        

    }
}
