namespace Quizzify_BLL.DTO
{
    public class AttemptQuizQuestionDetails
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Category { get; set; }
        public List<Answer> Answers { get; set; }
        public decimal Weightage { get; set; }
    }
}
