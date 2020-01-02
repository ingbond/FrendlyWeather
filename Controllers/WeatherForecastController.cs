using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrendlyWeather.APIServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrendlyWeather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOpenWeatherMapAPIService _weatherMapApi;
        private readonly IGoogleServiceAPI _googleServiceApi;

        public WeatherForecastController(IOpenWeatherMapAPIService weatherMapApi, IGoogleServiceAPI googleServiceApi)
        {  
            _weatherMapApi = weatherMapApi;
            _googleServiceApi = googleServiceApi;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string zip)
        {
            var resultWeather = await _weatherMapApi.GetWeatherForLocation(zip);
            var resultGoogle =
                await _googleServiceApi.GetTimezoneName(resultWeather.Coord.Lat + 10, resultWeather.Coord.Lon, resultWeather.Dt);

            if (resultWeather.Name == null || resultGoogle.TimeZoneId == null)
            {
                return NotFound();
            }

            var result =
                $"At the location {resultWeather.Name}, the temperature is {resultWeather.Main.Temp}, and the timezone is {resultGoogle.TimeZoneId}";

            return Ok(result);
        }
    }
}
