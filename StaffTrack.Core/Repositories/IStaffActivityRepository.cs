using System.Collections.Generic;
using System.Threading.Tasks;
using StaffTrack.Core.Entities;

namespace StaffTrack.Core.Repositories
{
    public interface IStaffActivityRepository:IRepository<StaffActivity>
    {
        IEnumerable<StaffActivity> GetAllActivitiesById(int id);
    }
}