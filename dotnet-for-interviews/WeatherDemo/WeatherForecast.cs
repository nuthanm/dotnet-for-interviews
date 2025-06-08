using Newtonsoft.Json.Linq;

namespace WeatherDemo
{
    public partial class WeatherForecast : Form
    {
        private static readonly string apiKey = "c29f5f2f5ff9c2ec41c5aff4a8c65b48";
        private static readonly string apiBaseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public WeatherForecast()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxCity.Text))
            {
                MessageBox.Show("Please enter a city name.");
                return;
            }

            try
            {
                lblStatus.Text = "Fetching weather data...";
                lblStatus.ForeColor = Color.Blue;

                var weatherData = await GetWeatherDataAsync(txtBoxCity.Text);
                DisplayWeatherInfo(weatherData);

                lblStatus.Text = "Weather data updated successfully!";
                lblStatus.ForeColor = Color.Green;
                weatherPanel.Visible = true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed to get weather data.";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Error fetching weather data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                weatherPanel.Visible = false;
            }
        }

        static async Task<JObject> GetWeatherDataAsync(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{apiBaseUrl}?q={city}&appid={apiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseBody);
            }
        }


        // Display Weather Info

        private void DisplayWeatherInfo(JObject weatherData)
        {
            string city = weatherData["name"].ToString();
            string country = weatherData["sys"]["country"].ToString();
            string description = weatherData["weather"][0]["description"].ToString();
            double temperature = (double)weatherData["main"]["temp"];
            double feelsLike = (double)weatherData["main"]["feels_like"];
            double humidity = (double)weatherData["main"]["humidity"];
            double windSpeed = (double)weatherData["wind"]["speed"];
            string weatherIconCode = weatherData["weather"][0]["icon"].ToString();

            // Format weather details with better layout
            lblWeatherDetails.Text = $"Weather in {city}, {country}\n" +
                                     $"Condition: {char.ToUpper(description[0]) + description.Substring(1)}\n" +
                                     $"Temperature: {temperature:F1}°C (Feels like: {feelsLike:F1}°C)\n" +
                                     $"Humidity: {humidity}%\n" +
                                     $"Wind Speed: {windSpeed} m/s";

            // Load weather icon from OpenWeatherMap
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string iconUrl = $"http://openweathermap.org/img/wn/{weatherIconCode}@2x.png";
                    byte[] imageBytes = client.GetByteArrayAsync(iconUrl).Result;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        pbxWeatherIcon.Image = Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                // If icon loading fails, don't show an icon
                pbxWeatherIcon.Image = null;
            }
        }

        private void txtBoxCity_TextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "Enter a city name and click Search to get the weather forecast.";
            lblStatus.ForeColor = SystemColors.ControlDark;
            weatherPanel.Visible = false;
        }

        private void WeatherForecast_Load(object sender, EventArgs e)
        {
            txtBoxCity.Text = string.Empty;
            txtBoxCity.Focus();
        }

        private void txtBoxCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Optionally prevent the ding sound
                e.SuppressKeyPress = true;
                btnSearch_Click(btnSearch, EventArgs.Empty);
            }

        }
    }
}
