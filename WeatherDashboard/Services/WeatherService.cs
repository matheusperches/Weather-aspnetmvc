using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherDashboard.Models; // Reference to the WeatherViewModel

namespace WeatherDashboard.Services
{
    public class WeatherService(HttpClient httpClient)
    {
        private readonly string apiKey = "f1d48faa8140bac093b0243ba23f705f";
        private readonly HttpClient _httpClient = httpClient;

        public async Task<WeatherViewModel?> GetWeatherData(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(data);

            // Parse JSON data into WeatherViewModel properties
            return new WeatherViewModel
            {
                City = json["name"]?.ToString(),
                Temperature = json["main"]?["temp"]?.Value<double>() ?? 0.0,
                Humidity = json["main"]?["humidity"]?.Value<int>() ?? 0,
                WindSpeed = json["wind"]?["speed"]?.Value<double>() ?? 0.0
            };
        }
        public async Task<WeatherViewModel?> GetWeatherDataByLocation(double latitude, double longitude)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(data);

            return new WeatherViewModel
            {
                City = json["name"]?.ToString() ?? "Unknown Location",
                Description = json["weather"]?[0]?["description"]?.ToString() ?? "No description available",
                Temperature = json["main"]?["temp"]?.Value<double>() ?? 0.0,
                Humidity = json["main"]?["humidity"]?.Value<int>() ?? 0,
                WindSpeed = json["wind"]?["speed"]?.Value<double>() ?? 0.0
            };
        }
    }
}
