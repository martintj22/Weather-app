using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();


        }

    
        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;

            }

        }

        private async void Button_Clicked(object sender, EventArgs e) 
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)

                    });
                }
                if (location == null)
                    LabelLocation.Text = "No GPS";
                else
                    LabelLocation.Text = $"{location.Latitude} {location.Longitude}";

                

            }
            catch (Exception)
            { 
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
        //  requestUri += $"&long={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
    }
}


