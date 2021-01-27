using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class MedicineModel : BaseModel
    {
        public int MedicineId { get; set; }
        public string Form { get; set; }
        public string Strength { get; set; }
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
        /*public bool? Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }*/
    }
}
