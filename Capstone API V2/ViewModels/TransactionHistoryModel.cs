using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class TransactionHistoryModel
    {
        public string TransactionId { get; set; }
        //public int DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        //public string EstimatedTime { get; set; }
        public byte? Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Relationship { get; set; }
        public string ServiceName { get; set; }
        public decimal? ServicePrice { get; set; }
        public bool isOldPatient { get; set; }
        /*public bool Disabled { get; set; }
        public int ExamId { get; set; }
        public int ServiceId { get; set; }*/
    }
}
