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
            ServiceDetails = new HashSet<ServiceDetail>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public bool? Disabled { get; set; }

        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
