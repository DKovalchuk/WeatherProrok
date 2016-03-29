using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Web
{
    /// <summary>
    /// Store SignalR connection manager to use it in code outside the controllers
    /// </summary>
    public class SignalRConnectionManager
    {
        static IConnectionManager connectionManager;
        public static IConnectionManager ConnectionManager
        {
            get { return connectionManager; }
        }

        public static void Create(IConnectionManager manager)
        {
            connectionManager = manager;
        }
    }
}
