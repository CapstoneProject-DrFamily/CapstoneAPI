using Capstone_API_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DoctorModel
    {
        public int DoctorId { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Description { get; set; }
        public int SpecialtyId { get; set; }
        public int ProfileId { get; set; }
        public SpecialtyModel Specialty { get; set; }
        public ProfileModel Profile { get; set; }
        public string School { get; set; }
        public bool Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
    }
}
