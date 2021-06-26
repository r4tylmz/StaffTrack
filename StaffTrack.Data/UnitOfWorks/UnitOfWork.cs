using System.Threading.Tasks;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.UnitOfWorks;
using StaffTrack.Data.Repositories;

namespace StaffTrack.Data.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private AppDbContext _context;
        private StaffRepository _staffRepository;
        private UserRepository _userRepository;
        private StaffActivityRepository _staffActivityRepository;
        private ConstantRepository _constantRepository;
        
        
        public IStaffRepository StaffRepository => _staffRepository ?? new StaffRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IStaffActivityRepository StaffActivityRepository =>
            _staffActivityRepository ?? new StaffActivityRepository(_context);
        public IConstantRepository ConstantRepository => _constantRepository ?? new ConstantRepository(_context);
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}