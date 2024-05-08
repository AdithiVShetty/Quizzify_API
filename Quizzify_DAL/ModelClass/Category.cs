using System.ComponentModel.DataAnnotations;

namespace Quizzify_DAL.ModelClass
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}