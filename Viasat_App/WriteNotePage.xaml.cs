using System;
using System.Collections.Generic;
using NoteType;
using globals;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class WriteNotePage : ContentPage
    {
        NoteModel newNote = new NoteModel();
        string endpoint;
        string belongsTo_itemId;

        string requestString;
        string responseString;

        public WriteNotePage(string endpointReceived, string itemId)
        {
            InitializeComponent();
            endpoint = endpointReceived;
            belongsTo_itemId = itemId;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            newNote.author = Globals.TheUser.name;
            newNote.author_id = Globals.TheUser._id;
            newNote.note = textEditor.Text;

            if(endpoint.Contains("/addnoteitem"))
            {
                newNote.belongs = Globals.TheUser._id;
            }
            else if(endpoint.Contains("/addnoteuser"))
            {
                newNote.belongs = belongsTo_itemId;
            }
            else
            {
                //who cares about this impossible scenario?
            }

            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

                await httpClient.PostAsync(endpoint, httpContent);
                //var httpResponse = await httpClient.PostAsync(endpoint, httpContent);

                //if(httpResponse.Content != null)
                //{
                //    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                //    responseString = responseContent;
                //}
            }

            
        }
    }
}
