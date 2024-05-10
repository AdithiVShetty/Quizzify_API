using Quizzify_DAL;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quizzify_API.Models
{
    public class FeedbackModel
    {
       
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
      
    }
}
