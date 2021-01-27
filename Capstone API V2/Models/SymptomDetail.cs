using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class SymptomDetail
    {
        public int SymptomId { get; set; }
        public string TransactionId { get; set; }

        public virtual Symptom Symptom { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
