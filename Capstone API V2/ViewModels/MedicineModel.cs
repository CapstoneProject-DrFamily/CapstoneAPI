using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class MedicineModel : BaseModel
    {
        public int Id { get; set; }
        public string Form { get; set; }
        public string Strength { get; set; }
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
    }
}
