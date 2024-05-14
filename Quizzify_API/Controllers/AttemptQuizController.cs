using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;
using Quizzify_BLL.DTO;
using Quizzify_BLL;
using System.Security.Claims;
using Quizzify_BLL;

namespace Quizzify_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptQuizController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AttemptQuizService attemptQuizService;

        public AttemptQuizController(AttemptQuizService attemptQuizService)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuizDTO, QuizModel>();
                cfg.CreateMap<QuizModel, QuizDTO>();
                cfg.CreateMap<QuestionDTO, QuestionModel>();
                cfg.CreateMap<QuestionModel, QuestionDTO>();
            });
            mapper = mapConfig.CreateMapper();
            this.attemptQuizService = attemptQuizService;
        }

        [HttpGet("GetListOfQuizzes/{organisationId}")]
        public List<QuizModel> GetListOfQuizzes(int organisationId)
        {
            List<QuizDTO> quizDTOs = attemptQuizService.GetListOfQuizzes(organisationId);
            List<QuizModel> quizModels = mapper.Map<List<QuizModel>>(quizDTOs);
            return quizModels;
        }

        [HttpGet("GetQuizDetails/{quizId}")]
        public QuizModel GetQuizByQuizId(int quizId)
        {
            QuizDTO quizDTO = attemptQuizService.GetQuizByQuizId(quizId);
            return mapper.Map<QuizModel>(quizDTO);
        }
        [HttpGet("GetQuizQuestions/{quizId}")]
        public List<QuestionModel> GetQuizQuestions(int quizId)
        {
            List<QuestionDTO> questionsDTO = attemptQuizService.GetQuizQuestions(quizId);
            List<QuestionModel> questionModels = mapper.Map<List<QuestionModel>>(questionsDTO);
            return questionModels;
        }

        [HttpGet("GetQuestionAnswer/{questionId}")]
        public AnswerModel GetQuestionAnswer(int questionId)
        {
            AnswerDTO answer = attemptQuizService.GetQuestionAnswer(questionId);
            return mapper.Map<AnswerModel>(answer);
        }

    }
}
