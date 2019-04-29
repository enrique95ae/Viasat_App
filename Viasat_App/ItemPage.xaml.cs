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
        public ObservableCollection<string> componentsList = new ObservableCollection<string>();
        public string requestString;
        public string responseString;
        public ItemModel item;
        public ObservableCollection<ItemModel> itemsList = new ObservableCollection<ItemModel>();
 
        public ItemPage(ItemModel itemReceived)
        {
            InitializeComponent();
            item = itemReceived;
            populatePage(itemReceived);
            itemsList.Clear();

        }

        private async void noteTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentsPage());
        }

        private async void componentTapped(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
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