using System.Threading.Tasks;
using StaffTrack.Core.Repositories;

namespace StaffTrack.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IStaffRepository StaffRepository { get; }
        public IStaffActivityRepository StaffActivityRepository { get; }
        public IUserRepository UserRepository { get; }
        public IConstantRepository ConstantRepository { get; }
        Task CommitAsync();
        void Commit();
    }
}