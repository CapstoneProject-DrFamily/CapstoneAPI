﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Symptom
    {
        public Symptom()
        {
            SymptomDetails = new HashSet<SymptomDetail>();
        }

        public int SymptomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool? Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual ICollection<SymptomDetail> SymptomDetails { get; set; }
    }
}
