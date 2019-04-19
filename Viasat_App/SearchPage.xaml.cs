using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
using ItemType;

//external info:
//https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
//https://putsreq.com/ZIailWh2iEVMAOP0RdGr/inspect  <---- for testing the http requests


namespace Viasat_App
{
    public partial class SearchPage : ContentPage
    {
        public ObservableCollection<Parameter> parametersList = new ObservableCollection<Parameter>();
        public string requestString;

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
        //  -API endpoint is created in a string according to the search parameters 
        //  -HTTP client is created and a POST request is sent (POST because the parameters are in the body instead of in the url)
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


            //======================================================================================================
            //======================================REQUEST CODE HERE===============================================
            //======================================================================================================
            createRequest(parametersList);
            Console.WriteLine(requestString);

            //Creating the http client which will provide us with the network capabilities
            using (var httpClient = new HttpClient())
            {
                //request string to be sent to the API
                var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

                //sending the previously created request to the api and waiting for a response that will be saved in the httpResponse var
                //  NOTE: if the api's base url changes this has to be modified.
                //var httpResponse = await httpClient.PostAsync("https://putsreq.com/ZIailWh2iEVMAOP0RdGr", httpContent);
                var httpResponse = await httpClient.PostAsync("http://52.13.18.254:3000/searchbyid", httpContent);

                //verifying that response is not empty
                if (httpResponse.Content != null)
                {
                    //response into a usable var
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    //DESERIALIZING CODE GOES HERE

                    //debugging lines
                    Console.WriteLine("JSON: " + requestString);
                    Console.WriteLine("POST: " + httpContent.ToString());
                    Console.WriteLine("GET: " + responseContent);
                }
            }



            //======================================================================================================
            //======================================================================================================
            //======================================================================================================

            //creating a http client to handle the async data retreival
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUri);

            //response stream into a string
            string jsonContent = await response.Content.ReadAsStringAsync();

            //parsing from json string to a list of objects of our item model type
            var itemsList = JsonConvert.DeserializeObject<List<ItemModel>>(jsonContent);

            //reset the parameters list for a future serch
            parametersList.Clear();

            //open the results page and pass the list of items to populate it
            await Navigation.PushAsync(new ResultsPage(itemsList));
        }

        void addParameterButton_Clicked(object sender, System.EventArgs e)
        {
            Parameter tempParam = new Parameter();

            tempParam.key = ParametersPicker.SelectedItem.ToString();
            tempParam.value = ParameterEntry.Text;

            if (!parametersList.Any(p => p.key == tempParam.key))
            {
                parametersList.Add(tempParam);
                ParameterEntry.Text = "";
            }
            else
            {
                DisplayAlert("Duplicate parameter", "The parameter has already been selected for this search.", "Ok");
            }
        }


        //PURPOSE: to use the parameters entered by the user and create a custom request json string
        //PARAMETERS: parameters list
        //ALGORITHM:
        public void createRequest(ObservableCollection<Parameter> list)
        {
            //temporary object which will hold all the search parameters
            //this object is to be serizlized into a json string to be sent as a request to the API
            ItemModel tempItem = new ItemModel();

            //Loop to go through all the parameters entered by the user and put them into the object's variables
            foreach(Parameter param in list)
            {
                if(param.key == "_id")
                {
                    tempItem.id = param.value;
                }
                else if(param.key == "item_number")
                {
                    tempItem.item_number = Convert.ToInt32(param.value);
                }
                else if(param.key == "revision")
                {
                    tempItem.revision = Convert.ToInt32(param.value);
                }
                else if(param.key == "description")
                {
                    tempItem.description = param.value;
                }
                else if(param.key == "part_type")
                {
                    tempItem.part_type = param.value;
                }
            }

            //item serialized to be sent as the request to the API
            //handles parameters not entered by the user, that way they are not included in the json string so the API doesn't have to parse and check for nulls.
            var jsonString = JsonConvert.SerializeObject(tempItem,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            //local string
            requestString = jsonString;
        }

        void Handle_DeleteParameter(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var parameterDelete = (Parameter)menuItem.CommandParameter;
            parametersList.Remove(parameterDelete);
        }
        //END: BUTTONS EVENTS #######################################################
    }
};
