namespace Quizzify_API.Models
{
    public class QuizQuestionModel
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public decimal Marks { get; set; }
    }
}
