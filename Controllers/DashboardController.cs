using HospitalManagementSystemAPIVersion.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        [HttpGet("counts")]
        public async Task<IActionResult> GetCounts()
        {
            var result = await _service.GetCountsAsync();
            return Ok(result);
        }
    }
}
