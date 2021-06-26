using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StaffTrack.Core.Entities;
using StaffTrack.Core.Services;

namespace StaffTrack.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staffs = await _staffService.GetAllAsync();
            return Ok(staffs.Reverse());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);
            return Ok(staff);
        }

        [HttpGet("check/{id}")]
        public async Task<IActionResult> CheckPresence(int id)
        {
            var isStaffExist = await _staffService.CheckStaffPresence(id);
            return Ok(isStaffExist);
        }
        
        [HttpPost]
        public async Task<IActionResult> Save(Staff staff)
        {
            var newStaff = await _staffService.AddAsync(staff);
            return Created(string.Empty, newStaff);
        }

        [HttpPut]
        public IActionResult Update(Staff staff)
        {
            var _ = _staffService.Update(staff);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Remove(int id)
        {
            var staff = _staffService.GetByIdAsync(id).Result;
            _staffService.Remove(staff);
            return NoContent();
        }
    }
}