using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using UserType;
using ItemType;
using Newtonsoft.Json;
using System.Net.Http;

namespace Viasat_App
{
    public partial class MainPage : ContentPage
    {
        UserModel theUser;
        public string responseString;
        public string requestString;

        public MainPage(UserModel user)
        {
            theUser = user;
            InitializeComponent();
            fillFavList();
        }

        //START: BUTTONS EVENTS ########################################################

        private async void searchButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void profileButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(theUser));
        }

        private async void historyButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ResultsPage(globals.Globals.recentlyViewedList));
        }

        private async void favoritesButton_Clicked(object sender, System.EventArgs e)
        {
            globals.Globals.favoritesItemsList.Clear();
            foreach (string itemId in globals.Globals.TheUser.favorites)
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
                    if (httpResponse.Content != null)
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

        //END: BUTTONS EVENTS ##########################################################

        private async void fillFavList()
        {
            //globals.Globals.favoritesItemsList.Clear();
            //foreach (string itemId in globals.Globals.TheUser.favorites)
            //{
            //    ItemModel tempItem = new ItemModel();
            //    tempItem.id = itemId;

            //    var jsonString = JsonConvert.SerializeObject(tempItem,
            //                    Newtonsoft.Json.Formatting.None,
            //                    new JsonSerializerSettings
            //                    {
            //                        NullValueHandling = NullValueHandling.Ignore
            //                    });

            //    requestString = jsonString.ToLower();

            //    using (var httpClient = new HttpClient())
            //    {
            //        var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");
            //        var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/searchbyid", httpContent);
            //        if (httpResponse.Content != null)
            //        {
            //            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            //            responseString = responseContent;
            //        }
            //    }

            //    List<ItemModel> tempItem2 = JsonConvert.DeserializeObject<List<ItemModel>>(responseString);
            //    globals.Globals.favoritesItemsList.Add(tempItem2[0]);
            //}
        }

    }
}