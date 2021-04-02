using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class AppConfigModel
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public string ConfigValue { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
    }
}
