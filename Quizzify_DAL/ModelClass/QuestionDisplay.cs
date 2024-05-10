namespace Quizzify_BLL.DTO
{
    public class QuestionDisplay
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionText { get; set; }
        //public string Answer { get; set; }
        public bool IsEnable { get; set; }
        public string CorrectAnswer { get; set; }
        public DateTime? CreatedDate { get; set; } 
    }
}
