namespace Quizzify_API.Models
{
    public class QuizDetailsModel
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
        public List<SelectedQuestionModel> selectedQuestions { get; set; }
    }
}
