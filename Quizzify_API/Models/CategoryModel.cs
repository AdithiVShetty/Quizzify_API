using System.ComponentModel.DataAnnotations;

namespace Quizzify_API.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
