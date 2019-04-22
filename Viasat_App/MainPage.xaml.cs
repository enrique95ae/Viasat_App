using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using UserType;

namespace Viasat_App
{
    public partial class MainPage : ContentPage
    {
        UserModel theUser;

        public MainPage(UserModel user)
        {
            theUser = user;
            InitializeComponent();
        }

        //START: BUTTONS EVENTS ########################################################

        private async void searchButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void profileButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(theUser));
        }


        //END: BUTTONS EVENTS ##########################################################


        //PAGE ALGORITHM:
        /*
         * 1- Page receives the username from the login page (if this page it's pushed onto the pages stack it means auth was successful)
         * 2- Page sends username and requests said username's AccessLevel
         * 3- Page receives the user's Access level and sets the global variable to that value.
         */

        private void SetUserAccessLevel()
        {
            //code here
        }

    }
}