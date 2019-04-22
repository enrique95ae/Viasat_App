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
            theUser.UserName = "Smith";
            theUser.PermissionLevel = 9;
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(theUser));

            //LOGIN CODE HERE ############################################################

            //if(UsernameEnteredExistsInDB() && UsernameEntered.Password == passwordEntry.Text)
            //{
            //    //LOGIN SUCCESS
            //    //RETRIEVE SESSION DATA FROM DB
            //    //POPULATE SESSION DATA IN THE APP
            //    //NAVIGATE TO HOME PAGE
            //}
            //else
            //{
            //    //LOGIN FAILURE
            //    //PROMPT LOGIN ERROR MESSAGE
            //    //INITIALIZE SECURITY PROTOCOL?
            //}

            //############################################################################
        }
    }
}