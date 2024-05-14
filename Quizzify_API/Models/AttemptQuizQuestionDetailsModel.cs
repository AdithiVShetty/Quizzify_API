namespace Quizzify_API.Models
{
    public class AttemptQuizQuestionDetailsModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Category { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public decimal Weightage { get; set; }
    }
}
