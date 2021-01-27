using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class PrescriptionDetail
    {
        public int PrescriptionDetailId { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int QuantityPerTime { get; set; }
        public string Method { get; set; }
        public int? TimesPerDay { get; set; }
        public int? NumberOfTime { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
