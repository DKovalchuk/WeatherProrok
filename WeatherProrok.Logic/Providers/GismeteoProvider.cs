using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WeatherProrok.Logic.Providers
{
    public class GismeteoProvider : IWeatherProvider
    {
        public string GismeteoRootURL
        {
            get
            {
                var url = ConfigurationManager.AppSettings["GismeteoURL"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Gismeteo root url not found in app.config file");

                return url;
            }
        }

        public CurrentWeatherModel GetCurrentWeatherByCity(string city)
        {
            throw new NotImplementedException();
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherByCityAsync(string city)
        {
            throw new NotImplementedException();
        }

        public CurrentWeatherModel GetCurrentWeatherByCityID(string cityId)
        {
            throw new NotImplementedException();

            var url = string.Format("{0}/city/daily/{1}", GismeteoRootURL, cityId);


        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherByCityIDAsync(string cityId)
        {
            throw new NotImplementedException();
        }

        public CurrentWeatherModel GetCurrentWeatherByCoordinates(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CityModel>> SearchCityAsync(string search)
        {
            var url = string.Format("{0}/ajax/suggest/?lang=ru&startsWith={1}&sort=typ", GismeteoRootURL, search);

            var responceData = await MakeRequestAsync(url);
            var data = JsonConvert.DeserializeObject<GismeteoResponceModel>(responceData);

            if (data.Items.Any())
                return data.Items.Select(i => new CityModel { Id = i.Id.ToString(), Name = i.Name, FullName = string.Format("{0}, {1}, {2}, {3}", i.CountryName, i.DistrictName, i.SubDistrictName == i.Name ? string.Empty : i.SubDistrictName, i.Name) });

            return new List<CityModel>();
        }

        public IEnumerable<CityModel> SearchCity(string search)
        {
            var url = string.Format("{0}/ajax/suggest/?lang=ru&startsWith={1}&sort=typ", GismeteoRootURL, search);

            var responceData = MakeRequest(url);
            var data = JsonConvert.DeserializeObject<GismeteoResponceModel>(responceData);

            if (data.Items.Any())
                return data.Items.Select(i => new CityModel { Id = i.Id.ToString(), Name = i.Name, FullName = string.Format("{0}, {1}, {2}, {3}", i.CountryName, i.DistrictName, i.SubDistrictName == i.Name ? string.Empty : i.SubDistrictName, i.Name) });

            return new List<CityModel>();
        }

        private string MakeRequest(string url)
        {
            var request = WebRequest.Create(url);
            var responce = request.GetResponse();
            var responceStream = responce.GetResponseStream();
            return new StreamReader(responceStream).ReadToEnd();
        }

        private async Task<string> MakeRequestAsync(string url)
        {
            var request = WebRequest.Create(url);
            var responce = await request.GetResponseAsync();
            var responceStream = responce.GetResponseStream();
            return await new StreamReader(responceStream).ReadToEndAsync();
        }
    }
}
