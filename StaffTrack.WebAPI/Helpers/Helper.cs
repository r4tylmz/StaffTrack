using System;
using System.Collections.Generic;
using StaffTrack.Core.Entities;

namespace StaffTrack.WebAPI.Helpers
{
    public static class Helper
    {
        public static double CalculateTotalMinutes(IEnumerable<StaffActivity> list)
        {
            var minutes = 0.0;
            foreach (var item in list)
            {
                minutes += item.ExitTime.Subtract(item.EntryTime).TotalSeconds;
            }
            return Convert.ToInt32(minutes);
        }
        
    }
}