using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WeatherProrok.Web.ViewModel;

namespace WeatherProrok.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var list = new List<WeatherEntityViewModel>
            {
                new WeatherEntityViewModel { Id = Guid.NewGuid(), Name = "Some city", Cloudity = "clouds", CurrentDateTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm"), Humidity = 40, Precipitation = "rain", Temp = 5 }
            };

            return View(list);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
