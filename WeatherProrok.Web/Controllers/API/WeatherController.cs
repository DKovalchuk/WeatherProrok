using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WeatherProrok.Web.ViewModel;
using WeatherProrok.Logic.Processors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherProrok.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        IWeatherProcessorForScheduler processor;
        IFactWeatherProcessor factProcessor;
        IForecastProcessor forecastProcessor;

        public WeatherController(IWeatherProcessorForScheduler processor, IFactWeatherProcessor factProcessor, IForecastProcessor forecastProcessor)
        {
            this.processor = processor;
            this.factProcessor = factProcessor;
            this.forecastProcessor = forecastProcessor;
        }

        // GET: api/weather/current
        [HttpGet("current")]
        public JsonResult GetCurrent()
        {
            var list = processor.GetWeather().Select(x => WeatherEntityViewModel.ToWeatherEntityViewModel(x));
            return Json(list);
        }

        // GET api/weateher/fact
        [HttpGet("fact")]
        public JsonResult GetFact()
        {
            var factData = factProcessor.GetCurrentWeatherForAllCities().Select(x => CurrentWeatherViewModel.ToCurrentWeatherViewModel(x));
            return Json(factData);
        }

        // GET api/weather/forecast
        [HttpGet("forecast")]
        public JsonResult GetForecast()
        {
            var forecastData = forecastProcessor.GetForecastForAllCities();
            return Json(forecastData);
        } 

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
