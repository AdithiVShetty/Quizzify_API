namespace Quizzify_BLL.DTO
{
    public class ChangePasswordDTO
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
