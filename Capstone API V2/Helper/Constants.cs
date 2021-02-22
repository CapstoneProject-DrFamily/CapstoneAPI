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

        public struct TransactionStatus
        {
            public const int OPEN = 0;
            public const int PROCESS = 1;
            public const int CLOSE = 2;
        }

        public struct SearchValue
        {
            public const string DEFAULT_VALUE = "a";
            public const string DEFAULT_VALUE_NUMBER = "1";

        }

        public struct Relationship
        {
            public const string OWNER = "owner";
        }
    }
}
