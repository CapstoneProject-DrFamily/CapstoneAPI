using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class TreatmentPutModel
    {
        public string Id { get; set; }
        public int DoctorId { get; set; }
        public int? PatientId { get; set; }
        public string EstimatedTime { get; set; }
        public byte Status { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public int? ScheduleId { get; set; }
        public string ReasonCancel { get; set; }
    }
}
