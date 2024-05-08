namespace Quizzify_API.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
