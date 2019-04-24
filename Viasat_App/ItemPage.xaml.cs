using System;
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
    public partial class ItemPage : ContentPage
    {

        public List<string> componentsList = new List<string>();
        public string requestString;
        public string responseString;
        public ItemModel item;
        public List<ItemModel> itemsList = new List<ItemModel>();
 
        public ItemPage(ItemModel itemReceived)
        {
            InitializeComponent();
            item = itemReceived;
            populatePage(itemReceived);
        }

        //START: BUTTONS EVENTS #######################################################

        private async void componentsButton_Clicked(object sender, EventArgs e)
        {
            for (int i = 0; i < item.componentsIDs.Count(); i++)
            {
                //string itemNum = item.componentsIDs[i];
                int itemNum = item.item_number.GetValueOrDefault();
                ItemModel tempItem = new ItemModel();
                tempItem.item_number = itemNum;

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
                    var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/partialobj", httpContent);

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
        
            //await Navigation.PushAsync(new ComponentsPage(componentsList));
            await Navigation.PushAsync(new ResultsPage(itemsList));
        }

        private async void commentsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentsPage());
        }

        private async void infoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }

        //END: BUTTONS EVENTS #######################################################


        //populating the GUI with the received item's data.
        private void populatePage(ItemModel itemReceived)
        {
            itemTitleLabel.Text = itemReceived.id;
            itemNumberLabel.Text = itemReceived.item_number.ToString();
            itemDescriptionLabel.Text = itemReceived.description;
            itemRevisionLabel.Text = itemReceived.revision.ToString();
            itemPartTypeLabel.Text = itemReceived.part_type;
            //itemComponentsLabel.Text = itemReceived.componentsIDs;
            componentsListview.ItemsSource = itemReceived.componentsIDs;
            componentsList = itemReceived.componentsIDs;
        }

    }
}