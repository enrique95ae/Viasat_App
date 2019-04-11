using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
//using ItemData;
using ItemType;

namespace Viasat_App
{
    public partial class SearchPage : ContentPage
    {
        //public List<ItemJson> itemsList = new List<ItemJson>();
        //public ItemJson itemDeserilized;

        public List<string> parametersList;
        string tempCompID;
        char tempChar;

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

            //from list of chars to list of ids(strings)
            //for (int i=0; i<itemsList.Count; i++)
            //{
            //    for (int j =0; j<itemsList[i].components.Length; j++)
            //    {
            //        tempChar = itemsList[i].components[j];
            //        if (tempChar != ',' && tempChar != '[' && tempChar != ']')
            //        {
            //            tempCompID += tempChar;
            //        }
            //    }
            //    itemsList[i].componentsIDs.Add(tempCompID);
            //    tempCompID = "";
            //}

            //open the results page and pass the list of items to populate it
            await Navigation.PushAsync(new ResultsPage(itemsList));
        }
        //END: BUTTONS EVENTS #######################################################
    }
};
