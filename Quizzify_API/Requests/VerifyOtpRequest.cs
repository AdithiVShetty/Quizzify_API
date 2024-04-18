namespace Quizzify_API.Requests
{
    public class VerifyOtpRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
