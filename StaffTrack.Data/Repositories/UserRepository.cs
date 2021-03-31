using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;

namespace StaffTrack.Data.Repositories
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        private AppDbContext _appDbContext => _context as AppDbContext;

        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}