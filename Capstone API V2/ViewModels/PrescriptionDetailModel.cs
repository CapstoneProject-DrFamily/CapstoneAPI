using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class PrescriptionDetailModel
    {
        public int PrescriptionDetailId { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int QuantityPerTime { get; set; }
        public string Method { get; set; }
        public int? TimesPerDay { get; set; }
        public int? NumberOfTime { get; set; }
        public MedicineModel Medicine { get; set; }

    }
}
