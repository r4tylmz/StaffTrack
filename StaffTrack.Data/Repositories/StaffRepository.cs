using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;

namespace StaffTrack.Data.Repositories
{
    public class StaffRepository:Repository<Staff>,IStaffRepository
    {
        private AppDbContext _appDbContext => _context as AppDbContext;
        public StaffRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckStaffPresence(int id)
        {
            
            var check = await GetByIdAsync(id);
            if (check == null)
            {
                return false;
            }
            return true;
        }
    }
}