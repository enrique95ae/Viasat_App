using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
using ItemType;

namespace Viasat_App
{
    public partial class SearchPage : ContentPage
    {

        public struct parameter
        {
            public string key;
            public string value;
        }


        public ObservableCollection<parameter> parametersList = new ObservableCollection<parameter>();



        public SearchPage()
        {
            InitializeComponent();
            ParameterListView.ItemsSource = parametersList;
        }


        //START: BUTTONS EVENTS #######################################################

        //PURPOSE: upon user click the app sends a request to the API and waits for a response and handles the incoming data accordingly:
        //PARAMETERS: navigation params
        //ALGORITHM:
        //  -Button is clicked
        //  -API endpoint is created in a string according to the search parameters <-----  NOT YET
        //  -HTTP client is created
        //  -App waits for a response in a JSON format
        //  -Response is checked for errors. If success data is parsed into an object of type: ItemData   <----- NOT YET
        private async void resultsButton_Clicked(object sender, EventArgs e)
        {
            /*
             * Logic for building a custom request based on the search parameters selected goes here.
             * As of right now the search is harcoded so we have a working api connection, data retreival and parsing.
             * Needs to be implemented in order to make searches dynamic.
             */

            //setting up endpoint
            string endpointSt = "http://enriqueae.com/ViasatTest/json2.json";
            Uri apiUri = new Uri(endpointSt);

            //creating a http client to handle the async data retreival
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUri);

            //response stream into a string
            string jsonContent = await response.Content.ReadAsStringAsync();

            //parsing from json string to a list of objects of our item model type
            var itemsList = JsonConvert.DeserializeObject<List<ItemModel>>(jsonContent);

            //open the results page and pass the list of items to populate it
            await Navigation.PushAsync(new ResultsPage(itemsList));
        }


        void addParameterButton_Clicked(object sender, System.EventArgs e)
        {

            parameter tempParam = new parameter();

            tempParam.key = ParametersPicker.SelectedItem.ToString();
            tempParam.value = ParameterEntry.Text;

            if (!parametersList.Any(p => p.key == tempParam.key))
            {
                parametersList.Add(tempParam);
                Console.WriteLine(tempParam.key);
                Console.WriteLine(tempParam.value);
            }
            else
            {
                DisplayAlert("Duplicate parameter", "The parameter has already been selected for this search.", "Ok");
            }
        }

        //END: BUTTONS EVENTS #######################################################
    }
};
