using System;
using System.Collections.Generic;
using System.Linq;

namespace Airports
{
    class Program
    {
        
        static void Main(string[] args)
        {
            AiportsFileHandler airportsHandler = new AiportsFileHandler();
            // A
            Console.WriteLine("Countries by name and Airport count:");
            foreach (var item in airportsHandler.GetAirportCountByCountries())
            {
                Console.WriteLine($"{item.Name}: {item.AirportCount}");
            }
            // B
            Console.WriteLine("Cities with the most airports: ");
            foreach (var item in airportsHandler.GetMaxAirportCountByCity())
            {
                Console.WriteLine($"{item.Name}: {item.AirportCount}");
            }
            // C
            Console.WriteLine("Please enter GPS coordinates in the format: Latitude, Longitude, Altitude");
            var line = Console.ReadLine().Trim();
            var location = Location.GetLocationByString(line);
            if(location == null)
            {
                Console.WriteLine("Invalid coordinates!");
            }
            else
            {
                var closestAirport = airportsHandler.GetClosestAirport(location);
                Console.WriteLine($"Closest Airport is: {closestAirport.Name} in {closestAirport.City}");
            }
            // D
            Console.WriteLine("Please enter IATACode of an Airport");
            line = Console.ReadLine().Trim();
            var airport = airportsHandler.GetAirportByIATACode(line);
            if (airport == null)
            {
                Console.WriteLine("Invalid IATA code, code not found!");
            }
            else
            {
                TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(airport.TimeZoneName);
                var localTime = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
                var utcOffset = timezone.BaseUtcOffset;
                var utcOffsetFormat = (utcOffset < TimeSpan.Zero ? "-" : "+") + utcOffset.Hours.ToString() + ":" + utcOffset.Minutes.ToString().PadRight(2, '0');
                Console.WriteLine($"{airport.Name} - {airport.City.Name}, {airport.Country.Name} - Local time: {localTime.ToString("HH:mm:ss")} (GMT{utcOffsetFormat})");
            }
        }
    }
}
