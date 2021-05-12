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
            HealthRecords = new HashSet<HealthRecord>();
            Treatments = new HashSet<Treatment>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Image { get; set; }
        public string IdCard { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string Relationship { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string BloodType { get; set; }
        public int AccountId { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public bool? Disabled { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<HealthRecord> HealthRecords { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
