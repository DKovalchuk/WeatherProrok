using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Logic.Models
{
    public enum Cloudity
    {
        NONE,
        LESS_CLOUD,
        CLOUD,
        HARD_CLOUD,
        UNKNOWN
    }

    public enum Precipitation
    {
        NONE,
        LESS_RAIN,
        RAIN,
        HARD_RAIN,
        LESS_SNOW,
        SNOW,
        HARD_SNOW,
        OTHER
    }
}
