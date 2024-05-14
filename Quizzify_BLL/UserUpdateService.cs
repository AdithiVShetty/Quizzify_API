using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Quizzify_API.Models;
using Quizzify_BLL;
using Quizzify_BLL.DTO;
using Quizzify_DAL;

namespace Quizzify_API
{
    public class UserUpdateService
    {

        private readonly IMapper mapper;
        private readonly UserUpdateDAL userupdateDAL;

        private readonly QuizzifyDbContext db;
        public UserUpdateService(QuizzifyDbContext _db, IMemoryCache cache, UserUpdateDAL userupdateDAL)
        {
            var mapConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<UpdateApproval, UpdateApprovalDTO>();
                cfg.CreateMap<UpdateApprovalDTO, UpdateApproval>();
                cfg.CreateMap<Role, RoleDTO>();
                cfg.CreateMap<RoleDTO, Role>();
                cfg.CreateMap<Feedback, FeedbackDTO>();
                cfg.CreateMap<FeedbackDTO, Feedback>();
               
            });
            mapper = mapConfig.CreateMapper();
            db = _db;
            this.userupdateDAL = userupdateDAL;

        }
        public List<RoleDTO> GetRoles()
        {
            UserUpdateDAL userupdateDAL = new UserUpdateDAL(db);
            List<Role> roles = userupdateDAL.GetAllRoles();
            List<RoleDTO> roleDTOs = mapper.Map<List<RoleDTO>>(roles);
            return roleDTOs;
        }
        public bool UpdateUserApprovalStatus(int userId, bool isApproved)
        {
            UserUpdateDAL userupdateDAL = new UserUpdateDAL(db);
            return userupdateDAL.UpdateUserApprovalStatus(userId, isApproved);
        }
        //public void UpdateUserApprovalStatus(int userId)
        //{
        //    userupdateDAL.UpdateUserApprovalStatus(userId);
        //}



        public bool updateuser(int userid, string newemail, string newrole)
        {
            UserUpdateDAL userupdatedal = new UserUpdateDAL(db);
            return userupdatedal.UpdateUser(userid, newemail, newrole);
        }
        //public bool UpdateUser(int userId, UserProfile userDetails)
        //{
        //    UserUpdateDAL userUpdateDAL = new UserUpdateDAL(db);
        //    return userUpdateDAL.UpdateUser(userId, userDetails);
        //}


        public List<UserDetailsDTO> GetAllUsers()
        {
            UserUpdateDAL userupdateDAL = new UserUpdateDAL(db);
            var users = userupdateDAL.GetAllUsers();
            // var users = .GetAllUsers();
            // users = users.Where(u => u.Id != currentUserId).ToList();
            return users.Select(u => new UserDetailsDTO
            {
                Id = u.Id,
                Name = u.Name,
                Role = u.Role.Name,
                Organisation = u.Organisation.Name,
                IsApproved = u.IsApproved,
                IsActive = u.IsActive
            }).ToList();
        }
        public bool AddFeedback(FeedbackDTO feedbackDTO)
        {
            UserUpdateDAL userupdateDAL = new UserUpdateDAL(db);
            Feedback feedback = mapper.Map<Feedback>(feedbackDTO);
            bool result = userupdateDAL.AddFeedback(feedback);
            return result;
        }
       

    }
}
