﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Account
    {
        public Account()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public bool? Waiting { get; set; }
        public string NotiToken { get; set; }

        public virtual Role Role { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
