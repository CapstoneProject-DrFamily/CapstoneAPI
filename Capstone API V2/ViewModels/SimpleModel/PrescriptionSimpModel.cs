using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class PrescriptionSimpModel
    {
        public int PrescriptionId { get; set; }
        public string Description { get; set; }
        public string InsBy { get; set; }
        public string UpdBy { get; set; }
        public ICollection<PrescriptionDetailModel> PrescriptionDetails { get; set; }
    }
}
