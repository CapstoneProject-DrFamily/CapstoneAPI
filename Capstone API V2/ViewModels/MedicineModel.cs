using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class MedicineModel : ResourceParameter
    {
        public string Name { get; set; }
        public int DrugId { get; set; }
    }
}
