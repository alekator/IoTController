using Microsoft.AspNetCore.Mvc;
using IoTController.API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using IoTController.Shared;

namespace IoTController.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceDataController : ControllerBase
    {
        private readonly ILogger<DeviceDataController> _logger;
        private readonly IoTContext _context;

        public DeviceDataController(ILogger<DeviceDataController> logger, IoTContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeviceDataModel data)
        {
            if (data == null)
            {
                return BadRequest();
            }

            _logger.LogInformation("Получены данные от устройства {DeviceId} в {Timestamp}", data.DeviceId, data.Timestamp);

            _context.DeviceData.Add(data);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
