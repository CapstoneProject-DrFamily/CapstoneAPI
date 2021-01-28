using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class UserModel : BaseModel
    {
        //public int AccountId { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        public int RoleId { get; set; }
        /*public bool Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }*/
        public int? ProfileId { get; set; }
    }
}
