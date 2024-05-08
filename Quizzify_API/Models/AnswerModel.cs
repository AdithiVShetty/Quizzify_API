using Quizzify_DAL.ModelClass;
using System.ComponentModel;

namespace Quizzify_API.Models
{
    public class AnswerModel 
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string OptionsAnswers { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        [DefaultValue(false)]
        public bool IsCorrect { get; set; }
    }
}
