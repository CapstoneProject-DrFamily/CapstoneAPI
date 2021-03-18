using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class FeedbackSimpModel
    {
        public string FeedbackId { get; set; }
        public double? RatingPoint { get; set; }
        public string Note { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
    }
}
