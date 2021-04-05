using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class PatientAppConfigModel
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public List<string> RelationShips { get; set; }
        public List<int> Distances { get; set; }
    }
}
