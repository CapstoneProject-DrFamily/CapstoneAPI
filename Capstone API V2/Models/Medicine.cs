using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            PrescriptionDetails = new HashSet<PrescriptionDetail>();
        }

        public int Id { get; set; }
        public string Form { get; set; }
        public string Strength { get; set; }
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
        public bool? Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
