using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class FeedbackModel
    {
        public string Id { get; set; }
        public double? RatingPoint { get; set; }
        public string Note { get; set; }
        public string InsBy { get; set; }
        public string UpdBy { get; set; }
    }
}
