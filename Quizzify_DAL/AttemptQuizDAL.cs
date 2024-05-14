using AutoMapper;
using Quizzify_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL
{
    public class AttemptQuizDAL
    {
        private QuizzifyDbContext context;
        public AttemptQuizDAL(QuizzifyDbContext context)
        {
            this.context = context;
        }
        public List<Quiz> GetListOfQuizzes(int organisationId)
        {
            var quizzes = context.QuizzifyQuiz
                            .Where(q => q.UserId == organisationId)
                            .OrderBy(q => q.Name)
                            .ToList();
            foreach (var quiz in quizzes)
            {
                var user = context.Users.FirstOrDefault(u => u.Id == quiz.UserId);
                quiz.Description = user.Name;
            }
            return quizzes;
        }

        public Quiz GetQuizByQuizId(int quizId)
        {
            return context.QuizzifyQuiz.FirstOrDefault(q => q.Id == quizId);
        }

        public List<Question> GetQuizQuestions(int quizId)
        {
            return context.QuizzifyQuizQuestions
                          .Where(q => q.QuizId == quizId)
                          .Select(q => q.Question)
                          .ToList();
        }

        public Answer GetQuestionAnswer(int questionId)
        {
            return context.QuizzifyAnswer.FirstOrDefault(a => a.QuestionId == questionId);
        }

        //public List<QuizQuestion> GetAllQuizQuestions()
        //{
        //    return context.QuizzifyQuizQuestions.ToList();
        //}
        //public QuizQuestion GetQuizQuestionById(int id)
        //{
        //    return context.QuizzifyQuizQuestions.FirstOrDefault(qq => qq.Id == id);
        //}
        //public List<Question> GetAllQuesions()
        //{
        //    return context.QuizzifyQuestion.ToList();
        //}
        //public Question GetQuestionById(int id)
        //{
        //    return context.QuizzifyQuestion.FirstOrDefault(ques => ques.Id == id);
        //}
        //public List<Answer> GetAllAnswers()
        //{
        //    return context.QuizzifyAnswer.ToList();
        //}
        //public Answer GetAnswerById(int id)
        //{
        //    return context.QuizzifyAnswer.FirstOrDefault(answer => answer.Id == id);
        //}
    }
}
