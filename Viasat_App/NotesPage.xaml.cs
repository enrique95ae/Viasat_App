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
        string endpoint = "http://52.13.18.254:3000/addnoteitem";
        string theId;


        public CommentsPage(string idReceived, ObservableCollection<NoteModel> notes)
        {

            InitializeComponent();
            NoteList = notes;
            CommentsListView.ItemsSource = NoteList;
            theId = idReceived;
        }

        private async void noteEntry_Tapped(object sender, EventArgs e)
        {
            NoteModel noteClicked = (NoteModel)((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;

            Console.WriteLine("note.author: " + noteClicked.author);
            Console.WriteLine("note.note: " + noteClicked.note);
            Console.WriteLine("note.date: " + noteClicked.date);

            await Navigation.PushAsync(new CommentPage(noteClicked));
        }

        private async void newNoteButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new WriteNotePage(endpoint, theId));
        }
    }
}