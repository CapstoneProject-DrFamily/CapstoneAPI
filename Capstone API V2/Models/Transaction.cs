using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Transaction
    {
        public string TransactionId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public int? PrescriptionId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public byte? Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public bool? Disabled { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
