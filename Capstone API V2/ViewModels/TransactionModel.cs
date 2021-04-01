using Capstone_API_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int PrescriptionId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string EstimatedTime { get; set; }
        public byte Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public bool Disabled { get; set; }
        public int ExamId { get; set; }
        public int ServiceId { get; set; }
        public bool isOldPatient { get; set; }
        public DoctorModel Doctor { get; set; }
        public ExaminationHistoryModel ExaminationHistory { get; set; }
        public PatientModel Patient { get; set; }
        public PrescriptionModel Prescription { get; set; }
        public ServiceModel Service { get; set; }
        public ICollection<SymptomDetailModel> SymptomDetails { get; set; }
    }
}
