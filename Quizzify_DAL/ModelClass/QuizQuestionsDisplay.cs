using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL.DTO
{
    public class QuizQuestionsDisplay
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public bool IsEnable { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public string CreatedBy { get; set; }
        public decimal Marks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
