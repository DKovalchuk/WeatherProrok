using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherProrok.Logic.Processors;
using WeatherProrok.Logic.Providers;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.DAL.Model;
using Microsoft.QualityTools.Testing.Fakes;
using WeatherProrok.Logic.Models;
using System.Collections.Generic;

namespace WeatherProrok.Tests.Logic
{
    [TestClass]
    public class WeatherProcessorTests
    {
        IWeatherProcessor processor = new WeatherProcessor(new GismeteoProvider(), new ForecastProcessor());

        [TestMethod]
        public void SearchCityTest()
        {
            var result = processor.SearchCity("Нико");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Any(x => x.Name == "Николаев"));
        }

        [TestMethod]
        public async Task SearchCityAsyncTest()
        {
            var result = await processor.SearchCityAsync("Нико");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Any(x => x.Name == "Николаев"));
        }

        [TestMethod]
        public void AddCityTest()
        {
            using (ShimsContext.Create())
            {
                DAL.Repository.Fakes.ShimBaseRepository<City>.AllInstances.AddT0 = (context, city) => 
                {
                    if (string.IsNullOrEmpty(city.Name) || string.IsNullOrEmpty(city.CityProviderId))
                        return Guid.Empty;
                    return Guid.NewGuid();
                };

                var result = processor.AddCity("tst", "Test city");

                Assert.AreNotEqual(Guid.Empty, result);
            }
        }

        [TestMethod]
        public void ProcessWeatherTest()
        {
            using (ShimsContext.Create())
            {
                var cityId = Guid.NewGuid();
                WeatherProrok.Logic.Processors.Fakes.ShimWeatherProcessor.AllInstances.GetCities = (processor) =>
                    {
                        return new List<CityModel>
                        {
                            new CityModel { Id = cityId, ProviderCityId = "test", Name = "Test City" }
                        };
                    };

                WeatherProrok.Logic.Processors.Fakes.ShimWeatherProcessor.AllInstances.GetCurrentWeatherString = (processor, city) =>
                    {
                        return new CurrentWeatherModel
                        {
                            Cloudity = WeatherProrok.Logic.Models.Cloudity.CLOUD,
                            Precipitation = WeatherProrok.Logic.Models.Precipitation.RAIN,
                            Humidity = 50,
                            Temp = 5,
                            CurrentDateTime = DateTime.Now
                        };
                    };

                DAL.Repository.Fakes.ShimBaseRepository<FactWeather>.AllInstances.GetAllBoolean = (repo, noTracking) =>
                {
                    return new List<FactWeather>
                    {
                        new FactWeather
                        {
                            CityId = cityId,
                            Cloudity = 0,
                            Humidity = 50,
                            Precipitations = 0,
                            Temp = 5,
                            Id = Guid.NewGuid(),
                            Updated = DateTime.Now.AddHours(-1)
                        },
                        new FactWeather
                        {
                            CityId = cityId,
                            Cloudity = 0,
                            Humidity = 50,
                            Precipitations = 0,
                            Temp = 5,
                            Id = Guid.NewGuid(),
                            Updated = DateTime.Now.AddHours(-2)
                        },
                        new FactWeather
                        {
                            CityId = cityId,
                            Cloudity = 0,
                            Humidity = 50,
                            Precipitations = 0,
                            Temp = 5,
                            Id = Guid.NewGuid(),
                            Updated = DateTime.Now.AddHours(-3)
                        }
                    }.AsQueryable();
                };

                DAL.Repository.Fakes.ShimBaseRepository<FactWeather>.AllInstances.AddT0 = (repo, weather) =>
                {
                    return Guid.NewGuid();
                };

                var result = processor.ProcessWeather();

                Assert.IsTrue(result);
            }
        }
    }
}
