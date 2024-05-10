using Quizzify_BLL.DTO;

namespace Quizzify_BLL
{
    public class QuizDAL
    {
        private QuizzifyDbContext context;
        public QuizDAL(QuizzifyDbContext context)
        {
            this.context = context;
        }

        public List<QuizQuestionsDisplay> GetCategoryQuestions(int categoryId, string organisation)
        {
            Organisation org = context.Organisations.FirstOrDefault(o => o.Name == organisation);

            var query = from question in context.QuizzifyQuestion
                        join user in context.Users on question.UserId equals user.Id
                        join image in context.QuizzifyImage on question.ImageId equals image.Id into imageGroup
                        from img in imageGroup.DefaultIfEmpty()
                        join category in context.QuizzifyCategory on question.CategoryId equals category.Id
                        where (user.OrganisationId == org.Id && question.CategoryId == categoryId && question.IsEnable)
                        select new QuizQuestionsDisplay
                        {
                            Id = question.Id,
                            UserId = question.UserId,
                            QuestionText = question.QuestionText,
                            CategoryName = category.Name,
                            IsEnable = question.IsEnable,
                            CreatedBy = question.CreatedBy,
                            Answers = context.QuizzifyAnswer
                                    .Where(a => a.QuestionId == question.Id)
                                    .Select(a => a.OptionsAnswers)
                                    .ToList(),
                            //CorrectAnswer = GetCorrectAnswer(question.Id),
                            IsCorrect = context.QuizzifyAnswer.Any(a => a.QuestionId == question.Id && a.IsCorrect),
                            ImageData = img != null ? img.Data : null,
                        };

            var result = query.ToList();
            return result;
        }
        private string GetCorrectAnswer(int questionId)
        {
            return context.QuizzifyAnswer.FirstOrDefault(a => a.QuestionId == questionId && a.IsCorrect)?.OptionsAnswers;
        }
        public List<Question> GetCategoryQuestionsByCreator(int categoryId, string organisation, string creatorName)
        {
            Organisation org = context.Organisations.FirstOrDefault(o => o.Name == organisation);

            var query = from qq in context.QuizzifyQuestion
                        join u in context.Users on qq.UserId equals u.Id
                        where (u.OrganisationId == org.Id && qq.CreatedBy == creatorName &&qq.CategoryId == categoryId && qq.IsEnable)
                        select qq;

            var result = query.ToList();
            return result;
        }
        public bool CreateQuiz(QuizDetails quizDetails)
        {
            Quiz quiz = new Quiz();
            quiz.UserId = quizDetails.UserId;
            quiz.Name = quizDetails.quizname;
            quiz.AutoValidation = quizDetails.autoValidation;
            quiz.StartDate = quizDetails.startDate;
            quiz.EndDate = quizDetails.endDate;
            quiz.Duration = quizDetails.duration;
            quiz.Description = quizDetails.description;
            quiz.TotalMarks = quizDetails.totalMarks;
            quiz.TotalQuestion = quizDetails.totalQuestion;
            quiz.Level = quizDetails.level;
            quiz.QuizCategory = quizDetails.categories;
            var obj = context.QuizzifyQuiz.Add(quiz);
            context.SaveChanges();

            int new_quizId = quiz.Id;
            foreach (var selectedQuestion in quizDetails.selectedQuestions)
            {
                QuizQuestion quizQuestion = new QuizQuestion
                {
                    QuizId = new_quizId,
                    QuestionId = selectedQuestion.id,
                    Marks = selectedQuestion.weightage
                };
                context.QuizzifyQuizQuestions.Add(quizQuestion);
            }
            int result = context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}