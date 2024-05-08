using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;
using Quizzify_BLL;
using Quizzify_BLL.DTO;
using static StackExchange.Redis.Role;

namespace Quizzify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateQuizController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly CreateQuizService _createQuizService;
        public CreateQuizController(CreateQuizService createQuizService)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, CategoryModel>();
                cfg.CreateMap<CategoryModel, CategoryDTO>();
                cfg.CreateMap<QuestionModel, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, QuestionModel>();
                cfg.CreateMap<QuizDetailsModel, QuizDetailsDTO>();
                cfg.CreateMap<QuizDetailsDTO,QuizDetailsModel>();
                cfg.CreateMap<SelectedQuestionModel,SelectedQuestionDTO>();
                cfg.CreateMap<SelectedQuestionDTO,SelectedQuestionModel>();
            });
            mapper = mapConfig.CreateMapper();
            _createQuizService = createQuizService;
        }
        [HttpGet("GetCategory")]
        public List<CategoryModel> GetCategories()
        {
            List<CategoryDTO> categoryDTOs = _createQuizService.GetCategories();
            List<CategoryModel> categoryModels = mapper.Map<List<CategoryModel>>(categoryDTOs);
            return categoryModels;
        }
        [HttpGet("CategoryQuestions")]
        public List<QuestionModel> GetCategoryQuestions(int categoryId, string organisationName)
        {
            List<QuestionDTO> questionDTOs = _createQuizService.GetCategoryQuestions(categoryId, organisationName);
            List<QuestionModel> questionsModels = mapper.Map<List<QuestionModel>>(questionDTOs);
            return questionsModels;
        }
        [HttpPost("CreateQuiz")]
        public IActionResult CreateQuiz([FromBody] QuizDetailsModel quizDetails)
        {
            QuizDetailsDTO quizDetailsDTO = mapper.Map<QuizDetailsDTO>(quizDetails);
            bool result = _createQuizService.CreateQuiz(quizDetailsDTO);
            if(result)
            {
                return Ok("Quiz Created successfully");
            }
            else
            {
                return BadRequest("Something went wrong try again ");
            }
        }
    }
}
