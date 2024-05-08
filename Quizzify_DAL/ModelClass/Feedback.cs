using System.ComponentModel.DataAnnotations;

namespace Quizzify_DAL.ModelClass
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public Quiz Quiz { get; set; }
        public User User { get; set; }
    }
}
