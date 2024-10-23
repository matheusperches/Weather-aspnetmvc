using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WeatherDashboard.Models;

namespace WeatherDashboard.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController()
        {
            _weatherService = new WeatherService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            var WeatherData = await _weatherService.GetWeatherData(cityName);
            if (WeatherData != null)
            {
                var json = JObject.Parse(WeatherData);
                    var viewModel = new WeatherViewModel
                    {
                        City = json["name"]?.ToString() ?? "Unknown City",
                        Description = json["weather"]?[0]?["description"]?.ToString() ?? "No description available",
                        Temperature = json["main"]?["temp"] != null ? double.Parse(json["main"]["temp"].ToString()) : 0.0,
                        Humidity = json["main"]?["humidity"] != null ? int.Parse(json["main"]["humidity"].ToString()) : 0,
                        WindSpeed = (json["wind"]?["speed"]) == null ? 0.0 : double.Parse(json["wind"]["speed"].ToString())
                    };
                return View("WeatherDetails", viewModel);
            }
            return View("Error");
        }
    }
}
