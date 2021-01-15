using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class HealthRecord
    {
        public int RecordId { get; set; }
        public int? PatientId { get; set; }
        public int? SymptomId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Symptom Symptom { get; set; }
    }
}
