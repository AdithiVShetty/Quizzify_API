using System.ComponentModel.DataAnnotations;

namespace Quizzify_DAL
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
