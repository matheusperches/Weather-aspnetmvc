using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;  // Install Newtonsoft.Json from NuGet for JSON parsing

namespace WeatherDashboard.Models
{
    public class WeatherService
    {
        private readonly string apiKey = "f1d48faa8140bac093b0243ba23f705f";
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string?> GetWeatherData(string cityName)
        {
            // Constructing the API URL 
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            return null;

        }
    }
}
