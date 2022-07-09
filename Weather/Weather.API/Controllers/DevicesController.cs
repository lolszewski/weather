using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Business.Managers;
using Weather.Contracts.Resources;

namespace Weather.API.Controllers
{
    /// <summary>
    /// Weather data items API
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class DevicesController : ControllerBase
    {
        private readonly IWeatherDataItemsManager _manager;

        public DevicesController(IWeatherDataItemsManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Gets data for given device and sensor.
        /// If there is no data for device and/or sensor returns an empty list.
        /// </summary>
        /// <param name="deviceId">Device identifier</param>
        /// <param name="sensorType">Sensor type - i.e. temperature</param>
        /// <param name="date">Date time - only year, month and day is included in data selection - i. e. 2019-01-10</param>
        /// <param name="skip">Number of data items to skip (0 by default)</param>
        /// <param name="take">Number of data items to take (100 by default)</param>
        /// <response code="500">Server error - when infrastructure is not responding</response>
        /// <returns>List containing weather data items for given device, sensor and date</returns>
        [HttpGet]
        [Route("{deviceId}/data/{date}/{sensorType}", Name = "GetDataForDeviceAndSensor")]
        [ProducesResponseType(typeof(IEnumerable<WeatherDataItemResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDataForDeviceAndSensor(
            [Required(AllowEmptyStrings = false)] string deviceId, 
            [Required(AllowEmptyStrings = false)] string sensorType, 
            [Required] DateTime date,
            [Range(0, int.MaxValue)] int skip = 0,
            [Range(1, 500)] int take = 100)
        {
            return Ok(await _manager.GetData(deviceId, sensorType, new DateOnly(date.Year, date.Month, date.Day), skip, take));
        }

        /// <summary>
        /// Gets data for given device.
        /// If there is no data for device returns an empty list.
        /// </summary>
        /// <param name="deviceId">Device identifier</param>
        /// <param name="date">Date time - only year, month and day is included in data selection - i. e. 2019-01-10</param>
        /// <param name="skip">Number of data items to skip for each sensor (0 by default)</param>
        /// <param name="take">Number of data items to take for each sensor (100 by default)</param>
        /// <response code="500">Server error - when infrastructure is not responding</response>
        /// <returns>List containing weather data items for given device and date</returns>
        [HttpGet]
        [Route("{deviceId}/data/{date}", Name = "GetDataForDevice")]
        [ProducesResponseType(typeof(IEnumerable<WeatherDataItemResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDataForDevice(
            [Required(AllowEmptyStrings = false)] string deviceId,
            [Required] DateTime date,
            [Range(0, int.MaxValue)] int skip = 0,
            [Range(1, 500)] int take = 100)
        {
            return Ok(await _manager.GetData(deviceId, new DateOnly(date.Year, date.Month, date.Day), skip, take));
        }
    }
}
