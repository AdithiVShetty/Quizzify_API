using Quizzify_DAL.ModelClass;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Quizzify_BLL.DTO
{
    public class QuestionAddDTO
    {
        public int userid { get; set; }
        public string categoryname { get; set; }
        public int questiontypeid { get; set; }
        public string questiontext { get; set; }
        public string answer { get; set; }
        public string correctanswer { get; set; }
        public string? ImageData { get; set; } // Assuming you receive the image data as a string
        public string? ImageName { get; set; }

        //public int UserId { get; set; }
        //public List<string> CategoryNames { get; set; }
        //public List<int> QuestionTypeIds { get; set; }
        //public List<string> QuestionTexts { get; set; }
        //public List<string> Answers { get; set; }
        //public List<string> CorrectAnswers { get; set; }


    }
}
