using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrendlyWeather.Exceptions
{
    public class NotFountException : Exception
    {
        public NotFountException(string msg) : base(msg)
        {

        }
    }
}
