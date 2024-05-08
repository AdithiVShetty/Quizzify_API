using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL.DTO
{
    public class QuestionDTO
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string QuestionText { get; set; }
        public int? ImageId { get; set; }
        public int QuestionTypeId { get; set; }
        public int CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [DefaultValue(true)]
        public bool IsEnable { get; set; }
    }
}
