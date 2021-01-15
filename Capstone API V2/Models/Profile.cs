﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Doctors = new HashSet<Doctor>();
            Patients = new HashSet<Patient>();
        }

        public int ProfileId { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int AccountId { get; set; }

        public virtual User Account { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
