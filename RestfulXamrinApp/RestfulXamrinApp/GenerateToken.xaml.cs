using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace RestfulXamrinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

    public class token
    {
        public string _token { get; set; }
    }
	public partial class GenerateToken : ContentPage
	{
        private HttpClient _http = new HttpClient();
        private const string Url = "http://devapi.gso.com/Rest/v1//Token";

        public GenerateToken ()
		{
            
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {

         
            _http.DefaultRequestHeaders.Add("UserName", ent1.Text);
            _http.DefaultRequestHeaders.Add("Password", ent2.Text);
            _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
     HttpResponseMessage k= await _http.GetAsync(Url);
            //var p=JsonConvert.DeserializeObject<token>(k.Headers.ToString();
           
            foreach(var xx in k.Headers)
            {
                if (xx.Key.ToLower()=="token")
                {
                    await DisplayAlert("alert1", xx.Value.First().ToString(), "ok");
                }                
            }

            //var xy = k.Headers.Where(c => c.Key.ToLower() == "token").FirstOrDefault();
            //await DisplayAlert("alert2", xy.Value.ToString(), "ok");

            //await DisplayAlert("alert",xx "ok");


        }
    }
}