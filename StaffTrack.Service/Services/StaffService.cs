using System.Threading.Tasks;
using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.Services;
using StaffTrack.Core.UnitOfWorks;

namespace StaffTrack.Service.Services
{
    public class StaffService:Service<Staff>,IStaffService
    {
        public StaffService(IRepository<Staff> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public async Task<bool> CheckStaffPresence(int id)
        {
            return await UnitOfWork.StaffRepository.CheckStaffPresence(id);
        }
    }
}