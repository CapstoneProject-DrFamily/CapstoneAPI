﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class ProfileSimpModel
    {
        public int ProfileId { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public int AccountId { get; set; }
    }
}
