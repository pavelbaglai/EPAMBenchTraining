using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Airports
{
    public class Airport
    {
        public int Id { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public string FullName => Name.EndsWith("Airport") ? Name : Name + " Airport";
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string TimeZoneName { get; set; }
        public Location Location { get; set; }
        [JsonIgnore]
        public City City { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
    }
}
