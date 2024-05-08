using System.ComponentModel.DataAnnotations;

namespace Quizzify_DAL.ModelClass
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string Name{ get; set; }
    }
}