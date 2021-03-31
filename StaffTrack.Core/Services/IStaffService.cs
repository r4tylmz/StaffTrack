using System.Threading.Tasks;
using StaffTrack.Core.Entities;

namespace StaffTrack.Core.Services
{
    public interface IStaffService:IService<Staff>
    {
        Task<bool>CheckStaffPresence(int id);
    }
}