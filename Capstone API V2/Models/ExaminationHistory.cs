using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Capstone_API_V2.Models
{
    public partial class ExaminationHistory
    {
        public string Id { get; set; }
        public string History { get; set; }
        public double? PulseRate { get; set; }
        public double? Temperature { get; set; }
        public string BloodPressure { get; set; }
        public double? RespiratoryRate { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? WaistCircumference { get; set; }
        public double? RightEye { get; set; }
        public double? LeftEye { get; set; }
        public double? RightEyeGlassed { get; set; }
        public double? LeftEyeGlassed { get; set; }
        public string Mucosa { get; set; }
        public string OtherBody { get; set; }
        public string Cardiovascular { get; set; }
        public string Respiratory { get; set; }
        public string Gastroenterology { get; set; }
        public string Nephrology { get; set; }
        public string Rheumatology { get; set; }
        public string Endocrine { get; set; }
        public string Nerve { get; set; }
        public string Mental { get; set; }
        public string Surgery { get; set; }
        public string ObstetricsGynecology { get; set; }
        public string Otorhinolaryngology { get; set; }
        public string OdontoStomatology { get; set; }
        public string Ophthalmology { get; set; }
        public string Dermatology { get; set; }
        public string Nutrition { get; set; }
        public string Activity { get; set; }
        public string Evaluation { get; set; }
        public string BloodTest { get; set; }
        public string UrineTest { get; set; }
        public string Conclusion { get; set; }
        public string Advisory { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual Treatment IdNavigation { get; set; }
    }
}
