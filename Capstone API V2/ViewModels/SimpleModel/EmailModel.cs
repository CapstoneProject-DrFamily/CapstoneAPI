﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class EmailModel
    {
        public UserModel UserModel { get; set; }
        public bool IsAcceptDoctor { get; set; }
        public string Reason { get; set; }
    }
}