using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;
using Quizzify_BLL.DTO;

namespace Quizzify_API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UserUpdateController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserUpdateService userUpdateService;
        public UserUpdateController(UserUpdateService _userUpdateService)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateApprovalDTO, UpdateApprovalModel>();
                cfg.CreateMap<RoleModel, UpdateApprovalDTO>();
                cfg.CreateMap<RoleDTO, RoleModel>();
                cfg.CreateMap<RoleModel, RoleDTO>();
                cfg.CreateMap<FeedbackDTO,FeedbackModel>();
                cfg.CreateMap<FeedbackModel, FeedbackDTO>();


            });
            mapper = mapConfig.CreateMapper();
            userUpdateService = _userUpdateService;
        }
        [HttpGet("GetRoles")]
        public List<RoleModel> GetRole()
        {
            List<RoleDTO> roleDTO = userUpdateService.GetRoles();
            List<RoleModel> roleModels = mapper.Map<List<RoleModel>>(roleDTO);
            return roleModels;
        }
        [HttpGet("userdetails")]
        public IActionResult GetAllUsers()
        {
            var users = userUpdateService.GetAllUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpPut("UpdateUserApprovalStatus/{userId}")]
        public IActionResult UpdateUserApprovalStatus(int userId, [FromBody] bool isApproved)
        {
           bool success = userUpdateService.UpdateUserApprovalStatus(userId, isApproved);
           if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound(); // User not found
            }
        }
        
        [HttpPut("UpdateUser/{userId}")]
        public IActionResult UpdateUser(int userId, string newEmail,string newRole)
        {
            bool success = userUpdateService.updateuser(userId, newEmail, newRole);
            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpPut("UpdateUser")]
        //public IActionResult UpdateUser(int userId, UserProfile userDetails)
        //{
        //    bool success = userUpdateService.UpdateUser(userId, userDetails);
        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost("feedback")]
        public IActionResult AddFeedback([FromBody] FeedbackDTO feedback)
        {
            try
            {
                bool result = userUpdateService.AddFeedback(feedback);
                if (result)
                {
                    return Ok("Feedback added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add feedback.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
       

    }
}
