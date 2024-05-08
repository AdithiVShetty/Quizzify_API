using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Quizzify_BLL.DTO;
using Quizzify_DAL.DAL;
using Quizzify_DAL;
using Quizzify_DAL.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL
{
    public class CreateQuizService
    {
        private readonly IMapper mapper;
        private readonly CreateQuizDAL createQuizDAL;
        public CreateQuizService(CreateQuizDAL createQuizDAL)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Question, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<QuizDetailsDTO, QuizDetails>();
                cfg.CreateMap<QuizDetails, QuizDetailsDTO>();
                cfg.CreateMap<SelectedQuestionDTO,SelectedQuestion>();
                cfg.CreateMap<SelectedQuestion, SelectedQuestionDTO>();
            });
            mapper = mapConfig.CreateMapper();
            this.createQuizDAL = createQuizDAL;
        }
        public List<CategoryDTO> GetCategories()
        {
            List<Category> categories = createQuizDAL.GetCategories();
            List<CategoryDTO> caregoryDTOs = mapper.Map<List<CategoryDTO>>(categories);
            return caregoryDTOs;
        }
        public List<QuestionDTO> GetCategoryQuestions(int categoryId, string organisation)
        {
            List<Question> questions = createQuizDAL.GetCategoryQuestions(categoryId, organisation);
            List<QuestionDTO> questionDTOs = mapper.Map<List<QuestionDTO>>(questions);
            return questionDTOs;
        }
        public bool CreateQuiz(QuizDetailsDTO quizDetailsDTO)
        {
            QuizDetails quizDetails = mapper.Map<QuizDetails>(quizDetailsDTO);
            bool result = createQuizDAL.CreateQuiz(quizDetails);
            return result;
        }
    }
}
