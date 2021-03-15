using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class SpecialtyModel 
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ServiceModel> Services { get; set; }
    }
}
