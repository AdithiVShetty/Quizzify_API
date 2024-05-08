namespace Quizzify_DAL.ModelClass
{
    public class ChangePassword
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
