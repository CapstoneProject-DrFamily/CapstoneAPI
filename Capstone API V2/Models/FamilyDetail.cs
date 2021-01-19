using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class FamilyDetail
    {
        public int FamilyDetailId { get; set; }
        public int FamilyId { get; set; }
        public int PatientId { get; set; }
        public string Relationship { get; set; }
        public string Phone { get; set; }
        public bool? Disabled { get; set; }

        public virtual Family Family { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
