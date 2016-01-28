using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WeatherProrok.Logic.Processors;
using WeatherProrok.Logic.Repositories;
using WeatherProrok.Web.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherProrok.Web.Controllers
{
    public class WeatherController : Controller
    {
        IFactWeatherProcessor processor;
        ICityRepository cityRepo;

        public WeatherController(IFactWeatherProcessor processor, ICityRepository cityRepo)
        {
            this.processor = processor;
            this.cityRepo = cityRepo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult City(Guid id)
        {
            var city = CityViewModel.ToCityViewModel(cityRepo.GetCity(id));
            var weather = processor.GetWeatherForCity(id).OrderByDescending(x => x.CurrentDateTime).Select(x => CurrentWeatherViewModel.ToCurrentWeatherViewModel(x));

            var model = new CityWeatherDetailsViewModel
            {
                City = city,
                Weather = weather
            };

            return View(model);
        }
    }
}
