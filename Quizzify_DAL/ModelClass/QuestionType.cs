using System.ComponentModel.DataAnnotations;

namespace Quizzify_BLL.DTO
{
    public class QuestionType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}