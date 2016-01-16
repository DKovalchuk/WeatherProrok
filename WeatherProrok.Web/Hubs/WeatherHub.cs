using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WeatherProrok.Web.Hubs
{
    [HubName("weather")]
    public class WeatherHub : Hub
    {
        public void Update()
        {
            Clients.All.updateCurrentWeather();
        }
    }
}
