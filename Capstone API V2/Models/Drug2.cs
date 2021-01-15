using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Drug2
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ManufacturerName { get; set; }
    }
}
