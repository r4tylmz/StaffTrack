using System;

namespace StaffTrack.WebAPI.DTOs
{
    public class ConstantDto
    { 
        public string WorkingHourStart { get; set; }
        public string WorkingHourEnd { get; set; }

        public short WorkingHourPerWeek { get; set; }
        public int HourlyWage { get; set; }
    }
}