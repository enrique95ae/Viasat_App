using System;
using System.Collections.Generic;
using UserType;
using ItemType;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class LoginPage : ContentPage
    {
        UserModel userRequest = new UserModel();
        ObservableCollection<UserModel> userReceived = new ObservableCollection<UserModel>();
        string requestString;
        string responseString;

        List<String> demoUsers = new List<string>();

        public LoginPage()
        {
            InitializeComponent();
            demoUsers.Add("000000000000000000000002");



            //globals.Globals.TheUser.name = "Forrest";
            //globals.Globals.TheUser.lastName = "Joy";
            //globals.Globals.TheUser._id = "000000000000000000000002";
            //globals.Globals.TheUser.permission_level = 9;

            globals.Globals.TheUser.recently_viewed = new System.Collections.ObjectModel.ObservableCollection<string>();

            globals.Globals.favoritesList = new ObservableCollection<string>();
            globals.Globals.favoritesItemsList = new ObservableCollection<ItemModel>();



        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            if(demoUsers.Contains(usernameEntry.Text) && passwordEntry.Text == "1234") //demo auth
            {
                createRequest(usernameEntry.Text);

                using (var httpClient = new HttpClient())
                {
                    var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

                    var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/returnuser", httpContent);

                    if(httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        responseString = responseContent;

                        //debugging
                        Console.WriteLine("JSON: " + requestString);
                        Console.WriteLine("POST: " + httpContent.ToString());
                        Console.WriteLine("GET: " + responseContent);
                    }
                }
            }

            userReceived = JsonConvert.DeserializeObject<ObservableCollection<UserModel>>(responseString);

            //globals.Globals.TheUser = JsonConvert.DeserializeObject<UserModel>(responseString);
            globals.Globals.TheUser = userReceived[0];


            await Navigation.PushAsync(new MainPage(globals.Globals.TheUser));
        }


        public void createRequest(string userID)
        {
            UserModel tempUser = new UserModel();
            tempUser._id = userID;

            var jsonString = JsonConvert.SerializeObject(tempUser,
                Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            requestString = jsonString;
        }
    }
}