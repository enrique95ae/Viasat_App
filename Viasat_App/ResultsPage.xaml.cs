using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ItemType;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class ResultsPage : ContentPage
    {
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

            //calling the ItemPage into the stack and passing the selected item by the user
            await Navigation.PushAsync(new ItemPage(item));
        }

        //END: BUTTONS EVENTS #########################################################
    }
}