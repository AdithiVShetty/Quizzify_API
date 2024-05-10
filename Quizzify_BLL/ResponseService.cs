//using Quizzify_API.Models;
//using Quizzify_DAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Quizzify_BLL
//{
//    public class ResponseService
//    {
//        public List<LeaderBoardDTO> GetAllAttemptedUsersOfQuizById(int quizId)
//        {
//            using (QuizzifyDbContext _context = new QuizzifyDbContext())
//            {
//                var userResponses = _context.QuizzifyResponse
//                    .Where(r => r.QuizId == quizId)
//                    .ToList(); // Fetch all responses for the quiz

//                var userAttempts = userResponses
//                    .GroupBy(r => new { r.UserId, r.QuizId })
//                    .Select(group => new
//                    {
//                        UserId = group.Key.UserId,
//                        QuizId = group.Key.QuizId,
//                        Attempts = group.Max(r => r.AttemptNumber),
//                        TotalTimeTakenSeconds = group.Sum(r => r.TimeTaken.TotalSeconds)
//                    })
//                    .ToList(); // Calculate summary data in memory

//                var leaderboard = (from r in userAttempts
//                                   let userName = _context.Users.FirstOrDefault(u => u.Id == r.UserId)?.Name
//                                   let score = CalculateScore(r.Attempts, r.TotalTimeTakenSeconds)
//                                   orderby score descending
//                                   select new LeaderBoardDTO
//                                   {
//                                       UserId = r.UserId,
//                                       UserName = userName,
//                                       Attempts = r.Attempts,
//                                       Rank = 0 // Initialize rank to 0
//                                   }).ToList();

//                // Assign ranks
//                for (int i = 0; i < leaderboard.Count; i++)
//                {
//                    leaderboard[i].Rank = i + 1;
//                }

//                return leaderboard;
//            }
//        }

//        private double CalculateScore(int attempts, double totalTimeTakenSeconds)
//        {
//            // Modify the scoring logic based on attempts and total time taken
//            double baseScore = 1 / (1 + (attempts * totalTimeTakenSeconds)) * 1000;

//            return baseScore;
//        }



//        public void UpdateManuallyMarksOfQuiz(int userId, int quizId, int attemptNumber, Dictionary<int, decimal> marks)
//        {
//            QuizzifyDbContext _context = new QuizzifyDbContext();
//            var response = _context.QuizzifyResponse.FirstOrDefault(r =>
//                r.UserId == userId && r.QuizId == quizId && r.AttemptNumber == attemptNumber);

//            if (response == null)
//            {
//                throw new InvalidOperationException("Response not found");
//            }

//            foreach (var entry in marks)
//            {
//                var questionId = entry.Key;
//                var obtainedMarks = entry.Value;

//                var userResponse = _context.QuizzifyUserResponse.FirstOrDefault(ur =>
//                    ur.ResponseId == response.Id && ur.QuestionId == questionId);

//                if (userResponse == null)
//                {
//                    throw new InvalidOperationException($"User response not found for question {questionId}");
//                }

//                // Update the obtained marks for the user response
//                userResponse.ObtainedMarks = obtainedMarks;

//                // Optionally, you can perform any additional actions here
//            }

//            _context.SaveChanges(); // Save changes to the database
//        }

//        public SeeAnswersDTO GetAttemptDetails(int userId, int quizId, int attemptNumber)
//        {
//            using (QuizzifyDbContext _context = new QuizzifyDbContext())
//            {
//                var response = _context.QuizzifyResponse.FirstOrDefault(r =>
//                    r.UserId == userId && r.QuizId == quizId && r.AttemptNumber == attemptNumber);

//                if (response == null)
//                {
//                    return null;
//                }

//                var quiz = _context.QuizzifyQuiz.FirstOrDefault(q => q.Id == quizId);

//                var questionIds = _context.QuizzifyQuizQuestions
//                    .Where(qq => qq.QuizId == quizId)
//                    .Select(qq => qq.QuestionId)
//                    .ToList();

//                var questionDetails = _context.QuizzifyQuestion
//                    .Where(q => questionIds.Contains(q.Id))
//                    .AsEnumerable() // Materialize the query here
//                    .ToList();

//                // Map response data to DTO
//                var attemptDetails = new SeeAnswersDTO
//                {
//                    QuizName = quiz.Name,
//                    Duration = response.TimeTaken, // Assuming TimeTaken is in seconds
//                    TotalScore = response.TotalScore
//                };

//                // Map questions and answers to DTOs
//                attemptDetails.Questions = questionDetails.Select(q =>
//                {
//                    var questionInQuiz = _context.QuizzifyQuizQuestions.FirstOrDefault(qq => qq.QuizId == quizId && qq.QuestionId == q.Id);
//                    var questionMarks = questionInQuiz != null ? questionInQuiz.Marks : 0;

//                    var answers = _context.QuizzifyAnswer
//                        .Where(a => a.QuestionId == q.Id)
//                        .ToList();

//                    // Calculate obtained marks for the question
//                    var obtainedMarks = CalculateObtainedMarks(_context, q, response.Id, answers, questionMarks);

//                    return new ReviewQuestionDTO
//                    {
//                        QuestionId = q.Id,
//                        QuestionText = q.QuestionText,
//                        Options = answers.Select(a => new ReviewAnswerDTO
//                        {
//                            AnswerId = a.Id,
//                            OptionText = a.OptionsAnswers,
//                            IsCorrect = a.IsCorrect,
//                            AttemptedAnswer = _context.QuizzifyUserResponse
//                                .FirstOrDefault(ur => ur.QuestionId == q.Id && ur.ResponseId == response.Id)
//                                ?.AttemptedAnswer ?? string.Empty, // Default value if AttemptedAnswer is null
//                            IsAttemptCorrect = _context.QuizzifyUserResponse
//                                .FirstOrDefault(ur => ur.QuestionId == q.Id && ur.ResponseId == response.Id)
//                                ?.AttemptedAnswer == a.OptionsAnswers // Check if attempted answer is correct inline
//                        }).ToList(),
//                        ObtainedMarks = obtainedMarks
//                    };
//                }).ToList();

//                return attemptDetails;
//            }
//        }
//        public List<MyQuizDTO> GetAllMyQuizzes(int userid)
//        {
//            QuizzifyDbContext context = new QuizzifyDbContext();
//            var quizzes = context.QuizzifyQuiz.ToList();
//            var myquizzes = quizzes.Select(q => new MyQuizDTO
//            {
//                QuizId = q.Id,
//                QuizName = q.Name,
//                TotalQuestions = q.TotalQuestion,
//                QuizCategory = q.QuizCategory,
//                Duration = q.Duration,
//                Level = q.Level,
//                Attempts = new ResponseCountDTO
//                {
//                    AttemptCount = context.QuizzifyResponse.Count(r => r.UserId == userid && r.QuizId == q.Id)
//                }
//            }).ToList();
//            return myquizzes;
//        }
//    }
//}
