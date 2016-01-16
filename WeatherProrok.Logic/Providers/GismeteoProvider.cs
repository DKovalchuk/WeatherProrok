using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherProrok.Logic.Models;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using HtmlAgilityPack;
using WeatherProrok.Logic.Helpers;

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
            var url = string.Format("{0}/city/daily/{1}", GismeteoRootURL, cityId);

            var responce = MakeRequest(url);
            return ParseCurrentWeather(responce);
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherByCityIDAsync(string cityId)
        {
            var url = string.Format("{0}/city/daily/{1}", GismeteoRootURL, cityId);

            var responce = await MakeRequestAsync(url);
            return ParseCurrentWeather(responce);
        }

        public CurrentWeatherModel GetCurrentWeatherByCoordinates(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SearchCityModel>> SearchCityAsync(string search)
        {
            var url = string.Format("{0}/ajax/suggest/?lang=ru&startsWith={1}&sort=typ", GismeteoRootURL, search);

            var responceData = await MakeRequestAsync(url);
            var data = JsonConvert.DeserializeObject<GismeteoResponceModel>(responceData);

            if (data.Items.Any())
                return data.Items.Select(i => new SearchCityModel { Id = i.Id.ToString(), Name = i.Name, FullName = string.Format("{0}, {1}, {2}, {3}", i.CountryName, i.DistrictName, i.SubDistrictName == i.Name ? string.Empty : i.SubDistrictName, i.Name) });

            return new List<SearchCityModel>();
        }

        public IEnumerable<SearchCityModel> SearchCity(string search)
        {
            var url = string.Format("{0}/ajax/suggest/?lang=ru&startsWith={1}&sort=typ", GismeteoRootURL, search);

            var responceData = MakeRequest(url);
            var data = JsonConvert.DeserializeObject<GismeteoResponceModel>(responceData);

            if (data.Items.Any())
                return data.Items.Select(i => new SearchCityModel { Id = i.Id.ToString(), Name = i.Name, FullName = string.Format("{0}, {1}, {2}, {3}", i.CountryName, i.DistrictName, i.SubDistrictName == i.Name ? string.Empty : i.SubDistrictName, i.Name) });

            return new List<SearchCityModel>();
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

        private CurrentWeatherModel ParseCurrentWeather(string data)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);

            var content = doc.GetElementbyId("weather")
                .ChildNodes.First(x => x.Attributes["class"].Value == "fcontent")
                .ChildNodes.First(x => x.Name == "div" && x.Attributes["class"].Value == "section higher");

            int temp = int.Parse(content
                .ChildNodes.First(x => x.Name == "div" && x.Attributes["class"].Value == "temp")
                .ChildNodes.First(x => x.Name == "dd" && x.Attributes["class"].Value == "value m_temp c")
                .ChildNodes.First(x => x.Name == "#text").InnerText);

            int humidity = int.Parse(content
                .ChildNodes.First(x => x.Name == "div" && x.Attributes["class"].Value == "wicon hum")
                .ChildNodes.First(x => x.Name == "#text").InnerText);

            var cloudsAndPrecipitation = content
                .ChildNodes.First(x => x.Name == "dl" && x.Attributes["class"].Value == "cloudness")
                .ChildNodes.First(x => x.Name == "dt").Attributes["title"].Value;

            var clouds = string.Empty;
            var precipitation = string.Empty;
            if (cloudsAndPrecipitation.Contains(","))
            {
                var cp = cloudsAndPrecipitation.Split(',');
                clouds = cp[0];
                precipitation = cp[1];
            }
            else
                clouds = cloudsAndPrecipitation;

            var time = DateTime.Parse(content
                .ChildNodes.First(x => x.Name == "div" && x.Attributes["class"].Value == "wrap f_link")
                .ChildNodes.First(x => x.Name == "span" && x.Attributes["class"].Value == "icon date").Attributes["data-obs-time"].Value);

            return new CurrentWeatherModel
            {
                Temp = temp,
                Humidity = humidity,
                Cloudity = GismeteoCloudsAndPrecipitationMappingHelper.MapCloudity(clouds),
                Precipitation = GismeteoCloudsAndPrecipitationMappingHelper.MapPrecipitation(precipitation),
                CurrentDateTime = time
            };
        }
    }
}
