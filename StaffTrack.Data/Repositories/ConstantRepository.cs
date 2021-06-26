using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;

namespace StaffTrack.Data.Repositories
{
    public class ConstantRepository:Repository<Constant>,IConstantRepository
    {
        private AppDbContext _appDbContext => _context as AppDbContext;

        public ConstantRepository(AppDbContext context) : base(context)
        {
        }
    }
}