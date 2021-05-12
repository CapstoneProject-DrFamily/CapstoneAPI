using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class PatientModel
    {
        public int PatientId { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Image { get; set; }
        public string IdCard { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string BloodType { get; set; }
        public bool Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public string Location { get; set; }
        public string Relationship { get; set; }
        //public int AccountId { get; set; }
        public ProfileModel PatientNavigation { get; set; }

    }
}
