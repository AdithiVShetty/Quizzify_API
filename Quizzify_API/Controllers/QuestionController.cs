using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Models;
using Quizzify_API;
using Quizzify_API.Models;

namespace Quizzify_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService questionService;
        private readonly IMapper mapper;

        public QuestionController(QuestionService questionService)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionDTO, QuestionModel>();
                cfg.CreateMap<QuestionModel, QuestionDTO>();
            });
            mapper = mapConfig.CreateMapper();
            this.questionService = questionService;
           
        }

        [HttpGet("GetQuestions/{userId}")]
        public IActionResult GetQuestionsByUserId(int userId)
        {
            List<QuestionDisplayDTO> questionsWithAnswers = questionService.GetQuestionsByUserId(userId);
            return Ok(questionsWithAnswers.OrderByDescending(q => q.CreatedDate)); // Order by created date in descending order
        }

        [HttpPost("CreateQuestion")]
        public IActionResult CreateQuestion([FromBody] QuestionAddModel request)
        {
            byte[] imageData = null;

            if (!string.IsNullOrEmpty(request.ImageData))
            {
                // Convert from Base64 only if imageData is provided
                imageData = Convert.FromBase64String(request.ImageData);
            }

            var question = questionService.CreateQuestion(request.UserId, request.CategoryName, request.QuestionTypeId, request.QuestionText, request.Answer, request.CorrectAnswer, imageData, request.ImageName);

            return Ok(question);
        }


        //[HttpPost("CreateQuestion")]
        //public IActionResult CreateQuestions([FromBody] CreateQuestionModel request)
        //{
        //    var imageDatas = new List<byte[]>();
        //    foreach (var questionData in request.Questions)
        //    {
        //        if (!string.IsNullOrEmpty(questionData.ImageData)) 
        //        {
        //            imageDatas.Add(Convert.FromBase64String(questionData.ImageData));
        //        }
        //        else
        //        {
        //            imageDatas.Add(null);
        //        }
        //    }
        //    var questions = new List<Question>();
        //    var answers = new List<string>();
        //    var correctAnswers = new List<string>();
        //    var imageNames = new List<string>();

        //    foreach (var questionData in request.Questions)
        //    {
        //        questions.Add(new Question
        //        {
        //            //UserId = questionData.UserId,
        //            //QuestionTypeId = questionData.QuestionTypeId,
        //            QuestionText = questionData.QuestionText,
        //            CreatedDate = DateTime.Now,
        //            IsEnable = true
        //        });

        //        answers.Add(questionData.Answer);
        //        correctAnswers.Add(questionData.CorrectAnswer);

        //        imageNames.Add(questionData.ImageName);
        //    }

        //    var createdQuestions = questionService.CreateQuestions(
        //        request.UserId,
        //        request.CategoryName,
        //        request.QuestionTypeId,
        //        questions.Select(q => q.QuestionText).ToList(),
        //        answers,
        //        correctAnswers,
        //        imageDatas,
        //        imageNames
        //    );


        //    return Ok(createdQuestions);
        //}
        //[HttpPost("CreateQuestions")]
        //public IActionResult CreateQuestions([FromBody] QuestionAddModel request)
        //{
        //    try
        //    {
        //        var imageDatas = new List<byte[]>();
        //        var imageNames = new List<string>();

        //        if (request.ImageDatas != null && request.ImageDatas.Any())
        //        {
        //            for (int i = 0; i < request.ImageDatas.Count; i++)
        //            {
        //                byte[] imageData = null;
        //                if (!string.IsNullOrEmpty(request.ImageDatas[i]))
        //                {
        //                    imageData = Convert.FromBase64String(request.ImageDatas[i]);

        //                }
        //                imageDatas.Add(imageData);
        //                imageNames.Add(request.ImageNames[i]);
        //            }
        //        }
        //        var questions = questionService.CreateQuestions(request.UserId, request.CategoryName, request.QuestionTypeId, request.QuestionTexts, request.Answers, request.CorrectAnswers, imageDatas, imageNames);

        //        return Ok(questions);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your application's error handling strategy
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error creating questions.");
        //    }
        //}


        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            var categories = questionService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("GetQuestiontypes")]
        public IActionResult GetQuestionTypes()
        {
            var questionTypes = questionService.GetQuestionTypes();
            return Ok(questionTypes);
        }

        [HttpPut("toggleQuestion/{questionId}")]
        public IActionResult ToggleQuestionEnableStatus(int questionId)
        {
            questionService.ToggleQuestion(questionId);
            var updatedQuestion = questionService.GetQuestionByQuestionId(questionId);
            string status = updatedQuestion.IsEnable ? "Enable" : "Disable";

            return Ok(status);
        }

        [HttpGet("GetQuestionDetails/{questionId}")]
        public IActionResult GetQuestionDetails(int questionId)
        {
            var questionDetails = questionService.GetQuestionByQuestionId(questionId);
            if (questionDetails == null)
            {
                return NotFound("Question not found");
            }
            return Ok(questionDetails);
        }

        [HttpGet("GetQuestionsByCategory/{userId}/{categoryName}")]
        public IActionResult GetListOfQuestionsByCategory(int userId, string categoryName)
        {
            List<QuestionDisplayDTO> questionsWithAnswers = questionService.GetListOfQuestionsByCategory(userId, categoryName);
            return Ok(questionsWithAnswers);
        }

    }
}