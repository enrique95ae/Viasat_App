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
        UserModel user = new UserModel();
        ItemModel item = new ItemModel();
        string responseString;
        string requestString;


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
            //nothing

        }
    }
}

