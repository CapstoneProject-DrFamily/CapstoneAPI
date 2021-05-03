using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Disease
    {
        public Disease()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public string DiseaseCode { get; set; }
        public string ChapterCode { get; set; }
        public string ChapterName { get; set; }
        public string MainGroupCode { get; set; }
        public string MainGroupName { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string DiseaseName { get; set; }
        public bool? Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
