﻿using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DependentModel
    {
        public int PatientID { get; set; }
        public string DependentName { get; set; }
        public string DependentImage { get; set; }

        public string DependentRelationShip { get; set; }
        public int ProfileID { get; set; }
    }
}
