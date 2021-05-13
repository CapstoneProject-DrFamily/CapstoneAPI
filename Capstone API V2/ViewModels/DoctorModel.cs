using Capstone_API_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public string Gender { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Description { get; set; }
        public int SpecialtyId { get; set; }
        public SpecialtyModel Specialty { get; set; }
        public ICollection<ScheduleModel> Schedules { get; set; }
        /*public ICollection<FeedbackModel> Feedbacks { get; set; }
        public ICollection<TransactionModel> Transactions { get; set; }*/

        public string School { get; set; }
        public bool Disabled { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }
        public double? RatingPoint { get; set; }
        public int BookedCount { get; set; }
        public int FeedbackCount { get; set; }
        public virtual Account IdNavigation { get; set; }

    }
}
