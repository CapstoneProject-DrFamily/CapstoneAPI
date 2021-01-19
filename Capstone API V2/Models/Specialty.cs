using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int SpecialtyId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool? Disabled { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
