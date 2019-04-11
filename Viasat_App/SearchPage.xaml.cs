using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
using ItemData;

namespace Viasat_App
{
    public partial class SearchPage : ContentPage
    {
        public ObservableCollection<ItemJson> itemsList = new ObservableCollection<ItemJson>();

        public List<string> parametersList;

        public SearchPage()
        {
            InitializeComponent();
            //PopulateList();

            parametersList = new List<string>();

            parametersList.Add("Part I.D. #");
            parametersList.Add("Description");

        }

        //START: BUTTONS EVENTS #######################################################

        //PURPOSE: upon user click the app sends a request to the API and waits for a response and handles the incoming data accordingly:
        //PARAMETERS: navigation params
        //ALGORITHM:
        //  -Button is clicked
        //  -API endpoint is created in a string according to the search parameters
        //  -HTTP client is created
        //  -App waits for a response in a JSON format
        //  -Response is checked for errors. If success data is parsed into an object of type: ItemData
        private async void resultsButton_Clicked(object sender, EventArgs e)
        {
            /*
             * Logic for building a custom request based on the search parameters selected goes here.
             * As of right now the search is harcoded so we have a working api connection, data retreival and parsing.
             * Needs to be implemented in order to make searches dynamic.
             */

            //setting up endpoint
            string endpointSt = "http://enriqueae.com/ViasatTest/itemsJson.json";
            Uri apiUri = new Uri(endpointSt);

            //creating a http client to handle the async data retreival
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUri);

            //response stream into a string
            string jsonContent = await response.Content.ReadAsStringAsync();

            itemsList = JsonConvert.DeserializeObject<ObservableCollection<ItemJson>>(jsonContent);

            await Navigation.PushAsync(new ResultsPage());
        }


        //END: BUTTONS EVENTS #######################################################
    }
}


