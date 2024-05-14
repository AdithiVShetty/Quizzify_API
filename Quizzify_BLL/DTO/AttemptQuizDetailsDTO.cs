namespace Quizzify_BLL.DTO
{
    public class AttemptQuizDetailsDTO
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string QuizDescription { get; set; }
        public string QuizCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Level { get; set; }
        public decimal TotalMarks { get; set; }
        public int TotalQuestions { get; set; }
        public List<AttemptQuizQuestionDetailsDTO> Questions { get; set; }
    }
}
