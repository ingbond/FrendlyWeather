using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrendlyWeather.APIServices.Contracts;
using FrendlyWeather.Exceptions;
using FrendlyWeather.Services.Contracts;

namespace FrendlyWeather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapAPIService _weatherMapApi;
        private readonly IGoogleServiceAPI _googleServiceApi;

        public WeatherService(IOpenWeatherMapAPIService weatherMapApi, IGoogleServiceAPI googleServiceApi)
        {
            _weatherMapApi = weatherMapApi;
            _googleServiceApi = googleServiceApi;
        }

        public async Task<string> GetFrendlyWeatherAsync(string zip)
        {
            var resultWeather = await _weatherMapApi.GetWeatherForLocationAsync(zip);
            var resultGoogle =
                await _googleServiceApi.GetTimezoneNameAsync(resultWeather.Coord.Lat + 10, resultWeather.Coord.Lon, resultWeather.Dt);

            if (resultWeather.Name == null || resultGoogle.TimeZoneId == null)
            {
                throw new NotFountException("No result");
            }

            var result =
                $"At the location {resultWeather.Name}, the temperature is {resultWeather.Main.Temp}, and the timezone is {resultGoogle.TimeZoneId}";

            return result;
        }
    }
}
