using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class TransactionSimpModel
    {
        public string TransactionId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int PrescriptionId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public int ExamId { get; set; }
        public int ServiceId { get; set; }
        public ICollection<SymptomDetailModel> SymptomDetails { get; set; }
    }
}
