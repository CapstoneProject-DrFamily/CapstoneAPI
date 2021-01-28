using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class PatientModel
    {
        public int PatientId { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string BloodType { get; set; }
        public int ProfileId { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public int RecordId { get; set; }
        public string Relationship { get; set; }
    }
}
