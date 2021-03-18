using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class PrescriptionDetailModel
    {
        public int PrescriptionDetailId { get; set; }
        public string PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int? MorningQuantity { get; set; }
        public string Method { get; set; }
        public int? NoonQuantity { get; set; }
        public int TotalQuantity { get; set; }
        public int? AfternoonQuantity { get; set; }
        public string Type { get; set; }
        public MedicineModel Medicine { get; set; }

    }
}
