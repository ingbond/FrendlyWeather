using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrendlyWeather.Services.Contracts
{
    public interface IWeatherService
    {
        Task<string> GetFrendlyWeatherAsync(string zip);
    }
}
