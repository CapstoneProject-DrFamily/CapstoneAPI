using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class TransactionPutModel
    {
        public string TransactionId { get; set; }
        public int DoctorId { get; set; }
        public int? PatientId { get; set; }
        //public int? PrescriptionId { get; set; }
        public string EstimatedTime { get; set; }
        public byte Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        //public int? ExamId { get; set; }
    }
}
