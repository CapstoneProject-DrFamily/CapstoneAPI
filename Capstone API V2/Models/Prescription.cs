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
            Transactions = new HashSet<Transaction>();
        }

        public int PrescriptionId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
