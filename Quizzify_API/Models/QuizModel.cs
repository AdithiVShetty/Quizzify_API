using Quizzify_DAL.ModelClass;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Quizzify_API.Models
{
    public class QuizModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string QuizCategory { get; set; }
        public bool AutoValidation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Level { get; set; }
        public decimal TotalMarks { get; set; }
        public int TotalQuestion { get; set; }
        public bool IsEnable { get; set; }
        public User User { get; set; }
    }
}
