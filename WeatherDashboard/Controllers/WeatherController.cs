using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherDashboard.Models;
using WeatherDashboard.Services;

namespace WeatherDashboard.Controllers
{
    public class WeatherController(WeatherService weatherService) : Controller
    {
        private readonly WeatherService _weatherService = weatherService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            var viewModel = await _weatherService.GetWeatherData(cityName);
            if (viewModel != null)
            {
                return View("WeatherDetails", viewModel);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> GetWeatherByLocation([FromBody] LocationModel location)
        {
            var viewModel = await _weatherService.GetWeatherDataByLocation(location.Latitude, location.Longitude);
            if (viewModel != null)
            {
                return PartialView("WeatherDetails", viewModel);
            }
            return View("Error");
        }
    }
}
