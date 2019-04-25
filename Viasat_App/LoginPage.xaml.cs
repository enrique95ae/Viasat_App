using System;
using System.Collections.Generic;
using UserType;
using ItemType;


using Xamarin.Forms;

namespace Viasat_App
{
    public partial class LoginPage : ContentPage
    {
        UserModel theUser = new UserModel();

        public LoginPage()
        {
            InitializeComponent();
            theUser.UserName = "John";
            theUser.UserLast = "Smith";
            theUser.PermissionLevel = 9;

            theUser.RecentlyViewed = new System.Collections.ObjectModel.ObservableCollection<string>();

            globals.Globals.favoritesList = new List<string>();
            globals.Globals.favoritesItemsList = new List<ItemModel>();

        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            //======================================================================================================
            //======================================SUBSTITUTE WITH USER INFO REQUEST CODE==========================
            //======================================================================================================
            //======================================================================================================
            //======================================================================================================
            //======================================================================================================



            await Navigation.PushAsync(new MainPage(theUser));
        }
    }
}