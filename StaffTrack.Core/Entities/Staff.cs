using System;
using System.Collections.Generic;

namespace StaffTrack.Core.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<StaffActivity> StaffActivities;
    }
}