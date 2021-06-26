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
    public class ConstantController : ControllerBase
    {
        private IConstantService _constantService;
        private readonly IMapper _mapper;

        public ConstantController(IConstantService constantService, IMapper mapper)
        {
            _constantService = constantService;
            _mapper = mapper;
        }

        [HttpPut]
        public IActionResult Update(Constant constant)
        {
            var _ = _constantService.Update(constant);
            return NoContent();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var constants = await _constantService.GetAllAsync();
            var constant = constants.FirstOrDefault();
            return Ok(_mapper.Map<ConstantDto>(constant));
        }
    }
}