using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class User
    {
        public User()
        {
            Profiles = new HashSet<Profile>();
        }

        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
