using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DoctorRequestModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialty { get; set; }
        public string DoctorImage { get; set; }
        public int DoctorServiceId { get; set; }
        public double? RatingPoint { get; set; }
        public int BookedCount { get; set; }
        public int FeedbackCount { get; set; }
    }
}
