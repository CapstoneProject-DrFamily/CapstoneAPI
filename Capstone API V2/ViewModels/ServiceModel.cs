using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }    
        public int? SpecialtyId { get; set; }
        public decimal ServicePrice { get; set; }
        public string Image { get; set; }
        public bool Disabled { get; set; }
        public bool? IsDefault { get; set; }
    }
}
