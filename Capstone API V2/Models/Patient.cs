using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Feedbacks = new HashSet<Feedback>();
            Transactions = new HashSet<Transaction>();
        }

        public int PatientId { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string BloodType { get; set; }
        public int ProfileId { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public string Relationship { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual HealthRecord Record { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
