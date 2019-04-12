using System;
using System.Collections.Generic;
using ItemType;
using System.Linq;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class ItemPage : ContentPage
    {
        public ItemPage(ItemModel itemReceived)
        {
            InitializeComponent();
            item = itemReceived;
            populatePage(itemReceived);
        }

        public ItemModel item;
        public List<String> components_id_List = new List<string>();
        public string temp;

        //START: BUTTONS EVENTS #######################################################

        private async void componentsButton_Clicked(object sender, EventArgs e)
        {
            //removing unnecessary characters
            temp = item.components;
            temp = temp.Replace("[", "");
            temp = temp.Replace("]", "");
            components_id_List = temp.Split(',').ToList();

            await Navigation.PushAsync(new ComponentsPage(components_id_List));
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
            itemComponentsLabel.Text = itemReceived.components;
        }

    }
}