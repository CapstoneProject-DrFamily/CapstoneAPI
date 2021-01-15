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
        public int DrugId { get; set; }
        public int Quantity { get; set; }

        public virtual Drug Drug { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
