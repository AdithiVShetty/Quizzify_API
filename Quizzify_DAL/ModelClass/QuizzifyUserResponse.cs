namespace Quizzify_DAL.ModelClass
{
    public class QuizzifyUserResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AttemptedAnswer { get; set; }
        public decimal ObtainedMarks { get; set; }
        public int ResponseId { get; set; }
        public QuizzifyResponse Response { get; set; }
        public Question Question { get; set; }

    }
}
