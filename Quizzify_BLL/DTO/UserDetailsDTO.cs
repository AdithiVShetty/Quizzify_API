namespace Quizzify_BLL.DTO
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Organisation { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
    }
}
