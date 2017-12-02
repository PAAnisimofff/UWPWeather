using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;





namespace UWPWeatherService.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home


        public async Task<ActionResult> Index(string lat, string lon)
        {
            var latitude = double.Parse(lat.Substring(0, 5));
            var longtitude = double.Parse(lon.Substring(0, 5));
            //double longtitude = 49.122139;
            //double latitude = 55.788738; 
            var weather = await Models.OpenWeatherMapProxy.GetWeather(latitude, longtitude);

            ViewBag.Name = weather.name;
            ViewBag.Temp = ((int)weather.main.temp).ToString();
            ViewBag.Description = weather.weather[0].description;

            return View();
        }
    }
}