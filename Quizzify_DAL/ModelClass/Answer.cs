using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quizzify_BLL.DTO
{
    public class Answer
    {
        [Key]
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