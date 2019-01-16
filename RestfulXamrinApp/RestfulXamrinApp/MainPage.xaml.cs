using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IronBarCode;

namespace RestfulXamrinApp
{
    public partial class Posts:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string _id;
        public string _name;
        public string _body;

        public string _email;
        public string id {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;

                _id = value;
                propertchanging();
            }
        }
        public string name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;

                _name = value;
                propertchanging();
            }
        }
        public string email
        {
            get { return _email; }
            set
            {
                if (_email == value)
                    return;

                _email = value;
                propertchanging();
            }
        }
        public string body
        {
            get { return _body; }
            set
            {
                if (_body == value)
                    return;

                _body = value;
                propertchanging();
            }
        }
       

      public void propertchanging ([CallerMemberName] string propertyname= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://jsonplaceholder.typicode.com/posts/1/comments";
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Posts> _collection;
       

        public MainPage()
        {


          


            InitializeComponent();
            
        }

        private  async void Button_Clicked(object sender, EventArgs e)
        {
            var getdata= await _client.GetStringAsync(Url);
            IMAGE1.IsVisible = true;
            if (getdata != null)
            { 
                var h = JsonConvert.DeserializeObject<List<Posts>>(getdata);
                //_collection = new ObservableCollection<Posts>
                //{
                //    new Posts{name="sam",email="@gmail,.com",body="45454"},
                //      new Posts{name="sam",email="@gmail,.com",body="45454"},
                //        new Posts{name="sam",email="@gmail,.com",body="45454"}
                //};

                _collection = new ObservableCollection<Posts>(h);
               var id = _collection.First().name;
            //  await  DisplayAlert("alert",id, "ok");
                btn1.IsVisible = false;
             
                restfullist.ItemsSource = _collection;
            }
            else
            {
                label1.IsVisible = true;
                label1.Text = "Error While getting Data";
                label1.TextColor = Color.Red;
            }
          
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Restfullist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
         
            var selected=e.SelectedItem as Posts;
            if (selected != null)
            {
                var commentpage = new CommentDetails(selected);
                await Navigation.PushModalAsync(commentpage);
                MessagingCenter.Subscribe<CommentDetails, Posts>(this, "Update Item", async (source, post) =>
                {
                    MessagingCenter.Unsubscribe<CommentDetails,Posts>(this, "Update Item");

                    if (post != null)
                    {
                        //commentpage._updateitem += (source, post) =>
                        //{
                       
                        var count = _collection.Count;
                      //  post.id = (count + 1).ToString();
                        selected.name = post.name;
                        selected.email = post.email;
                        selected.body = post.body;
                        var h = JsonConvert.SerializeObject(post);
                        var resukt = _client.PutAsync(Url + "/" + selected.id, new StringContent(h));
                        await DisplayAlert("alert", post.id, "ok");
                        //};
                    }

                });
            }
            restfullist.SelectedItem = null;
        



        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            var commentpage = new CommentDetails(new Posts());
            await Navigation.PushModalAsync(commentpage);
            MessagingCenter.Subscribe<CommentDetails, Posts>(this, "Add New Item", async (source, p) =>
            {
                MessagingCenter.Unsubscribe<CommentDetails,Posts>(this, "Add New Item");
                if (p != null)
                {
                    var count = _collection.Count;
                    p.id = (count + 1).ToString();
                    var serlize = JsonConvert.SerializeObject(p);
                    var result =  await _client.PostAsync(Url, new StringContent(serlize));
                    _collection.Insert(0, p);
                    p = null;
                   
                }
            });
            //commentpage._additem += (source, post) =>
            //{
            //    // DisplayAlert("alert","start", "ok");
            //    var serlize = JsonConvert.SerializeObject(post);
            //  var result= _client.PostAsync(Url, new StringContent(serlize));
            //    _collection.Insert(0,post);
            //  //  DisplayAlert("alert", result.ToString(), "ok");
            //};
         
             // _collection.Insert(0,new Posts { name="sam"});



        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var selceted = (sender as MenuItem).CommandParameter as Posts;
          //  await DisplayAlert("alert", selceted.name, "ok");
            await    _client.DeleteAsync(Url + "/" + selceted.id);
         
            _collection.Remove(selceted);
           // restfullist.ItemsSource = _collection;
        }
        //public async void addItem(CommentDetails source,Posts p){
        //    //   await  DisplayAlert("messiging", p.name, "ok");
        //    if (p != null)
        //    {
        //        var serlize = JsonConvert.SerializeObject(p);
        //        var result = await _client.PostAsync(Url, new StringContent(serlize));
        //        _collection.Insert(0, p);
        //        MessagingCenter.Unsubscribe<CommentDetails>(this, "Add New Item");
        //    }
          
        //  //  IronBarCode.PagedBarcodeResult(_collection);
        //}

    }
}
