using System;

namespace StaffTrack.Core.Entities
{
    public class Constant
    {
        public int Id { get; set; }
        public string WorkingHourStart { get; set; }
        public string WorkingHourEnd { get; set; }

        public short WorkingHourPerWeek { get; set; }
        public int HourlyWage { get; set; }
        
    }
}