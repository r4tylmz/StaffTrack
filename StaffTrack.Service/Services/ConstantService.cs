using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.Services;
using StaffTrack.Core.UnitOfWorks;

namespace StaffTrack.Service.Services
{
    public class ConstantService:Service<Constant>,IConstantService
    {
        public ConstantService(IRepository<Constant> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}