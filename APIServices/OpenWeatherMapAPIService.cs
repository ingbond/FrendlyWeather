using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrendlyWeather.APIServices.Contracts;
using FrendlyWeather.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FrendlyWeather.APIServices
{
    public class OpenWeatherMapAPIService : IOpenWeatherMapAPIService
    {
        private readonly IConfiguration _configuration;

        public OpenWeatherMapAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<OpenWeatherResponse> GetWeatherForLocation(string zip)
        {
            var key = _configuration.GetSection("Weather").GetSection("WeatherApiKey").Value;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("Weather").GetSection("WeatherApiURL").Value);
                    var response = await client.GetAsync($"/data/2.5/weather?q={zip}&appid={key}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    OpenWeatherResponse rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);

                    return rawWeather;
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new Exception("Error on weather call", httpRequestException);
                }
            }
        }
    }
}
