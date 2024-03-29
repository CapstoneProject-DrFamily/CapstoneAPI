﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class Treatment
    {
        public string Id { get; set; }
        public string EstimatedTime { get; set; }
        public byte? Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public string ReasonCancel { get; set; }
        public int? ServiceId { get; set; }
        public int? ScheduleId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateStart { get; set; }
        public bool? Disabled { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Service Service { get; set; }
        public virtual ExaminationHistory ExaminationHistory { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
