using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StaffTrack.Core.Entities;
using StaffTrack.Core.Services;
using StaffTrack.WebAPI.DTOs;

namespace StaffTrack.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffActivityController:ControllerBase
    {
        private readonly IStaffActivityService _staffActivityService;
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        public StaffActivityController(IStaffActivityService staffActivityService, IMapper mapper, IStaffService staffService)
        {
            _staffActivityService = staffActivityService;
            _mapper = mapper;
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _staffActivityService.GetAllAsync();
            var staffs = await _staffService.GetAllAsync();
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
            return Ok(staffActivities);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllActivitiesById(int id)
        {
            var activities = _staffActivityService.GetAllActivitiesById(id);
            var staffs = await _staffService.GetAllAsync();
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
            return Ok(staffActivities);
        }

        [HttpGet("{id}/loyality")]
        public IActionResult GetLoyalityById(int id)
        {
            var activities = _staffActivityService.GetAllActivitiesById(id);
            double totalSeconds = 0;
            foreach (var item in activities)
            {
                totalSeconds += item.ExitTime.Subtract(item.EntryTime).TotalSeconds;
            }
            return Ok((int)totalSeconds);
        }

        [HttpPost]
        public async Task<IActionResult> Save(StaffActivity activity)
        {
            var newActivity = await _staffActivityService.AddAsync(activity);
            return Created(string.Empty,newActivity);
        }
        
        [HttpPut]
        public IActionResult Update(StaffActivity activity)
        {
            var _ = _staffActivityService.Update(activity);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Remove(int id)
        {
            var activity = _staffActivityService.GetByIdAsync(id).Result;
            _staffActivityService.Remove(activity);
            return NoContent();
        }
    }
}