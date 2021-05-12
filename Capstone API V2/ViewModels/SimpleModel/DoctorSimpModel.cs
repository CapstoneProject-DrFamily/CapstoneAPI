using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class DoctorSimpModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public string Gender { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Description { get; set; }
        public int SpecialtyId { get; set; }
        public string School { get; set; }
    }
}
