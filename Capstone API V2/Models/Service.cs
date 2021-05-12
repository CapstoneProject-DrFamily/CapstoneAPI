using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Service
    {
        public Service()
        {
            Treatments = new HashSet<Treatment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool? Disabled { get; set; }
        public int? SpecialtyId { get; set; }
        public bool? IsDefault { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
