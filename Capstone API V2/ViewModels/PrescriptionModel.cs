using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class PrescriptionModel
    {
        public string PrescriptionId { get; set; }
        public string Description { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public TransactionModel PrescriptionNavigation { get; set; }
        public ICollection<PrescriptionDetailModel> PrescriptionDetails { get; set; }
    }
}
