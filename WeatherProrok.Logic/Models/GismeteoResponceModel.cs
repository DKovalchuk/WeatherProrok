using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProrok.Logic.Models
{
    public class GismeteoResponceModel
    {
        public IEnumerable<GismeteoResponceItemModel> Items { get; set; }
    }

    public class GismeteoResponceItemModel
    {
        public int Id { get; set; }
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
        [JsonProperty("district_name")]
        public string DistrictName { get; set; }
        [JsonProperty("sub_district_name")]
        public string SubDistrictName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
