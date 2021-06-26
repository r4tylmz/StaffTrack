using System;
using StaffTrack.Core.Services;

namespace StaffTrack.WebAPI.Helpers
{
    public class WorkingTime
    {
        private IConstantService _constantService;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public WorkingTime(IConstantService constantService)
        {
            _constantService = constantService;
            var constant = _constantService.GetByIdAsync(1).Result;
            var workingHourStart = constant.WorkingHourStart.Split(":");
            var workingHourEnd = constant.WorkingHourEnd.Split(":");
            
            
            var startHours = int.Parse(workingHourStart[0]);
            var startMinutes = int.Parse(workingHourStart[1]);
            var endHours = int.Parse(workingHourEnd[0]);
            var endMinutes = int.Parse(workingHourEnd[1]);
            
            
            StartTime = new TimeSpan(startHours, startMinutes, 0);
            EndTime = new TimeSpan(endHours, endMinutes, 0);
        }
    }
}