using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class PatientSimpModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Image { get; set; }
        public string IdCard { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; }
        public string Relationship { get; set; }
        public string Location { get; set; }

        //public int AccountId { get; set; }
    }
}
