using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;

namespace StaffTrack.Data.Repositories
{
    public class StaffActivityRepository:Repository<StaffActivity>,IStaffActivityRepository
    {        
        private AppDbContext _appDbContext => _context as AppDbContext;
        public StaffActivityRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<StaffActivity> GetAllActivitiesById(int id)
        {
            var activities = Where(x=> x.StaffId == id).Result.ToList();
            return activities;
        }
    }
}