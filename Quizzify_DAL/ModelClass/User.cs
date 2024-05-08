using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Quizzify_DAL.ModelClass
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool IsApproved { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
        [DefaultValue(3)]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
