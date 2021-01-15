using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Symptom
    {
        public Symptom()
        {
            HealthRecords = new HashSet<HealthRecord>();
        }

        public int SymptomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<HealthRecord> HealthRecords { get; set; }
    }
}
