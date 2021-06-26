using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffTrack.Core.Services;
using StaffTrack.WebAPI.Helpers;

namespace StaffTrack.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private IConstantService _constantService;
        private IStaffService _staffService;
        private IStaffActivityService _staffActivityService;


        public WorkController(IConstantService constantService, IStaffService staffService, IStaffActivityService staffActivityService)
        {
            _constantService = constantService;
            _staffService = staffService;
            _staffActivityService = staffActivityService;
        }

        [HttpGet("{id}")]
        public IActionResult GetAllWorkingTimeById(int id)
        {
            var activities = _staffActivityService.GetAllActivitiesById(id);
            WorkingTime workingTime = new(_constantService);
            var workingDatesById = activities
                .Where(x => 
                    x.EntryTime.TimeOfDay >= workingTime.StartTime &&
                    x.EntryTime.TimeOfDay <= workingTime.EndTime &&
                    x.ExitTime.TimeOfDay <= workingTime.EndTime);
            
            return Ok(Helper.CalculateTotalMinutes(workingDatesById));
        }
        

        [HttpGet("wage/{id}")]
        public IActionResult GetWageById(int id)
        {
            var constant = _constantService.GetByIdAsync(1).Result;
            var activities = _staffActivityService.GetAllActivitiesById(id);
            var wage = constant.HourlyWage * Helper.CalculateTotalMinutes(activities);
            return Ok(wage);
        }
    }
}