using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;

using ItemType;
using UserType;

namespace Viasat_App
{
    public partial class ProfilePage : ContentPage
    {
        public UserModel user = new UserModel();
        //ItemModel item = new ItemModel();
        public string responseString;
        public string requestString;
        public ObservableCollection<ItemModel> favoritesList = new ObservableCollection<ItemModel>();


        public ProfilePage(UserModel theUser)
        {
            user = theUser;

            InitializeComponent();

            nameLabel.Text = user.name;
            lastLabel.Text = user.lastName;
            permissionLabel.Text = user.permission_level.ToString();
        }

        public async void recentlyViewedButton_Clicked(object sender, EventArgs e)
        {
            //historyList.Clear();
            for(int i=0; i<user.recently_viewed.Count(); i++)
            {
                string itemId = user.recently_viewed[i];
                ItemModel tempItem = new ItemModel();
                tempItem.id = itemId;

                //item serialized to be sent as the request to the API
                //handles parameters not entered by the user, that way they are not included in the json string so the API doesn't have to parse and check for nulls.
                var jsonString = JsonConvert.SerializeObject(tempItem,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                requestString = jsonString.ToLower();

                //Creating the http client which will provide us with the network capabilities
                using (var httpClient = new HttpClient())
                {
                    //request string to be sent to the API
                    var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

                    //sending the previously created request to the api and waiting for a response that will be saved in the httpResponse var
                    //  NOTE: if the api's base url changes this has to be modified.
                    var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/searchbyid", httpContent);

                    //to visualize the json sent over the network comment the previous line, uncomment the next one and go to the link.
                    //var httpResponse = await httpClient.PostAsync("https://putsreq.com/qmumqAwIq9s5RBEfbNfh", httpContent);

                    //verifying that response is not empty
                    if (httpResponse.Content != null)
                    {
                        //response into a usable var
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        //debugging
                        Console.WriteLine("JSON: " + requestString.ToString());
                        Console.WriteLine("POST: " + httpContent.ToString());
                        Console.WriteLine("GET: " + responseContent);

                        responseString = responseContent;
                    }
                }


                ObservableCollection<ItemModel> tempItem2 = JsonConvert.DeserializeObject<ObservableCollection<ItemModel>>(responseString);
                //historyList.Add(tempItem2[0]);
               
            }

            await Navigation.PushAsync(new ResultsPage(globals.Globals.recentlyViewedList));
        }

        public async void favoritesButton_Clicked(object sender, System.EventArgs e)
        {
            globals.Globals.favoritesItemsList.Clear();
            foreach(string itemId in globals.Globals.favoritesList)
            {
                ItemModel tempItem = new ItemModel();
                tempItem.id = itemId;

                var jsonString = JsonConvert.SerializeObject(tempItem,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                requestString = jsonString.ToLower();

                using (var httpClient = new HttpClient())
                {
                    var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");
                    var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/searchbyid", httpContent);
                    if(httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        responseString = responseContent;
                    }
                }

                List<ItemModel> tempItem2 = JsonConvert.DeserializeObject<List<ItemModel>>(responseString);
                globals.Globals.favoritesItemsList.Add(tempItem2[0]);
            }
            await Navigation.PushAsync(new ResultsPage(globals.Globals.favoritesItemsList));
        }
    }
}

