using Quizzify_DAL.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_DAL
{
    public class CreateQuizDAL
    {
        private QuizzifyDbContext context;
        public CreateQuizDAL(QuizzifyDbContext context)
        {
            this.context = context;
        }
        public List<Category> GetCategories()
        {
            return context.QuizzifyCategory.ToList();
        }
        //return context.Organisations.FirstOrDefault(o => o.Name == organisationName);
        public List<Question> GetCategoryQuestions(int categoryId, string organisation)
        {
            Organisation org = context.Organisations.FirstOrDefault(o => o.Name == organisation);
            //return context.QuizzifyQuestions
            //          .Where(qq => qq.CategoryId == categoryId)
            //          .ToList();
            var query = from qq in context.QuizzifyQuestion
                        join u in context.Users on qq.UserId equals u.Id
                        where (u.OrganisationId == org.Id && qq.CategoryId == categoryId && qq.IsEnable)
                        select qq;

            var result = query.ToList();
            return result;
        }
        public bool CreateQuiz(QuizDetails quizDetails)
        {
            Quiz quiz = new Quiz();
            quiz.UserId=quizDetails.UserId;
            quiz.Name = quizDetails.quizname;
            quiz.AutoValidation = quizDetails.autoValidation;
            quiz.StartDate = quizDetails.startDate;
            quiz.EndDate = quizDetails.endDate;
            quiz.Duration = quizDetails.duration;
            quiz.Description = quizDetails.description;
            quiz.TotalMarks = quizDetails.totalMarks;
            quiz.TotalQuestion= quizDetails.totalQuestion;
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
            if(result > 0)
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
