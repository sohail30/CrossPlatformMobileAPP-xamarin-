using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestfulXamrinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentDetails : ContentPage
    {
        // public event EventHandler<Posts> _updateitem;
        // public event EventHandler<Posts> _additem;
        public CommentDetails(Posts p = null)
        {

            //BindingContext = p;
            if (p != null)
            {
                BindingContext = new Posts()
                {
                    id = p.id,
                    name = p.name,
                    email = p.email,
                    body = p.body
                };
            }
            InitializeComponent();
            btn1.Text = (p.name != null) ? "update" : "Add";
            btn1.BackgroundColor = (p.name != null) ? Color.Green : Color.BlueViolet;
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            var changed = BindingContext as Posts;
            // DisplayAlert("alert", changed.name,"ok");
            if (string.IsNullOrWhiteSpace(changed.name))
            {
                DisplayAlert("Alert", "Name should not be empty", "ok");
                return;
            }
            if (!string.IsNullOrWhiteSpace(changed.id))
            {
                //DisplayAlert("Upadting", changed.name, "ok");
                // _updateitem?.Invoke(this, changed);
                MessagingCenter.Send(this, "Update Item", changed);
            }
            else
            {
                // _additem?.Invoke(this, changed);
                MessagingCenter.Send(this, "Add New Item", changed);
                Navigation.PopModalAsync();
            }
        }
    }
}