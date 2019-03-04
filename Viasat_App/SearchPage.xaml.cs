using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Viasat_App
{
    public partial class SearchPage : ContentPage
    {
        //public ObservableCollection<Parameter> ParameterList { get; set; }
        //public ObservableCollection<Picker> PickList { get; set; }
        public SearchPage()
        {
            InitializeComponent();
            //PopulateList();
            //List<ParameterModel> models = new List<ParameterModel>();
            List<Parameter> selection = new List<Parameter>();

            for (int i = 0; i < 3; i++)
            {
                selection.Add(new Parameter
                {
                    ParameterSelection = "Pick a Parameter",
                    PickerItems = new List<Parameter.PickerItem>
                        {
                         new Parameter.PickerItem {Name="Part I.D. #"},
                         new Parameter.PickerItem {Name="Description"},
                         new Parameter.PickerItem {Name="Application"},
                         new Parameter.PickerItem {Name="Level #"},
                         new Parameter.PickerItem {Name="Item"},
                         new Parameter.PickerItem {Name="Components"}
                        }
                });
            }
            ParameterListView.ItemsSource = selection;
        }
        //Populate the list

        //END of Populating initial list
        //ParameterListView.ItemsSource = ParameterList;

        //START: BUTTONS EVENTS #######################################################

        private async void resultsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultsPage());
        }

        //END: BUTTONS EVENTS #######################################################
    }
}

        /* SEARCHING ALGORITHM
         * 1: User enters a keword(s) in the entry field and selects parameters
         * 2: Upon click event the parameters and keywords are put into an object
         * 3: The object is serialized and sent trough the network
         * 4: Application waits for a response
         * 5: Once response data is received it is deserialized
         * 6: Deserialized data is parsed
         * 7: IF
         *      -Objects: data is put into objects and these into an ObservableCollection (list)
         *      -Error/Empty/Not found: [DEFINE HANDLING ACTION]
         * 8: Object is passed to the next page -> ResultsPage
         */

