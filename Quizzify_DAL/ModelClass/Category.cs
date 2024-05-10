using System.ComponentModel.DataAnnotations;

namespace Quizzify_BLL.DTO
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}