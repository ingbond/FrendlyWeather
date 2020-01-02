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
    public class GoogleServiceAPI : IGoogleServiceAPI
    {
        private readonly IConfiguration _configuration;

        public GoogleServiceAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleResponse> GetTimezoneNameAsync(float lat, float lon, int dt)
        {
            var key = _configuration.GetSection("Google").GetSection("GoogleApiKey").Value;
            var latStr = lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var lonStr = lon.ToString(System.Globalization.CultureInfo.InvariantCulture);

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("Google").GetSection("GoogleApiURL").Value);
                    var response = await client.GetAsync($"/maps/api/timezone/json?location={latStr},{lonStr}&timestamp={dt}&language=es&key={key}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    GoogleResponse googleResult = JsonConvert.DeserializeObject<GoogleResponse>(stringResult);

                    return googleResult;
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new Exception("Error on google call", httpRequestException);
                }
            }
        }
    }
}
