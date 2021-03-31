using System;

namespace StaffTrack.WebAPI.DTOs
{
    public class StaffActivityDto {
        public int StaffId { get; set; }
        public int RoomId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }

    }
}