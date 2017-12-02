using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace UWPWeather
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public String ReplaceCharInString(String str, int index, Char newSymb)
        {
            return str.Remove(index, 1).Insert(index, newSymb.ToString());
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                var position = await LocationManager.GetPosition();

                var lat = position.Coordinate.Point.Position.Latitude;
                var lon = position.Coordinate.Point.Position.Longitude;

                RootObject myWeather = await OpenWeatherMapProxy.GetWeather(lat, lon);


                var uri = String.Format("http://uwpweatherservice20171202044413.azurewebsites.net/?lat={0}&lon={1}", lat, lon);
                uri = ReplaceCharInString(uri, 64, '.');
                uri = ReplaceCharInString(uri, 76, '.');
                var tileContent = new Uri(uri);
                var requestedInterval = PeriodicUpdateRecurrence.HalfHour;
                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.StartPeriodicUpdate(tileContent, requestedInterval);

                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                //ResultTextBlock.Text = myWeather.name + "   " + (myWeather.main.temp).ToString() + "   " + myWeather.weather[0].description;
                TempTextBlock.Text = (myWeather.main.temp).ToString();
                DescriptionTextBlock.Text = myWeather.weather[0].description;
                LocationTextBlock.Text = myWeather.name;
            }
            catch
            {
                LocationTextBlock.Text = "Ошибка! Невозможно узнать погоду.";
            }
        }
    }
}
