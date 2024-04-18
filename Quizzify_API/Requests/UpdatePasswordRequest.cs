namespace Quizzify_API.Requests
{
    public class UpdatePasswordRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
