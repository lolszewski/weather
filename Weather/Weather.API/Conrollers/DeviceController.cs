using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Weather.API.Conrollers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DeviceController : ControllerBase
    {
        [Route("{deviceId}/data/{date}/{sensorType}")]
        [HttpGet]
        public  async Task<IActionResult> GetData(string deviceId ,DateTime date, string sensorType )
        {
            return Ok("result");
        }

        [Route("{deviceId}/data/{date}")]
        [HttpGet]
        public async Task<IActionResult> GetDataForDevice(string deviceId, DateTime date)
        {
            return Ok("result");
        }
    }
}
