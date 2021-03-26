using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class ScheduleModel
    {
        public string ScheduleId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public bool? Status { get; set; }
        public string InsBy { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public TransactionPutModel ScheduleNavigation { get; set; }

    }
}
