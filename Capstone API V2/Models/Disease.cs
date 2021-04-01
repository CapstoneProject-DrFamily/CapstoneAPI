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

        public string Mãbệnh { get; set; }
        public string MãChương { get; set; }
        public string ChapterName { get; set; }
        public string TênChương { get; set; }
        public string MãNhómChính { get; set; }
        public string MainGroupNameI { get; set; }
        public string TênNhómChính { get; set; }
        public string MãLoại { get; set; }
        public string TypeName { get; set; }
        public string TênLoại { get; set; }
        public string DiseaseName { get; set; }
        public string TênBệnh { get; set; }
        public string MãNhómBáoCáoBộYTế { get; set; }
        public string MãNhómCầnChiTiếtHơn { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
