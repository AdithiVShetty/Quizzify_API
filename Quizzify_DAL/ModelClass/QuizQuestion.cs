using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_BLL.DTO
{
    public class QuizQuestion
    {
        [Key]
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        //public int CategoryId { get; set; }
        public decimal Marks { get; set; }
        public Quiz Quiz { get; set; }
        public Question Question { get; set; }
        //public Category Category { get; set; }
    }
}
