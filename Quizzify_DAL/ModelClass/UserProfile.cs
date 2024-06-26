﻿using System.ComponentModel;

namespace Quizzify_BLL.DTO
{
    public class UserProfile
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string Password { get; set; }
        public string OrganisationName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role{ get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
