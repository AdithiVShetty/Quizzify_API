using Quizzify_API.Models;

namespace Quizzify_BLL.DTO
{
    public class AttemptQuizQuestionDetailsDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Category { get; set; }
        public List<AnswerDTO> Answers { get; set; }
        public decimal Weightage { get; set; }
    }
}
