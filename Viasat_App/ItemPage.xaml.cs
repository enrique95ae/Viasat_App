using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
using ItemType;
using NoteType;

namespace Viasat_App
{
    public partial class ItemPage : ContentPage
    {
        //since for this call the API returns an array with only 1 item in it, there is a need for a data structure capable of hold it so we can parse the json. 
        //the only item in the returned array will take index 0 of the collection and then will be extracted into an ItemModel object to be used.
        public ObservableCollection<ItemModel> itemsList = new ObservableCollection<ItemModel>();
        public ObservableCollection<string> componentsList = new ObservableCollection<string>(); //holds the components' IDs for this item
        public ObservableCollection<NoteModel> notesList = new ObservableCollection<NoteModel>(); //holds the notes' IDs written about this item

        public ItemModel item; //item to be used throughout the page

        public string requestString; //will hold the json string to be sent to the API
        public string responseString; //will hold the json string returned by the API

 
        public ItemPage(ItemModel itemReceived)
        {
            InitializeComponent();
            item = itemReceived; //making the item passed to this page global so it can be access by all the methods
            populatePage(item); //using the item to populate the page's fields
            itemsList.Clear();  //clearing the global list that will hold the info returned by the API

        }

        private async void noteTapped(object sender, EventArgs e)
        {
            string endpoint = "http://52.13.18.254:3000/addnoteitem";
            await Navigation.PushAsync(new CommentsPage(endpoint, item.id));
        }

        private async void componentTapped(object sender, SelectedItemChangedEventArgs e)
        {
            itemsList.Clear();
            for (int i = 0; i < item.componentsIDs.Count(); i++)
            {
                //string itemNum = item.componentsIDs[i];
                int itemNum = Int32.Parse(item.componentsIDs[i]);
                ItemModel tempItem = new ItemModel();
                tempItem.item_number = itemNum;

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

                    var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/partialobj", httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        responseString = responseContent;
                    }
                }

                var itemInArray = JsonConvert.DeserializeObject<ObservableCollection<ItemModel>>(responseString);
                var itemReceived = itemInArray[0];

                itemsList.Add(itemReceived);
            }
            //await Navigation.PushAsync(new ComponentsPage(componentsList));
            await Navigation.PushAsync(new ResultsPage(itemsList));
        }

        private async void infoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }

        private void favButton_Clicked(object sender, System.EventArgs e)
        {
            if (!globals.Globals.favoritesList.Contains(item.id))
            {
                globals.Globals.favoritesList.Add(item.id);
                favButton.Source = "FavImg.png";
            }
            else
            {
                globals.Globals.favoritesList.Remove(item.id);
                favButton.Source = "noFavImg.png";
            }
        }

        //populating the GUI with the received item's data.
        private void populatePage(ItemModel itemReceived)
        {
            //itemTitleLabel.Text = item.id;
            itemNumberLabel.Text = item.item_number.ToString();
            itemDescriptionLabel.Text = item.description;
            itemRevisionLabel.Text = item.revision.ToString();
            itemPartTypeLabel.Text = item.part_type;
            //itemComponentsLabel.Text = itemReceived.componentsIDs;
            componentsListview.ItemsSource = item.componentsIDs;
            componentsList = item.componentsIDs;

            //set the favorite button depending if the item is already in the favorites list.
            if (globals.Globals.favoritesList.Contains(item.id))
            {
                favButton.Source = "FavImg.png";
            }
            else
            {
                favButton.Source = "noFavImg.png";
            }
        }

    }
}