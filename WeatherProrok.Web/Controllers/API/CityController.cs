using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WeatherProrok.Logic.Processors;
using WeatherProrok.Web.Helpers;
using WeatherProrok.Web.Store;
using WeatherProrok.Web.Hubs;
using WeatherProrok.Web.ViewModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherProrok.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        IWeatherProcessor processor = new WeatherProcessor(new Logic.Providers.GismeteoProvider(), new ForecastProcessor(), new FactWeatherProcessor());
        ISearchCityStore searchCityStore;
        IFactWeatherProcessor factProcessor;

        public CityController(ISearchCityStore searchCityStore, IFactWeatherProcessor factProcessor)
        {
            this.searchCityStore = searchCityStore;
            this.factProcessor = factProcessor;
        }

        // POST: api/city/search
        [HttpPost("search")]
        public JsonResult PostSearch(string value)
        {
            var result = processor.SearchCity(value);
            searchCityStore.AddToStore(result);
            return Json(result.Select(x => x.FullName).ToArray());
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
            var city = searchCityStore.GetFromStore(x => x.FullName == value).FirstOrDefault();
            if (city != null)
            {
                processor.AddCity(city.Id, city.Name);
                searchCityStore.ClearStore();

                var connectionManager = SignalRConnectionManager.ConnectionManager;
                if (connectionManager != null)
                {
                    var weatherHub = connectionManager.GetHubContext<Weather>();
                    var model = (processor as IWeatherProcessorForScheduler).GetWeather().Select(x => WeatherEntityViewModel.ToWeatherEntityViewModel(x));
                    weatherHub.Clients.All.updateCurrentWeather(model);
                }
            }
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
