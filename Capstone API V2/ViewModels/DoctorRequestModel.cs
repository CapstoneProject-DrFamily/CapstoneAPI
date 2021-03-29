using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DoctorRequestModel
    {
        public int DoctorId { get; set; }
        public String DoctorName { get; set; }
        public String DoctorSpecialty { get; set; }
        public String DoctorImage { get; set; }
        public int DoctorServiceId { get; set; }


    }
}
