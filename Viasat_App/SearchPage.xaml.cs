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
            string endpointSt = "http://enriqueae.com/ViasatTest/json2.json";
            Uri apiUri = new Uri(endpointSt);

            //creating a http client to handle the async data retreival
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUri);

            //response stream into a string
            string jsonContent = await response.Content.ReadAsStringAsync();

            //var itemsList = JsonConvert.DeserializeObject<List<ItemJson>>(jsonContent);
            //var itemJson = ItemJson.FromJson(jsonContent);
            var itemsList = JsonConvert.DeserializeObject<List<ItemModel>>(jsonContent);

            //for(int i=0; i<itemsList.Count(); i++)
            //{
            //    Console.WriteLine("The number of elements in the json is: " + itemsList.Count());
            //    Console.WriteLine("Item number: " + itemsList[i].item_number);
            //    Console.WriteLine("Description: " + itemsList[i].description);
            //    Console.WriteLine("Part type: " + itemsList[i].part_type);
            //    Console.WriteLine("Permission lvl: " + itemsList[i].permission_level);
            //    Console.WriteLine("Revison: " + itemsList[i].revision);

            //    for(int j=0; j<itemsList[i].components.Count(); j++)
            //    {
            //        Console.WriteLine("Component #" + j + ": " + itemsList[i].components[j]);
            //    }
            //}


            await Navigation.PushAsync(new ResultsPage(itemsList));
        }
        //END: BUTTONS EVENTS #######################################################
    }
}

