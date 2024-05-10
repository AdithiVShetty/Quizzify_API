using AutoMapper;
using Quizzify_BLL.DTO;
using Image = Quizzify_BLL.DTO.Image;

namespace Quizzify_BLL
{
    public class QuestionDAL
    {
        private readonly QuizzifyDbContext db;
        private readonly IMapper mapper;

        public QuestionDAL(QuizzifyDbContext dbContext)
        {
            db = dbContext;
        }
        public IEnumerable<Category> GetCategories()
        {
            return db.QuizzifyCategory.ToList();
        }
        public Category GetCategoryById(int categoryId)
        {
            return db.QuizzifyCategory.FirstOrDefault(c => c.Id == categoryId);
        }
        public QuestionType GetQuestionTypeById(int questionTypeId)
        {
            return db.QuizzifyQuestionType.FirstOrDefault(qt => qt.Id == questionTypeId);
        }
        public IEnumerable<QuestionType> GetQuestionTypes()
        {
            return db.QuizzifyQuestionType.ToList();
        }
        public Question AddQuestion(Question question, string answer, string correctAnswer, string categoryName, byte[] imageData, string imageName)
        {
            var existingCategory = db.QuizzifyCategory.FirstOrDefault(c => c.Name == categoryName);
            if (existingCategory == null)
            {
                var newCategory = new Category { Name = categoryName };
                db.QuizzifyCategory.Add(newCategory);
                db.SaveChanges();
                question.CategoryId = newCategory.Id;
            }
            else
            {
                question.CategoryId = existingCategory.Id;
            }

            db.QuizzifyQuestion.Add(question);
            db.SaveChanges();

            int new_QuestionId = question.Id;

            var options = answer.Split('|').Select(opt => opt.Trim());

            foreach (var option in options)
            {
                bool isCorrectAnswer = correctAnswer.Contains(option);
                var answerEntity = new Answer
                {
                    QuestionId = new_QuestionId,
                    OptionsAnswers = option,
                    ImageId = 1,
                    IsCorrect = isCorrectAnswer
                };

                db.QuizzifyAnswer.Add(answerEntity);
                db.SaveChanges();
            }

            if (imageData != null)
            {
                var existingImage = db.QuizzifyImage.FirstOrDefault(img => img.Name == imageName);
                if (existingImage == null)
                {
                    var image = new Image
                    {
                        Name = imageName,
                        Data = imageData
                    };
                    db.QuizzifyImage.Add(image);
                    db.SaveChanges();

                    question.ImageId = image.Id;
                }
                else
                {
                    question.ImageId = existingImage.Id;
                }

                db.SaveChanges();
            }
            return question;
        }
        //public List<Question> AddQuestions(List<Question> questions, List<string> answers, List<string> correctAnswers, string categoryName, List<byte[]> imageDatas, List<string> imageNames)
        //{
        //    List<Question> addedQuestions = new List<Question>();

        //    for (int i = 0; i < questions.Count; i++)
        //    {
        //        Question question = questions[i];
        //        var existingCategory = db.QuizzifyCategory.FirstOrDefault(c => c.Name == categoryName);
        //        if (existingCategory == null)
        //        {
        //            var newCategory = new Category { Name = categoryName };
        //            db.QuizzifyCategory.Add(newCategory);
        //            db.SaveChanges();
        //            question.CategoryId = newCategory.Id;
        //        }
        //        else
        //        {
        //            question.CategoryId = existingCategory.Id;
        //        }

        //        db.QuizzifyQuestion.Add(question);
        //        db.SaveChanges();

        //        int new_QuestionId = question.Id;

        //        var options = answers[i].Split('|').Select(opt => opt.Trim());

        //        foreach (var option in options)
        //        {
        //            bool isCorrectAnswer = correctAnswers[i].Contains(option);
        //            var answerEntity = new Answer
        //            {
        //                QuestionId = new_QuestionId,
        //                OptionsAnswers = option,
        //                ImageId = 1,
        //                IsCorrect = isCorrectAnswer
        //            };

        //            db.QuizzifyAnswer.Add(answerEntity);
        //            db.SaveChanges();
        //        }

        //        if (imageDatas != null && imageDatas.Count > i && imageDatas[i] != null)
        //        {
        //            var existingImage = db.QuizzifyImage.FirstOrDefault(img => img.Name == imageNames[i]);
        //            if (existingImage == null)
        //            {
        //                var image = new Image
        //                {
        //                    Name = imageNames[i],
        //                    Data = imageDatas[i]
        //                };
        //                db.QuizzifyImage.Add(image);
        //                db.SaveChanges();

        //                question.ImageId = image.Id;
        //            }
        //            else
        //            {
        //                question.ImageId = existingImage.Id;
        //            }
        //            db.SaveChanges();
        //        }
        //        addedQuestions.Add(question);
        //    }
        //    return addedQuestions;
        //}

        //public List<Question> AddQuestions(List<Question> questions, List<string> answers, List<string> correctAnswers, string categoryName, List<byte[]> imageDatas, List<string> imageNames)
        //{
        //    var addedQuestions = new List<Question>();

        //    var existingCategory = db.QuizzifyCategory.FirstOrDefault(c => c.Name == categoryName);
        //    if (existingCategory == null)
        //    {
        //        var newCategory = new Category { Name = categoryName };
        //        db.QuizzifyCategory.Add(newCategory);
        //        db.SaveChanges();
        //    }

        //    for (int i = 0; i < questions.Count; i++)
        //    {
        //        var question = questions[i];
        //        var answer = answers[i];
        //        var correctAnswer = correctAnswers[i];
        //        var imageData = imageDatas[i];
        //        var imageName = imageNames[i];

        //        if (existingCategory == null)
        //        {
        //            var newCategory = new Category { Name = categoryName };
        //            question.CategoryId = newCategory.Id;
        //        }
        //        else
        //        {
        //            question.CategoryId = existingCategory.Id;
        //        }

        //        db.QuizzifyQuestion.Add(question);
        //        db.SaveChanges();

        //        int newQuestionId = question.Id;

        //        var options = answer.Split('|').Select(opt => opt.Trim());

        //        foreach (var option in options)
        //        {
        //            bool isCorrectAnswer = correctAnswer.Contains(option);
        //            var answerEntity = new Answer
        //            {
        //                QuestionId = newQuestionId,
        //                OptionsAnswers = option,
        //                ImageId = 1, // Adjust as needed
        //                IsCorrect = isCorrectAnswer
        //            };

        //            db.QuizzifyAnswer.Add(answerEntity);
        //            db.SaveChanges();
        //        }

        //        if (imageData != null)
        //        {
        //            var existingImage = db.QuizzifyImage.FirstOrDefault(img => img.Name == imageName);
        //            if (existingImage == null)
        //            {
        //                var image = new Image
        //                {
        //                    Name = imageName,
        //                    Data = imageData
        //                };
        //                db.QuizzifyImage.Add(image);
        //                db.SaveChanges();

        //                question.ImageId = image.Id;
        //            }
        //            else
        //            {
        //                question.ImageId = existingImage.Id;
        //            }
        //            db.SaveChanges();
        //        }
        //        addedQuestions.Add(question);
        //    }
        //    return addedQuestions;
        //}

        public Answer AddAnswer(Answer answer)
        {
            db.QuizzifyAnswer.Add(answer);
            db.SaveChanges();
            return answer;
        }
        public List<QuestionDisplay> GetQuestionsByUserId(int userId)
        {
            var questionAnswer = from question in db.QuizzifyQuestion
                        join answer in db.QuizzifyAnswer on question.Id equals answer.QuestionId
                        join category in db.QuizzifyCategory on question.CategoryId equals category.Id
                        where question.UserId == userId && answer.IsCorrect
                        orderby question.CreatedDate descending
                        select new QuestionDisplay
                        {
                            Id = question.Id,
                            UserId = question.UserId,
                            QuestionTypeId = question.QuestionTypeId,
                            QuestionText = question.QuestionText,
                            //Answer = answer.OptionsAnswers,
                            CorrectAnswer = answer.OptionsAnswers,
                            CategoryName = category.Name,
                            CreatedDate = question.CreatedDate,
                            IsEnable = question.IsEnable,
                        };

            var distinctQuestions = questionAnswer.GroupBy(q => q.Id).Select(group => group.First()).ToList();
            return distinctQuestions;
        }
        public void ToggleQuestionStatus(int questionId)
        {
            var question = db.QuizzifyQuestion.FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                question.IsEnable = !question.IsEnable;
                db.SaveChanges();
            }
        }
        public QuestionView GetQuestionByQuestionId(int questionId)
        {
            var question = db.QuizzifyQuestion.FirstOrDefault(q => q.Id == questionId);

            var category = db.QuizzifyCategory.FirstOrDefault(c => c.Id == question.CategoryId);

            var optionsAnswers = db.QuizzifyAnswer.Where(a => a.QuestionId == questionId).Select(a => a.OptionsAnswers).ToList();
            var correctAnswer = db.QuizzifyAnswer.FirstOrDefault(a => a.QuestionId == questionId && a.IsCorrect)?.OptionsAnswers;
            var isEnable = question.IsEnable;

            byte[] imageData = null;
            if (question.ImageId.HasValue)
            {
                var image = db.QuizzifyImage.FirstOrDefault(img => img.Id == question.ImageId);
                imageData = image.Data;
            }

            var questionDetails = new QuestionView
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                CreatedDate = question.CreatedDate,
                CategoryName = category.Name,
                Answers = optionsAnswers,
                CorrectAnswer = correctAnswer,
                IsCorrect = optionsAnswers.Any(),
                IsEnable = isEnable,
                ImageData = imageData
            };

            return questionDetails;
        }
        public List<QuestionDisplay> GetListOfQuestionsByCategory(int userId, string categoryName)
        {
            var questionAnswer = from question in db.QuizzifyQuestion
                                 join answer in db.QuizzifyAnswer on question.Id equals answer.QuestionId
                                 join category in db.QuizzifyCategory on question.CategoryId equals category.Id
                                 where question.UserId == userId && category.Name == categoryName && answer.IsCorrect
                                 select new QuestionDisplay
                                 {
                                     Id = question.Id,
                                     UserId = question.UserId,
                                     QuestionTypeId = question.QuestionTypeId,
                                     QuestionText = question.QuestionText,
                                     CorrectAnswer = answer.OptionsAnswers,
                                     CategoryName = category.Name,
                                     CreatedDate = question.CreatedDate,
                                     IsEnable = question.IsEnable,
                                 };

            var distinctQuestions = questionAnswer.GroupBy(q => q.Id).Select(group => group.First()).ToList();
            return distinctQuestions;
        }

    }
}