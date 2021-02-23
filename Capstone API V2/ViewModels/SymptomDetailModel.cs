using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class SymptomDetailModel
    {
        public int SymptomDetailId { get; set; }
        public int SymptomId { get; set; }
        public string TransactionId { get; set; }
        public SymptomModel Symptom { get; set; }
    }
}
