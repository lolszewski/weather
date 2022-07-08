using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Business.Managers;
using Weather.Contracts.Resources;

namespace Weather.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DevicesController : ControllerBase
    {
        private readonly IWeatherDataItemsManager _manager;

        public DevicesController(IWeatherDataItemsManager manager)
        {
            _manager = manager;
        }

        [Route("{deviceId}/data/{date}/{sensorType}")]
        [HttpGet]
        [Produces(typeof(IEnumerable<WeatherDataItemResource>))]
        public async Task<IActionResult> GetData(
            [Required(AllowEmptyStrings = false)] string deviceId, 
            [Required(AllowEmptyStrings = false)] string sensorType, 
            [Required] DateTime date)
        {
            return Ok(await _manager.GetData(deviceId, sensorType, new DateOnly(date.Year, date.Month, date.Day)));
        }

        [Route("{deviceId}/data/{date}")]
        [HttpGet]
        [Produces(typeof(IEnumerable<WeatherDataItemResource>))]
        public async Task<IActionResult> GetDataForDevice(
            [Required(AllowEmptyStrings = false)] string deviceId,
            [Required] DateTime date)
        {
            return Ok(await _manager.GetData(deviceId, new DateOnly(date.Year, date.Month, date.Day)));
        }
    }
}
