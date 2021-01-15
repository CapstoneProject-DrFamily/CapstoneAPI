using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public bool? Status { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
