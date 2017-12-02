﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UWPWeather
{
   public class LocationManager
    {
        public async static Task<Geoposition> GetPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            if (accessStatus != GeolocationAccessStatus.Allowed)
                throw new Exception();

            var geolcater = new Geolocator { DesiredAccuracyInMeters = 0 };

            var position = await geolcater.GetGeopositionAsync();

            return position;
        }
    }
}
