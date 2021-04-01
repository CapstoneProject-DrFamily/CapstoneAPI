using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DiseaseModel
    {
        public string DiseaseCode { get; set; }
        public string ChapterCode { get; set; }
        public string ChapterName { get; set; }
        public string MainGroupCode { get; set; }
        public string MainGroupName { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string DiseaseName { get; set; }
    }
}
