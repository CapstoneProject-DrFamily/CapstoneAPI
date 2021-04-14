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
            public const int PATIENT_APP_ID = 1;
            public const int DOCTOR_APP_ID = 2;
        }

        public struct TransactionStatus
        {
            public const int OPEN = 0;
            public const int ONGOING = 1;
            public const int CHECKING = 2;
            public const int DONE = 3;
            public const int CANCEL = 4;
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

        public struct Format
        {
            public const string DATETIME = "yyyy-MM-ddTHH:mm:ss.fffffff";
            public const string VN_TIMEZONE_ID = "SE Asia Standard Time";
        }

        public struct EmailConfig
        {
            public const string API_KEY = "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";
            public const string FROM_EMAIL = "taitpse130083@fpt.edu.vn";
            public const string FROM_NAME = "Family Doctor";
            public const string ACCEPTED_TEMPLATE_ID = "d-6fcc2d8f17f54a12927dddfe30c8776b";
            public const string REJECTED_TEMPLATE_ID = "d-b1ad1985ef234811b16177cb06235c9d";
        }

        public struct SMSConfig
        {
            public const string USERNAME = "ACc6d43f1fee15a96bfc35652a7bbc237e";
            public const string PASSWORD = "4774c2b01896df63b4b1812a9bf34ba9";
            public const string BODY_TEMPLATE = "Hello doctor, you have new booking appointment at";
            public const string SERVER_NUMER = "+19036907045";
            public const string VN_REGION_CODE = "+84";
        }
    }
}
