using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class WriteNotePage : ContentPage
    {
        public WriteNotePage()
        {
            InitializeComponent();
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            //File.WriteAllText(_fileName, _editor.Text);
        }

    }
}
