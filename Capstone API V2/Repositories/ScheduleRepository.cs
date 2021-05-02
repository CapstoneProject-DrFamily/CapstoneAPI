using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels.SimpleModel;

namespace Capstone_API_V2.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly FamilyDoctorContext _context;

        public ScheduleRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public bool checkInvalidSchedule(ScheduleSimpModel schedule, double examinationHour)
        {
            bool isInvalidSchedule = false;
            var lstSchedule = _context.Schedules.Where(s => s.Disabled == false && s.DoctorId == schedule.DoctorId && s.AppointmentTime.Value.Date == schedule.AppointmentTime.Value.Date ).ToList();
            if(lstSchedule.Count > 0)
            {
                if(_context.Schedules.Any(s => s.Disabled == false && s.DoctorId == schedule.DoctorId && s.AppointmentTime == schedule.AppointmentTime ))
                {
                    isInvalidSchedule = true;
                }

                var lowerSchedule = lstSchedule.Where(s => s.Disabled == false && s.DoctorId == schedule.DoctorId && s.AppointmentTime < schedule.AppointmentTime ).OrderByDescending(s => s.AppointmentTime).FirstOrDefault();
                if(lowerSchedule != null)
                {
                    var lowerBoundary = (schedule.AppointmentTime - lowerSchedule.AppointmentTime).Value.TotalHours;
                    if (lowerBoundary < examinationHour)
                    {
                        isInvalidSchedule = true;
                    }
                }

                var higherSchedule = lstSchedule.Where(s => s.Disabled == false && s.DoctorId == schedule.DoctorId && s.AppointmentTime > schedule.AppointmentTime ).OrderBy(s => s.AppointmentTime).FirstOrDefault();
                if (higherSchedule != null)
                {
                    var higherBoundary = (higherSchedule.AppointmentTime - schedule.AppointmentTime).Value.TotalHours;
                    if (higherBoundary < examinationHour)
                    {
                        isInvalidSchedule = true;
                    }
                }

            }
            return isInvalidSchedule;
        }
    }
}
