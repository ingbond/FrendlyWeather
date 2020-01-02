using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrendlyWeather.APIServices.Contracts;
using FrendlyWeather.Exceptions;
using FrendlyWeather.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrendlyWeather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherForecastController( IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string zip)
        {
            try
            {
                var result = _weatherService.GetFrendlyWeatherAsync(zip);

                return Ok(result);
            }
            catch (NotFountException e)
            {
                return NotFound(e);
            }
        }
    }
}
