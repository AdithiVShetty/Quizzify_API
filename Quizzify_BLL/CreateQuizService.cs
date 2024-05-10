using AutoMapper;
using Quizzify_API.Models;
using Quizzify_BLL;
using Quizzify_BLL.DTO;

namespace Quizzify_API
{
    public class CreateQuizService
    {
        private readonly IMapper mapper;
        private readonly QuizDAL createQuizDAL;
        public CreateQuizService(QuizDAL createQuizDAL)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<QuizQuestionsDisplayDTO, QuizQuestionsDisplay>();
                cfg.CreateMap<QuizQuestionsDisplay, QuizQuestionsDisplayDTO>();
                cfg.CreateMap<QuizDetailsDTO, QuizDetails>();
                cfg.CreateMap<QuizDetails, QuizDetailsDTO>();
                cfg.CreateMap<SelectedQuestionDTO,SelectedQuestion>();
                cfg.CreateMap<SelectedQuestion, SelectedQuestionDTO>();
            });
            mapper = mapConfig.CreateMapper();
            this.createQuizDAL = createQuizDAL;
        }
        public List<QuizQuestionsDisplayDTO> GetCategoryQuestions(int categoryId, string organisation)
        {
            List<QuizQuestionsDisplay> questions = createQuizDAL.GetCategoryQuestions(categoryId, organisation);
            List<QuizQuestionsDisplayDTO> questionDTOs = mapper.Map<List<QuizQuestionsDisplayDTO>>(questions);
            return questionDTOs;
        }
        public bool CreateQuiz(QuizDetailsDTO quizDetailsDTO)
        {
            QuizDetails quizDetails = mapper.Map<QuizDetails>(quizDetailsDTO);
            bool result = createQuizDAL.CreateQuiz(quizDetails); 
            return result;
        }
        public List<QuestionDTO> GetCategoryQuestionsByCreator(int categoryId, string organisation, string creatorName)
        {
            List<Question> questions = createQuizDAL.GetCategoryQuestionsByCreator(categoryId, organisation, creatorName);
            List<QuestionDTO> questionDTOs = mapper.Map<List<QuestionDTO>>(questions);
            return questionDTOs;
        }
    }
}
