using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;

namespace WeatherProrok.Logic.Helpers
{
    class GismeteoCloudsAndPrecipitationMappingHelper
    {
        public static Cloudity MapCloudity(string cloudity)
        {
            switch(cloudity)
            {
                case "Малооблачно":
                    return Cloudity.LESS_CLOUD;
                case "Пасмурно":
                    return Cloudity.CLOUD;
                case "Ясно":
                    return Cloudity.NONE;
                default:
                    return Cloudity.UNKNOWN;
            }
        }

        public static Precipitation MapPrecipitation(string precipitation)
        {
            switch(precipitation)
            {
                case "":
                    return Precipitation.NONE;
                case "небольшой дождь":
                    return Precipitation.LESS_RAIN;
                case "дождь":
                    return Precipitation.RAIN;
                case "сильный дождь":
                    return Precipitation.HARD_RAIN;
                case "небольшой снег":
                    return Precipitation.LESS_SNOW;
                case "снег":
                    return Precipitation.SNOW;
                case "сильный снег":
                    return Precipitation.HARD_SNOW;
                default:
                    return Precipitation.OTHER;
            }
        }
    }
}
