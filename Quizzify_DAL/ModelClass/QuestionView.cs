namespace Quizzify_BLL.DTO
{
    public class QuestionView
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public bool IsEnable { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
