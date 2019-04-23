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
        public List<ItemModel> itemsList = new List<ItemModel>();


        public ProfilePage(UserModel theUser)
        {
            InitializeComponent();

            user = theUser;

            nameLabel.Text = user.UserName;
            lastLabel.Text = user.UserLast;
            permissionLabel.Text = user.PermissionLevel.ToString();
        }

        public async void recentlyViewedButton_Clicked(object sender, EventArgs e)
        {
            for(int i=0; i<user.RecentlyViewed.Count(); i++)
            {
                string itemId = user.RecentlyViewed[i];
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

                var itemInArray = JsonConvert.DeserializeObject<List<ItemModel>>(responseString);
                var itemReceived = itemInArray[0];
                itemsList.Add(itemReceived);


            }

            await Navigation.PushAsync(new ResultsPage(itemsList));
        }
    }
}

