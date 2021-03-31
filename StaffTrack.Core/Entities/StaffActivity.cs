using System;
using System.Collections.Generic;

namespace StaffTrack.Core.Entities
{
    public class StaffActivity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int StaffId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}