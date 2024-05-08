using AutoMapper;
using Quizzify_BLL.DTO;
using Quizzify_DAL;
using Quizzify_DAL.ModelClass;

namespace Quizzify_BLL
{
    public class QuestionService
    {
        private readonly IMapper mapper;
        private readonly QuestionDAL questionDAL;
        private readonly UserDAL userDAL;

        public QuestionService(QuestionDAL questionDAL, UserDAL userDAL)
        {
            var mapConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<QuestionType, QuestionTypeDTO>();
                cfg.CreateMap<QuestionTypeDTO, QuestionType>();
                cfg.CreateMap<Answer, AnswerDTO>();
                cfg.CreateMap<AnswerDTO, Answer>();
                cfg.CreateMap<QuestionDisplay, QuestionDisplayDTO>();
                cfg.CreateMap<QuestionDisplayDTO, QuestionDisplay>();
                cfg.CreateMap<QuestionView, QuestionViewDTO>();
                cfg.CreateMap<QuestionViewDTO, QuestionView>();
            });
            mapper = mapConfig.CreateMapper();
            this.questionDAL = questionDAL;
            this.userDAL = userDAL;

        }
        public List<QuestionDisplayDTO> GetQuestionsByUserId(int userId)
        {
            List<QuestionDisplay> questions = questionDAL.GetQuestionsByUserId(userId);
            return mapper.Map<List<QuestionDisplayDTO>>(questions);
        }
        public QuestionDTO CreateQuestion(int userId, string categoryName, int questionTypeId, string questionText, string answer, string correctAnswer, byte[] imageData, string imageName)
        {
            var user = userDAL.GetUserById(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            var question = new Question
            {
                UserId = userId,
                //CategoryId = categoryId,
                QuestionTypeId = questionTypeId,
                QuestionText = questionText,

                CreatedBy = user.Name,
                CreatedDate = DateTime.Now,
                IsEnable = true
            };

            var addedQuestion = questionDAL.AddQuestion(question, answer, correctAnswer, categoryName, imageData, imageName);

            var questionDTO = new QuestionDTO
            {
                Id = addedQuestion.Id,
                UserId = addedQuestion.UserId,
                CategoryId = addedQuestion.CategoryId,
                QuestionTypeId = addedQuestion.QuestionTypeId,
                QuestionText = addedQuestion.QuestionText,
                CreatedBy = addedQuestion.CreatedBy,
                CreatedDate = addedQuestion.CreatedDate,
                IsEnable = addedQuestion.IsEnable
            };

            return questionDTO;
        }
        //public List<QuestionDTO> CreateQuestions(int userId, string categoryName, int questionTypeId, List<string> questionTexts, List<string> answers, List<string> correctAnswers, List<byte[]> imageDatas, List<string> imageNames)
        //{
        //    var user = userDAL.GetUserById(userId);
        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("User not found.");
        //    }

        //    var questions = new List<Question>();

        //    for (int i = 0; i < questionTexts.Count; i++)
        //    {
        //        var question = new Question
        //        {
        //            UserId = userId,
        //            QuestionTypeId = questionTypeId,
        //            QuestionText = questionTexts[i],
        //            CreatedBy = user.Name,
        //            CreatedDate = DateTime.Now,
        //            IsEnable = true
        //        };

        //        questions.Add(question);
        //    }

        //    var addedQuestions = questionDAL.AddQuestions(questions, answers, correctAnswers, categoryName, imageDatas, imageNames);

        //    var questionDTOs = addedQuestions.Select(q => new QuestionDTO
        //    {
        //        Id = q.Id,
        //        UserId = q.UserId,
        //        CategoryId = q.CategoryId,
        //        QuestionTypeId = q.QuestionTypeId,
        //        QuestionText = q.QuestionText,
        //        CreatedBy = q.CreatedBy,
        //        CreatedDate = q.CreatedDate,
        //        IsEnable = q.IsEnable
        //    }).ToList();



        //    return questionDTOs;
        //}


        //public List<QuestionDTO> CreateQuestions(int userId, string categoryName, int questionTypeId, List<string> questionTexts, List<string> answers, List<string> correctAnswers, List<byte[]> imageDatas, List<string> imageNames)
        //{
        //    var user = userDAL.GetUserById(userId);
        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("User not found.");
        //    }
        //    List<Question> questions = new List<Question>();

        //    for (int i = 0; i < questionTexts.Count; i++)
        //    {
        //        Question question = new Question
        //        {
        //            UserId = userId,
        //            QuestionTypeId = questionTypeId,
        //            QuestionText = questionTexts[i],

        //            CreatedBy = user.Name,
        //            CreatedDate = DateTime.Now,
        //            IsEnable = true
        //        };
        //        questions.Add(question);
        //    }

        //    var addedQuestions = questionDAL.AddQuestions(questions, answers, correctAnswers, categoryName, imageDatas, imageNames);

        //    List<QuestionDTO> questionDTOs = addedQuestions.Select(q => new QuestionDTO
        //    {
        //        Id = q.Id,
        //        UserId = q.UserId,
        //        CategoryId = q.CategoryId,
        //        QuestionTypeId = q.QuestionTypeId,
        //        QuestionText = q.QuestionText,
        //        CreatedBy = q.CreatedBy,
        //        CreatedDate = q.CreatedDate,
        //        IsEnable = q.IsEnable
        //    }).ToList();

        //    return questionDTOs;
        //}


        public IEnumerable<CategoryDTO> GetCategories()
        {
            var categories = questionDAL.GetCategories();
            var sortedCategories = categories.OrderBy(c => c.Name); // Sort by name

            var categoryDTOs = sortedCategories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return categoryDTOs;
        }
        public IEnumerable<QuestionTypeDTO> GetQuestionTypes()
        {
            var questionTypes = questionDAL.GetQuestionTypes();
            var questionTypeDTOs = new List<QuestionTypeDTO>();

            foreach (var questionType in questionTypes)
            {
                var questionTypeDTO = new QuestionTypeDTO
                {
                    Id = questionType.Id,
                    Type = questionType.Type
                };
                questionTypeDTOs.Add(questionTypeDTO);
            }

            return questionTypeDTOs;
        }
        public void ToggleQuestion(int questionId)
        {
            questionDAL.ToggleQuestionStatus(questionId);
        }
        public QuestionViewDTO GetQuestionByQuestionId(int questionId)
        {
            var questionDetails = questionDAL.GetQuestionByQuestionId(questionId);
            if (questionDetails == null)
            {
                throw new InvalidOperationException("Question not found.");
            }
            return mapper.Map<QuestionViewDTO>(questionDetails);
        }
        public List<QuestionDisplayDTO> GetListOfQuestionsByCategory(int userId, string categoryName)
        {
            List<QuestionDisplay> questions = questionDAL.GetListOfQuestionsByCategory(userId, categoryName);
            return mapper.Map<List<QuestionDisplayDTO>>(questions);
        }

    }
}