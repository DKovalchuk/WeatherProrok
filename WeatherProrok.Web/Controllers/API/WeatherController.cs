using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WeatherProrok.Web.ViewModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherProrok.Web.Controllers.API
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        // GET: api/weather/current
        [HttpGet("current")]
        public JsonResult GetCurrent()
        {
            var list = new List<WeatherEntityViewModel>
            {
                new WeatherEntityViewModel { Id = Guid.NewGuid(), Name = "Some city", Cloudity = "clouds", CurrentDateTime = DateTime.Now.ToString("dd MMMM YYYY hh:mm"), Humidity = 40, Precipitation = "rain", Temp = 5 }
            };
            return Json(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
