using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrendlyWeather.Responses;

namespace FrendlyWeather.APIServices.Contracts
{
    public interface IOpenWeatherMapAPIService
    {
        Task<OpenWeatherResponse> GetWeatherForLocation(string zip);
    }
}
