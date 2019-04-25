using System;
using System.Collections.Generic;
using UserType;


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

            theUser.RecentlyViewed.Add("5cbe98938a8537a90101c64a");
            theUser.RecentlyViewed.Add("5cbe98938a8537a90101c64b");
            theUser.RecentlyViewed.Add("5cbe98938a8537a90101c64c");

            globals.Globals.favoritesList = new List<string>();


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