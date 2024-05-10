using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Quizzify_BLL.DTO;
using Quizzify_DAL;

namespace Quizzify_BLL
{
    public class UserUpdateService
    {

        private readonly IMapper mapper;
      
        private readonly QuizzifyDbContext db;
        public UserUpdateService(QuizzifyDbContext _db, IMemoryCache cache)
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



        public bool UpdateUser(int userId, string newEmail, string newRole)
        {
            UserUpdateDAL userupdateDAL = new UserUpdateDAL(db);
            return userupdateDAL.UpdateUser(userId, newEmail, newRole);
        }
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
