using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class PatientSimpModel
    {
        public int PatientId { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; }
        public string Relationship { get; set; }
        public string Location { get; set; }

        //public int AccountId { get; set; }
    }
}
