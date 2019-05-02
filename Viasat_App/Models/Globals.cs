using UserType;
using ItemType;
using System.Collections.ObjectModel;

namespace globals
{
    /*
     *  This file contains all the global variables used throught the app
     *  All this lists are filled upon successful login
     */

    public class Globals
    {
        public static UserModel TheUser { get; set; }
        public static ObservableCollection<ItemModel> recentlyViewedList { get; set; }
        public static ObservableCollection<string> favoritesList { get; set; } //ids
        public static ObservableCollection<ItemModel> favoritesItemsList { get; set; } //whole objects
    }

}