using Capstone_API_V2.ViewModels.SimpleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.Repositories
{
    public interface IScheduleRepository
    {
        bool checkInvalidSchedule(ScheduleSimpModel schedule);
    }
}
