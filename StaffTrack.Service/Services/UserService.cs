using StaffTrack.Core.Entities;
using StaffTrack.Core.Repositories;
using StaffTrack.Core.Services;
using StaffTrack.Core.UnitOfWorks;

namespace StaffTrack.Service.Services
{
    public class UserService:Service<User>,IUserService
    {
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}