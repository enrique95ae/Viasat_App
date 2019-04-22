using System;
using System.Collections.Generic;
using UserType;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(UserModel theUser)
        {
            InitializeComponent();

            nameLabel.Text = theUser.UserName;
            lastLabel.Text = theUser.UserLast;
            permissionLabel.Text = theUser.PermissionLevel.ToString();
        }
    }
}
