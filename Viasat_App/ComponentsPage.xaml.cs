using System;
using System.Collections.Generic;
using ItemType;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class ComponentsPage : ContentPage
    {
        public ComponentsPage(List<string> componentsList)
        {
            //ComponentsListView.ItemsSource = componentsList;
            InitializeComponent();
            ComponentsListView.ItemsSource = componentsList;
        }
    }
}
