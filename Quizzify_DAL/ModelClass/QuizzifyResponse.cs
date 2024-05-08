namespace Quizzify_DAL.ModelClass
{
    public class QuizzifyResponse
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public decimal TotalScore { get; set; }
        public int AttemptNumber { get; set; }
        public Quiz Quiz { get; set; }
        public User User { get; set; }
    }
}
