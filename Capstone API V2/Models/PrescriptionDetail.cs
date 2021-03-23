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
        public string PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int? MorningQuantity { get; set; }
        public string Method { get; set; }
        public int? NoonQuantity { get; set; }
        public int TotalQuantity { get; set; }
        public int? AfternoonQuantity { get; set; }
        public string Type { get; set; }
        public int? TotalDays { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
