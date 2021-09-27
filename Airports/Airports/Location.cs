using System;
using System.Collections.Generic;
using System.Text;
using Geolocation;

namespace Airports
{
    public class Location
    {
        public int Altitude { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public static double GetDistance(Location origin, Location destination)
        {
            return GetDistance(origin.Latitude, origin.Longtitude, 0, destination.Latitude, destination.Longtitude, 0);
        }
        public static double GetDistance(double originLatitude, double originLongtitude, int originAltitude, double destinationLatitude, double destinationLongtitude, int destinationAltitude)
        {
            return Geolocation.GeoCalculator.GetDistance(originLatitude, originLongtitude, destinationLatitude, destinationLongtitude, 2, DistanceUnit.Kilometers);
        }
        public static Location GetLocationByString(string s)
        {
            Location location = null;
            var items = s.Split(",");
            if(items.Length== 3)
            {
                double latitude, longtitude;
                int altitude;
                if (double.TryParse(items[0].Trim(), out latitude) && double.TryParse(items[1].Trim(), out longtitude) && int.TryParse(items[2].Trim(), out altitude))
                {
                    location = new Location
                    {
                        Altitude = altitude,
                        Latitude = latitude,
                        Longtitude = longtitude,
                    };
                }
            }
            return location;
        }
    }
}
