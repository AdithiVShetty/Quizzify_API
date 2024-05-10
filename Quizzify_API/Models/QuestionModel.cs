using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Quizzify_BLL.DTO;

namespace Quizzify_API.Models
{
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string QuestionText { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
      
        public User? User { get; set; }
        public int QuestionTypeId { get; set; }
        public QuestionType? QuestionType { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [DefaultValue(true)]
        public bool IsEnable { get; set; }
    }
}
