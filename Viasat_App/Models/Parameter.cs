using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Viasat_App
{
    public class Parameter
    {
        public string ParameterSelection { set; get; }

        public List<PickerItem> PickerItems { set; get; }

        public class PickerItem
        {
            public string Name { get; set; }
        }
    }
}