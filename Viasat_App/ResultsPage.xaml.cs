﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
using ItemType;


namespace Viasat_App
{
    public partial class ResultsPage : ContentPage
    {
        public ObservableCollection<Parameter> parametersList = new ObservableCollection<Parameter>();
        public string requestString;
        public string responseString;

        public ResultsPage(List<ItemModel> itemList)
        {
            InitializeComponent();
            ResultsListView.ItemsSource = itemList;
        }

        //START: BUTTONS EVENTS #######################################################

        private async void itemEntry_Tapped(object sender, ItemTappedEventArgs e)
        {
            //Creating an object of type ItemModel 
            ItemModel item = (ItemModel)((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;

            string itemId = item.id;
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

            requestString = jsonString;

            //Creating the http client which will provide us with the network capabilities
            using (var httpClient = new HttpClient())
            {
                //request string to be sent to the API
                var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

                //sending the previously created request to the api and waiting for a response that will be saved in the httpResponse var
                //  NOTE: if the api's base url changes this has to be modified.
                var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/searchbyid", httpContent);

                //verifying that response is not empty
                if (httpResponse.Content != null)
                {
                    //response into a usable var
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    //debugging
                    Console.WriteLine("JSON: " + requestString.ToUpper());
                    Console.WriteLine("POST: " + httpContent.ToString());
                    Console.WriteLine("GET: " + responseContent);

                    responseString = responseContent;
                }
            }

            var itemsList = JsonConvert.DeserializeObject<List<ItemModel>>(responseString);
            var itemReceived = itemsList[0];

            //calling the ItemPage into the stack and passing the selected item by the user
            await Navigation.PushAsync(new ItemPage(itemReceived));
        }

        //END: BUTTONS EVENTS #########################################################
    }
}