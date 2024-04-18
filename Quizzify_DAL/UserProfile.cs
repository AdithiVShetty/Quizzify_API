using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzify_DAL
{
    public class UserProfile
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string Password { get; set; }
        public string OrganisationName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleName { get; set; }
    }
}
