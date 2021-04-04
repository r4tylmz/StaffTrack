using System.Collections.Generic;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _userService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(admins));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var admin = await _userService.GetByIdAsync(id);
            return Ok(_mapper.Map<UserDto>(admin));
        }
        
        [HttpPost]
        public async Task<IActionResult> Save(User user)
        {
            var newAdmin = await _userService.AddAsync(user);
            return Created(string.Empty, _mapper.Map<UserDto>(newAdmin));
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            var admin = await _userService.SingleOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            if (admin == null)
            {
                return NotFound("Bulunamadi");
            }
            return Ok(_mapper.Map<UserDto>(admin));
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            var _ = _userService.Update(user);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult Remove(int id)
        {
            var admin = _userService.GetByIdAsync(id).Result;
            _userService.Remove(admin);
            return NoContent();
        }
    }
}