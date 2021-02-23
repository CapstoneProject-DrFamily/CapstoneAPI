using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class ServiceDetail
    {
        public int ServiceDetailId { get; set; }
        public int FamilyId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AcceptedDate { get; set; }
        public int ServiceId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Service Service { get; set; }
    }
}
