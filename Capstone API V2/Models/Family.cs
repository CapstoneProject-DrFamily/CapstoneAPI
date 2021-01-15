using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Family
    {
        public Family()
        {
            FamilyDetails = new HashSet<FamilyDetail>();
            ServiceDetails = new HashSet<ServiceDetail>();
        }

        public int FamilyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FamilyDetail> FamilyDetails { get; set; }
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
