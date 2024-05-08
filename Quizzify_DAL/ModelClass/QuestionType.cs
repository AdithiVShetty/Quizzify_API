using System.ComponentModel.DataAnnotations;

namespace Quizzify_DAL.ModelClass
{
    public class QuestionType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}