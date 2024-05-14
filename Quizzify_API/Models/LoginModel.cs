using System.ComponentModel.DataAnnotations;

namespace Quizzify_API.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailId cannot be empty")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
        public string OrganisationName { get; set; }
        public string Role { get; set; }
        public bool IsApproved { get; set; }
    }
}
