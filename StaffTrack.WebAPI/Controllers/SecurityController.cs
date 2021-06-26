using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffTrack.Core.Services;
using StaffTrack.WebAPI.DTOs;
using StaffTrack.WebAPI.Helpers;

namespace StaffTrack.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IConstantService _constantService;
        private IStaffService _staffService;
        private IStaffActivityService _staffActivityService;

        public SecurityController(IStaffActivityService staffActivityService, IStaffService staffService, IConstantService constantService)
        {
            _staffActivityService = staffActivityService;
            _staffService = staffService;
            _constantService = constantService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllSecurityResults()
        {
            var activities =  await _staffActivityService.GetAllAsync();
            WorkingTime workingTime = new(_constantService);
            var all = activities
                .Where(x => 
                    x.EntryTime.TimeOfDay >= workingTime.StartTime &&
                    x.EntryTime.TimeOfDay <= workingTime.EndTime &&
                    x.ExitTime.TimeOfDay <= workingTime.EndTime);
            return Ok(all);
        }

        [HttpGet("{id}")]
        public IActionResult GetSecurityResultById(int id)
        {
            var activities =  _staffActivityService.GetAllActivitiesById(id);
            var staffs =  _staffService.GetAllAsync().Result;
            var staffActivities = activities.Join(staffs,
                activity=>activity.StaffId,
                staff=>staff.Id,
                (activity, staff) => new StaffActivityDto{
                    StaffId = staff.Id,
                    RoomId = activity.RoomId,
                    Name = staff.Name,
                    LastName = staff.LastName,
                    EntryTime = activity.EntryTime,
                    ExitTime = activity.ExitTime
                });
            WorkingTime workingTime = new(_constantService);
            var workingTimeById = staffActivities
                .Where(x => 
                    x.EntryTime.TimeOfDay <= workingTime.StartTime ||
                    x.EntryTime.TimeOfDay >= workingTime.EndTime
                    );
            return Ok(workingTimeById);
        }
    }
}