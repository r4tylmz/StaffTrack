using System.Threading.Tasks;
using StaffTrack.Core.Entities;

namespace StaffTrack.Core.Repositories
{
    public interface IStaffRepository:IRepository<Staff>
    {
        Task<bool>CheckStaffPresence(int id);
    }
}