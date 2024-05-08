using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL.DTO
{
    public class QuestionDisplayDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionText { get; set; }
        public bool IsEnable { get; set; }
        // public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
