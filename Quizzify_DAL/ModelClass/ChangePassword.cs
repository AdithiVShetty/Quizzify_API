namespace Quizzify_BLL.DTO
{
    public class ChangePassword
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
