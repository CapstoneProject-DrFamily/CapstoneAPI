using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Helper
{
    public class Constants
    {
        public struct Roles
        {
            public const int ROLE_ADMIN_ID = 0;
            public const int ROLE_PATIENT_ID = 1;
            public const int ROLE_DOCTOR_ID = 2;
            public const string ROLE_ADMIN = "Admin";
            public const string ROLE_PATIENT = "Patient";
            public const string ROLE_DOCTOR = "Doctor";
        }
    }
}
