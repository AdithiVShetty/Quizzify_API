namespace Quizzify_API.Models
{
    public class CreateQuestionModel
    {
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public int QuestionTypeId { get; set; }
        public List<QuestionAddModel> Questions { get; set; }
       // public string ImageData { get; set; }
    }
}
