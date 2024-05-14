using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;

namespace Quizzify_API.Controllers
{
    [Authorize]
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
                cfg.CreateMap<QuestionModel, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, QuestionModel>();
                cfg.CreateMap<QuizQuestionsDisplayModel, QuizQuestionsDisplayDTO>();
                cfg.CreateMap<QuizQuestionsDisplayDTO, QuizQuestionsDisplayModel>();
                cfg.CreateMap<QuizDetailsModel, QuizDetailsDTO>();
                cfg.CreateMap<QuizDetailsDTO,QuizDetailsModel>();
                cfg.CreateMap<SelectedQuestionModel,SelectedQuestionDTO>();
                cfg.CreateMap<SelectedQuestionDTO,SelectedQuestionModel>();
            });
            mapper = mapConfig.CreateMapper();
            _createQuizService = createQuizService;
        }

        [HttpGet("CategoryQuestions")]
        public List<QuizQuestionsDisplayModel> GetCategoryQuestions(int categoryId, string organisationName)
        {
            List<QuizQuestionsDisplayDTO> questionDTOs = _createQuizService.GetCategoryQuestions(categoryId, organisationName);
            List<QuizQuestionsDisplayModel> questionsModels = mapper.Map<List<QuizQuestionsDisplayModel>>(questionDTOs);
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
        [HttpGet("CategoryQuestionsByCreator/{creatorName}")]
        public List<QuestionModel> GetCategoryQuestionsByCreator(int categoryId, string organisationName, string creatorName)
        {
            List<QuestionDTO> questionDTOs = _createQuizService.GetCategoryQuestionsByCreator(categoryId, organisationName, creatorName);
            List<QuestionModel> questionsModels = mapper.Map<List<QuestionModel>>(questionDTOs);
            return questionsModels;
        }
    }
}
