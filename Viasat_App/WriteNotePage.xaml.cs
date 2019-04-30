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

            if(endpoint.Contains("/addnoteuser"))
            {
                newNote.belongs_to = Globals.TheUser._id;
            }
            else if(endpoint.Contains("/addnoteitem"))
            {
                newNote.belongs_to = belongsTo_itemId;
            }
            else
            {
                //who cares about this impossible scenario?
            }

            var jsonString = JsonConvert.SerializeObject(newNote,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            requestString = jsonString;

            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(requestString, Encoding.UTF8, "application/json");

                await httpClient.PostAsync(endpoint, httpContent);
            }    
        }
    }
}
