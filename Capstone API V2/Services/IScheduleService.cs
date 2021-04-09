using Capstone_API_V2.Models;
using Capstone_API_V2.ViewModels;
using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Services
{
    public interface IScheduleService : IBaseService<Schedule, ScheduleModel>
    {
        Task<List<ScheduleSimpModel>> CreateScheduleAsync(List<ScheduleSimpModel> dto);
        Task<ScheduleSimpModel> UpdateScheduleAsync(ScheduleSimpModel dto);
        bool checkIsOldPatient(int doctorId, int patientId);
        string GetPhoneNumber(int? doctorId);
        Task<CheckingSchedule> isCheckingTransaction(int doctorId);
    }
}
