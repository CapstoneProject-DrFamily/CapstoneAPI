using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Prescription
    {
        public Prescription()
        {
            PrescriptionDetails = new HashSet<PrescriptionDetail>();
        }

        public string PrescriptionId { get; set; }
        public string Description { get; set; }
        public bool? IsTemplate { get; set; }
        public string DiseaseId { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual Disease Disease { get; set; }
        public virtual Transaction PrescriptionNavigation { get; set; }
        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
