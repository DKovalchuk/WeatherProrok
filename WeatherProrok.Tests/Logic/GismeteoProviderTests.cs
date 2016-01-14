using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherProrok.Logic.Providers;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherProrok.Tests.Logic
{
    [TestClass]
    public class GismeteoProviderTests
    {
        IWeatherProvider provider = new GismeteoProvider();

        [TestMethod]
        public void SearchCityTest()
        {
            var result = provider.SearchCity("Нико");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Any(x => x.Name == "Николаев"));
        }

        [TestMethod]
        public async Task SearchCityAsyncTest()
        {
            var result = await provider.SearchCityAsync("Нико");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Any(x => x.Name == "Николаев"));
        }

        [TestMethod]
        public void GetCurrentWeatherByCityIDTest()
        {
            var result = provider.GetCurrentWeatherByCityID("4983");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Humidity >= 0);
        }
    }
}
