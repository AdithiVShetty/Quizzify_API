﻿namespace Quizzify_API.Models
{
    public class UserProfileModel
    {
        public UserProfileModel()
        {
            IsActive = true;        
        }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? OrganisationName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }
}
