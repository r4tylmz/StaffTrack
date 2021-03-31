using System.Collections.Generic;
using System.Threading.Tasks;
using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.Services;
using StaffTrack.Core.UnitOfWorks;

namespace StaffTrack.Service.Services
{
    public class StaffActivityService:Service<StaffActivity>,IStaffActivityService
    {
        public StaffActivityService(IRepository<StaffActivity> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public IEnumerable<StaffActivity> GetAllActivitiesById(int id)
        {
            return UnitOfWork.StaffActivityRepository.GetAllActivitiesById(id);
        }
    }
}