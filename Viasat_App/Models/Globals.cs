namespace globals
{
    using Xamarin.Forms;
    using UserType;
    using ItemType;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Globals
    {
        public static UserModel TheUser { get; set; }
        public static ObservableCollection<ItemModel> recentlyViewedList { get; set; }
        public static ObservableCollection<string> favoritesList { get; set; }
        public static ObservableCollection<ItemModel> favoritesItemsList { get; set; }
    }

}