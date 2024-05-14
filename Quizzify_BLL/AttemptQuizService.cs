using AutoMapper;
using Quizzify_API.Models;
using Quizzify_BLL;
using Quizzify_BLL.DTO;

namespace Quizzify_BLL
{
    public class AttemptQuizService
    {
        private readonly IMapper mapper;
        private readonly AttemptQuizDAL attemptQuizDAL; 

        public AttemptQuizService(AttemptQuizDAL attemptQuizDAL)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                cfg.CreateMap<Question, QuestionDTO>().ReverseMap();
                cfg.CreateMap<QuizQuestion, QuizQuestionDTO>().ReverseMap();
                cfg.CreateMap<Answer, AnswerDTO>().ReverseMap();
                cfg.CreateMap<Quiz, QuizDTO>();
                cfg.CreateMap<QuizDTO, Quiz>();


            });
            mapper = mapConfig.CreateMapper();
            this.attemptQuizDAL = attemptQuizDAL;
        }

        public QuizDTO GetQuizByQuizId(int quizId)
        {
            Quiz quiz = attemptQuizDAL.GetQuizByQuizId(quizId);
            return mapper.Map<QuizDTO>(quiz);
        }

        public List<QuizDTO> GetListOfQuizzes(int organisationId)
        {
            List<Quiz> quizzes = attemptQuizDAL.GetListOfQuizzes(organisationId);
            List<QuizDTO> quizDTOs = mapper.Map<List<QuizDTO>>(quizzes);
            return quizDTOs;
        }

        public List<QuestionDTO> GetQuizQuestions(int quizId)
        {
            List<Question> questions = attemptQuizDAL.GetQuizQuestions(quizId);
            List<QuestionDTO> questionDTOs = mapper.Map<List<QuestionDTO>>(questions);
            return questionDTOs;
        }

        public AnswerDTO GetQuestionAnswer(int questionId)
        {
            Answer answer = attemptQuizDAL.GetQuestionAnswer(questionId);
            return mapper.Map<AnswerDTO>(answer);
        }


        //public void UpdateQuiz(QuizDTO quizDTO)
        //{
        //    QuizzifyDbContext context = new QuizzifyDbContext();
        //    var quizEntity = mapper.Map<Quiz>(quizDTO);
        //    context.QuizzifyQuiz.Update(quizEntity);
        //    context.SaveChanges();
        //}

        //public QuizQuestionDTO GetQuizQuestionById(int quizQuestionId)
        //{
        //    QuizQuestion quizQuestion = attemptQuizDAL.GetQuizQuestionById(quizQuestionId);
        //    return mapper.Map<QuizQuestionDTO>(quizQuestion);
        //}
        //// Get all quiz questions
        //public List<QuizQuestionDTO> GetAllQuizQuestions()
        //{
        //    List<QuizQuestion> quizQuestions = attemptQuizDAL.GetAllQuizQuestions();
        //    return mapper.Map<List<QuizQuestionDTO>>(quizQuestions);
        //}

        //public List<QuestionDTO> GetAllQuestions()
        //{
        //    List<Question> questions = attemptQuizDAL.GetAllQuesions();
        //    return mapper.Map<List<QuestionDTO>>(questions);
        //}
        //public QuestionDTO GetQuestionById(int id)
        //{
        //    Question question = attemptQuizDAL.GetQuestionById(id);
        //    return mapper.Map<QuestionDTO>(question);
        //}
        //public List<AnswerDTO> GetAllAnswers()
        //{
        //    List<Answer> answers = attemptQuizDAL.GetAllAnswers();
        //    return mapper.Map<List<AnswerDTO>>(answers);
        //}
        //public AnswerDTO GetAnswerById(int id)
        //{
        //    Answer answer = attemptQuizDAL.GetAnswerById(id);
        //    return mapper.Map<AnswerDTO>(answer);
        //}

        //public List<QuizQuestionWithAnswersDTO> GetQuizQuestionWithAnswersById(int quizid)
        //{
        //    QuizzifyDbContext context = new QuizzifyDbContext();
        //    var questionWithAnswers = context.QuizzifyQuizQuestions.
        //        Where(qq => qq.QuizId == quizid)
        //        .Select(qq => new QuizQuestionWithAnswersDTO
        //        {
        //            Id = qq.Id,
        //            QuizId = qq.QuizId,
        //            QuestionText = qq.Question.QuestionText,
        //            Options = context.QuizzifyAnswer
        //            .Where(a => a.QuestionId == qq.Id)
        //           .Select(q => new OptionsDTO
        //           {
        //               Option_Answers = q.OptionsAnswers,
        //               IsCorrect = q.IsCorrect
        //           }).ToList()
        //        }).ToList();

        //    return questionWithAnswers;
        //}
        //public List<AttemptQuizQuestionDTO> GetAllQuizQuestionsById(int quizId)
        //{
        //    using (QuizzifyDbContext context = new QuizzifyDbContext())
        //    {
        //        var allQuizQuestions = context.QuizzifyQuizQuestions
        //            .Where(qq => qq.QuizId == quizId)
        //            .Select(qq => new
        //            {
        //                QuizId = qq.QuizId,
        //                QuestionId = qq.QuestionId,
        //                Marks = qq.Marks
        //            })
        //            .ToList();

        //        var quizDetails = context.QuizzifyQuiz
        //            .Where(q => q.Id == quizId)
        //            .Select(q => new AttemptQuizDTO
        //            {
        //                UserId = q.UserId,
        //                Name = q.Name,
        //                Description = q.Description,
        //                QuizCategory = q.QuizCategory,
        //                StartDate = q.StartDate,
        //                EndDate = q.EndDate,
        //                Duration = q.Duration,
        //                Level = q.Level,
        //                TotalQuestions = q.TotalQuestion,
        //                TotalMarks = q.TotalMarks
        //            })
        //            .FirstOrDefault();

        //        var attemptQuizQuestions = allQuizQuestions
        //            .Select(qq => new AttemptQuestionDTO
        //            {
        //                QuestionText = context.QuizzifyQuestion
        //                    .Where(ques => ques.Id == qq.QuestionId)
        //                    .Select(ques => ques.QuestionText)
        //                    .FirstOrDefault(),
        //                AttemptQuestionsType = context.QuizzifyQuestion
        //                    .Where(ques => ques.Id == qq.QuestionId)
        //                    .Select(ques => new AttemptQuestionTypeDTO
        //                    {
        //                        Type = context.QuizzifyQuestionType
        //                            .Where(t => t.Id == ques.QuestionTypeId)
        //                            .Select(t => t.Type)
        //                            .FirstOrDefault()
        //                    })
        //                    .FirstOrDefault(),
        //                QuestionMarks = context.QuizzifyQuestion
        //                .Where(ques => ques.Id == qq.QuestionId)
        //                .Select(ques => new QuestionMarksDTO
        //                {
        //                    Marks = qq.Marks,
        //                }).FirstOrDefault(),
        //                AttemptQuestionsOptions = context.QuizzifyAnswer
        //                    .Where(opt => opt.QuestionId == qq.QuestionId)
        //                    .Select(opt => new AttemptQuestionsOptionsDTO
        //                    {
        //                        OptionAnswer = opt.OptionsAnswers,
        //                        IsCorrect = opt.IsCorrect
        //                    })
        //                    .ToList()
        //            })
        //            .ToList();

        //        var result = new List<AttemptQuizQuestionDTO>
        //        {
        //           new AttemptQuizQuestionDTO
        //           {
        //              AttemptQuiz = quizDetails,
        //              AttemptQuestion = attemptQuizQuestions
        //           }
        //        };

        //        return result;
        //    }
        //}

        //public IEnumerable<CreatedQuizzesDTO> AllCreatedQuizzes()
        //{
        //    QuizzifyDbContext context = new QuizzifyDbContext();
        //    var quizzes = context.QuizzifyQuiz.ToList();
        //    var createdQuizzesDTO = quizzes.Select(q => new CreatedQuizzesDTO
        //    {
        //        Id = q.Id,
        //        Name = q.Name,
        //        TotalQuestions = q.TotalQuestion,
        //        TotalMarks = q.TotalMarks,
        //        Duration = q.Duration,
        //        Level = q.Level,
        //        StartDate = q.StartDate,
        //        EndDate = q.EndDate,
        //        IsEnable = q.IsEnable,
        //        AutoValidation = q.AutoValidation
        //    }).ToList();

        //    return createdQuizzesDTO;

        //}
        //public CreatedQuizzesDTO GetCreatedQuizById(int createdQuizId)
        //{
        //    QuizzifyDbContext context = new QuizzifyDbContext();
        //    var createdQuiz = context.QuizzifyQuiz
        //        .Where(q => q.Id == createdQuizId)
        //        .Select(q => new CreatedQuizzesDTO
        //        {
        //            Id = q.Id,
        //            Name = q.Name,
        //            TotalQuestions = q.TotalQuestion,
        //            TotalMarks = q.TotalMarks,
        //            Duration = q.Duration,
        //            Level = q.Level,
        //            StartDate = q.StartDate,
        //            EndDate = q.EndDate,
        //            IsEnable = q.IsEnable,
        //            AutoValidation = q.AutoValidation
        //        }).FirstOrDefault();
        //    return createdQuiz;
        //}






        //private decimal CalculateObtainedMarks(QuizzifyDbContext context, Question question, int responseId, List<Answer> answers, decimal questionMarks)
        //{
        //    var attemptedAnswer = context.QuizzifyUserResponse
        //        .FirstOrDefault(ur => ur.QuestionId == question.Id && ur.ResponseId == responseId)?.AttemptedAnswer;

        //    // Normalize attempted answer for comparison
        //    attemptedAnswer = attemptedAnswer?.Trim().ToLower();

        //    // Filter correct answers for the question
        //    var correctAnswers = answers.Where(a => a.IsCorrect);

        //    foreach (var correctAnswer in correctAnswers)
        //    {
        //        var answerText = correctAnswer.OptionsAnswers.Trim().ToLower();

        //        // Compare attempted answer with correct answer(s)
        //        if (string.Equals(attemptedAnswer, answerText, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return questionMarks; // Return full marks if answer is correct
        //        }
        //    }

        //    return 0; // Return 0 marks if attempted answer is incorrect or null
        //}





    }
}
