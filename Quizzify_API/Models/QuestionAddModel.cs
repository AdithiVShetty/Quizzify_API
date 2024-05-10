using Quizzify_BLL.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Quizzify_API.Models
{
    public class QuestionAddModel
    {
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
        public string? ImageData { get; set; } // Assuming you receive the image data as a string
        public string? ImageName { get; set; }


        //public int UserId { get; set; }
        //public string CategoryName { get; set; }
        //public int QuestionTypeId { get; set; }
        //public List<string> QuestionTexts { get; set; }
        //public List<string> Answers { get; set; }
        //public List<string> CorrectAnswers { get; set; }
        //public List<string> ImageDatas { get; set; }
        //public List<string> ImageNames { get; set; }




    }
}
