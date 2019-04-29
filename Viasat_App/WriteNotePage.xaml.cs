using System;
using System.Collections.Generic;
using NoteType;
using globals;

using Xamarin.Forms;

namespace Viasat_App
{
    public partial class WriteNotePage : ContentPage
    {
        NoteModel newNote = new NoteModel();
        string endpoint;
        string belongsTo_itemId;

        public WriteNotePage(string endpointReceived, string itemId)
        {
            InitializeComponent();
            endpoint = endpointReceived;
            belongsTo_itemId = itemId;
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
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
        }

    }
}
