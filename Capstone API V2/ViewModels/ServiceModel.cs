using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool? Disabled { get; set; }
        public int? SpecialtyId { get; set; }
        public bool? IsDefault { get; set; }

    }
}
