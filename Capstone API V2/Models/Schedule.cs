﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Treatments = new HashSet<Treatment>();
        }

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public bool? Status { get; set; }
        public bool? Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
