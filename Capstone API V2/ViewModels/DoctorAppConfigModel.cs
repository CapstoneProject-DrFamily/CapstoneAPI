﻿using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DoctorAppConfigModel
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public int Timeout { get; set; }
        public Dictionary<string, PrescriptionSimpModel> PrescriptionTemplates { get; set; }
        public string Policy { get; set; }
        public int AppointmentNotifyTime { get; set; }
        public int ImageQuantity { get; set; }
        public int MinusTimeInMinute { get; set; }
        public double ExaminationHour { get; set; }
    }
}
