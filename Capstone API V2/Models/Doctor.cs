using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Feedbacks = new HashSet<Feedback>();
            Schedules = new HashSet<Schedule>();
            Transactions = new HashSet<Transaction>();
        }

        public int DoctorId { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Description { get; set; }
        public int SpecialtyId { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public string School { get; set; }
        public bool? Disabled { get; set; }

        public virtual Profile DoctorNavigation { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
