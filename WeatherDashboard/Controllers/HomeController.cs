using Microsoft.AspNetCore.Mvc;
using WeatherDashboard.Services;

namespace WeatherDashboard.Controllers
{
    public class HomeController(WeatherService weatherService) : Controller
    {
        private readonly WeatherService _weatherService = weatherService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FetchWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                ModelState.AddModelError("", "City name cannot be empty.");
                return View("Index");
            }

            // Call the weather API with the provided city name
            var weatherData = await _weatherService.GetWeatherData(city);
            if (weatherData == null)
            {
                ModelState.AddModelError("", "Weather data not found for the specified city.");
                return View("Index");
            }

            // Pass the weather data to the view
            return View("Index", weatherData);
        }
    }
}
