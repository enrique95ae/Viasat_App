using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NoteType;



using Xamarin.Forms;


namespace Viasat_App
{
    public partial class CommentsPage : ContentPage
    {

        public ObservableCollection<NoteModel> NoteList { set; get; }
        string endpoint;
        string Id;


        public CommentsPage(string endpointReceived, string idReceived)
        {
            InitializeComponent();
            endpoint = endpointReceived;
            Id = idReceived;
            populateList();
        }



        private async void commentEntry_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentPage());
        }

        private async void newNoteButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new WriteNotePage(endpoint, Id));
        }

        private void populateList()
        {
            NoteList = new ObservableCollection<NoteModel>()
            {
                new NoteModel()
                {
                    note = "comment1title",
                    belongs = "",
                    date = "02/19/2019",
                    author_id = "Author1"
                },

                new NoteModel()
                {
                    note = "comment2title",
                    belongs = "",
                    date = "02/19/2019",
                    author_id = "Author2"
                },

                new NoteModel()
                {
                    note = "comment2title",
                    belongs = "",
                    date = "02/19/2019",
                    author_id = "Author2"
                },
            };
            CommentsListView.ItemsSource = NoteList;
        }
    }
}