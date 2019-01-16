using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestfulXamrinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoLocation : ContentPage
    {
        public GeoLocation()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {



            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                  await  DisplayAlert("alert", location.Speed.ToString(), "ok");
                    label1.Text ="Latitude="+ location.Latitude +", Longitude="+ location.Longitude+", Altitude="+ location.Altitude;
                    await Share.RequestAsync(new ShareTextRequest(label1.Text, "Location"));
                }
                else
                {
                    await DisplayAlert("alert", "error", "ok");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                label1.Text = "error" + fnsEx.Message;
            }
            catch (PermissionException pEx)
            {
                 label1.Text = "error" + pEx.Message;
            }
            catch (Exception ex)
            {
                label1.Text = "Please turn on Location";
            }
        }
    }
}