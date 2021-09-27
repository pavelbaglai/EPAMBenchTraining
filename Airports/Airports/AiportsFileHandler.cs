using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace Airports
{
    public class AiportsFileHandler
    {
        const string AirportsJsonFilePath = @"airports.json";
        const string CitiesJsonFilePath = @"cities.json";
        const string CountriesJsonFilePath = @"countries.json";
        const string DatFilePath = @"airports.dat";
        const string TimeZoneInfoPath = @"timezoneinfo.json";
        
        private List<Airport> Airports;
        private Dictionary<int, string> AirportTimeZones;
        public AiportsFileHandler()
        {
            Airports = new List<Airport>();
            AirportTimeZones = new Dictionary<int, string>();
            CheckNecessaryFiles();
            DeserializeAirports();
        }
        public void CheckNecessaryFiles()
        {
            if(!File.Exists(AirportsJsonFilePath) || !File.Exists(CitiesJsonFilePath) || !File.Exists(CountriesJsonFilePath))
            {
                if (!File.Exists(DatFilePath))
                    throw new FileNotFoundException(DatFilePath);
                ProcessTimeZoneInfoFile();
                ProcessAirportsDatFile();
            }
        }

        private void CreateJsonFiles(List<City> cities, List<Country> countries)
        {
            File.WriteAllText(AirportsJsonFilePath, JsonConvert.SerializeObject(Airports));
            File.WriteAllText(CitiesJsonFilePath, JsonConvert.SerializeObject(cities));
            File.WriteAllText(CountriesJsonFilePath, JsonConvert.SerializeObject(countries));
        }

        private void ProcessTimeZoneInfoFile()
        {
            if (!File.Exists(TimeZoneInfoPath))
                throw new FileNotFoundException(TimeZoneInfoPath);
            AirportTimeZones = JsonConvert.DeserializeObject<List<TimeZoneInfoJSON>>(File.ReadAllText(TimeZoneInfoPath))
                .ToDictionary(a => a.AirportId, a => a.TimeZoneInfoId);
        }

        private void ProcessAirportsDatFile()
        {
            var cities = new List<City>();
            var countries = new Dictionary<string, Country>();
            Log.Logger.Information("Processing airports.dat file...");
            var pattern = @"^(?<Id>\d+),""
                             (?<AirportName>[^""]+)"",""
                             (?<CityName>[^""]+)"",""
                             (?<CountryName>[^""]+)"",""
                             (?<IATACode>[A-Z]{3})"",""
                             (?<ICAOCode>[A-Z]{4})"",
                             (?<Latitude>-?[0-9]*\.?[0-9]*),
                             (?<Longtitude>-?[0-9]*\.?[0-9]*),
                             (?<Altitude>-?[0-9]*)";
            var regionInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(s => new RegionInfo(s.ToString()))
                .Select(s => new { s.EnglishName, s.TwoLetterISORegionName, s.ThreeLetterISORegionName })
                .Distinct()
                .ToDictionary(a => a.EnglishName, a => new { a.TwoLetterISORegionName, a.ThreeLetterISORegionName });
            int wrongLineCount = 0;
            foreach (var line in File.ReadLines(DatFilePath))
            {
                var match = Regex.Match(line, pattern, RegexOptions.IgnorePatternWhitespace);
                if (match.Success)
                {
                    var cityName = match.Groups["CityName"].Value;
                    var countryName = match.Groups["CountryName"].Value;
                    Airport airport = new Airport();
                    airport.Id = int.Parse(match.Groups["Id"].Value);
                    airport.TimeZoneName = AirportTimeZones[airport.Id];
                    if (!countries.ContainsKey(countryName))
                    {
                        if (!regionInfos.ContainsKey(countryName))
                        {
                            Log.Logger.Error("Invalid country name! - country: " + countryName);
                            wrongLineCount++;
                            continue;
                        }
                        Country country = new Country
                        {
                            Id = countries.Keys.Count + 1,
                            Name = countryName,
                            TwoLetterISOCode = regionInfos[countryName].TwoLetterISORegionName,
                            ThreeLetterISOCode = regionInfos[countryName].ThreeLetterISORegionName
                        };

                        countries.Add(countryName, country);
                    }
                    airport.Country = countries[countryName];
                    airport.CountryId = airport.Country.Id;
                    var city = cities.Where(a => a.Name == cityName && a.CountryId == airport.CountryId).FirstOrDefault();
                    if (city == null)
                    {
                        city = new City
                        {
                            Id = cities.Count + 1,
                            Name = cityName,
                            CountryId = airport.CountryId,
                            TimeZoneName = airport.TimeZoneName
                        };
                        cities.Add(city);
                    }
                    airport.City = city;
                    airport.CityId = airport.City.Id;
                    airport.Name = match.Groups["AirportName"].Value;
                    airport.IATACode = match.Groups["IATACode"].Value;
                    airport.ICAOCode = match.Groups["ICAOCode"].Value;
                    airport.Location = new Location
                    {
                        Longtitude = double.Parse(match.Groups["Longtitude"].Value),
                        Latitude = double.Parse(match.Groups["Latitude"].Value),
                        Altitude = int.Parse(match.Groups["Altitude"].Value)
                    };
                    Airports.Add(airport);
                }
                else
                {
                    Log.Logger.Error("Invalid row! - data: " + line);
                    wrongLineCount++;
                }
            }
            Log.Logger.Information("Total skipped row count: " + wrongLineCount);
            Log.Logger.Information("Finished processing airports.dat file...");
            CreateJsonFiles(cities, countries.Values.ToList());
        }

        private List<City> DeserializeCities()
        {
            return JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(CitiesJsonFilePath));
        }

        private List<Country> DeserializeCountries()
        {
            return JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(CountriesJsonFilePath));
        }

        private void DeserializeAirports()

        {
            var countries = DeserializeCountries().ToDictionary(a => a.Id, a => a);
            var cities = DeserializeCities().ToDictionary(a => a.Id, a => a);
            var airports = JsonConvert.DeserializeObject<List<Airport>>(File.ReadAllText(AirportsJsonFilePath));
            for (int i = 0; i < airports.Count; i++)
            {
                var airport = airports[i];
                var city = cities[airport.CityId];
                var country = countries[airport.CountryId];
                city.Country = country;
                airport.City = city;
                airport.Country = country;
            }
            Airports = airports;
        }
        public IEnumerable<AirportCountWithName> GetAirportCountByCountries()
        {
            return Airports.GroupBy(g => g.Country.Id)
                    .Select(s => new AirportCountWithName
                    {
                        Name = s.First().Country.Name,
                        AirportCount = s.Count(),
                    })
                    .OrderBy(o => o.Name);
        }

        public IEnumerable<AirportCountWithName> GetMaxAirportCountByCity()
        {

            var maxAirportsCountInCity = Airports.GroupBy(g => g.City.Id).Max(a => a.Count());
            return Airports.GroupBy(g => g.City.Id)
                                    .Where(a => a.Count() == maxAirportsCountInCity)
                                    .Select(s => new AirportCountWithName
                                    {
                                        Name = s.First().City.Name,
                                        AirportCount = maxAirportsCountInCity,
                                    });
        }

        public AirportDataWithDistance GetClosestAirport(Location location)
        {
            return Airports.Select(s => new AirportDataWithDistance
            {
                Distance = Location.GetDistance(location, s.Location),
                Name = s.FullName,
                City = s.City.Name,
            }).OrderBy(o => o.Distance).First();
        }

        public Airport GetAirportByIATACode(string iataCode)
        {
            return Airports.SingleOrDefault(a => a.IATACode == iataCode);
        }
    }

    public class AirportCountWithName
    {
        public string Name { get; set; }
        public int AirportCount { get; set; }
    }

    public class AirportDataWithDistance
    {
        public double Distance { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}

