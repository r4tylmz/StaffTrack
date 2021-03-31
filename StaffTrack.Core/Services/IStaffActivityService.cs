using System.Collections.Generic;
using System.Threading.Tasks;
using StaffTrack.Core.Entities;

namespace StaffTrack.Core.Services
{
    public interface IStaffActivityService:IService<StaffActivity>
    {
        IEnumerable<StaffActivity> GetAllActivitiesById(int id);
    }
}