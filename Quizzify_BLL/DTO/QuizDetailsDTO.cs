using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL.DTO
{
    public class QuizDetailsDTO
    {
        public int UserId { get; set; }
        public string quizname { get; set; }
        public bool autoValidation { get; set; }
        //public string Organisation { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string level { get; set; }
        public TimeSpan duration { get; set; }
        public string categories { get; set; }
        public int totalMarks { get; set; }
        public int totalQuestion { get; set; }
        public List<SelectedQuestionDTO> selectedQuestions { get; set; }
    }
}
