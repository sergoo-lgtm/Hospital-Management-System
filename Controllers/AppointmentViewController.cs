using HospitalManagementSystemAPIVersion.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentViewController : ControllerBase
    {
        private readonly AppointmentViewService _service;

        public AppointmentViewController(AppointmentViewService service)
        {
            _service = service;
        }

        [HttpGet("details")]
        public IActionResult GetAppointmentDetails()
        {
            var result = _service.GetAppointmentDetails();
            return Ok(result);
        }
    }
        
        
    }

