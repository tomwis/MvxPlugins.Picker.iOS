using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.Core.ViewModels;

namespace MvxPlugins.Picker.iOS.Sample.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        public FirstViewModel()
        {
            PickerItems = new List<PickerItem>
            {
                new PickerItem { DisplayName = "Item 1" , Value = 1 },
                new PickerItem { DisplayName = "Item 2" , Value = 2 },
                new PickerItem { DisplayName = "Item 3" , Value = 3 },
                new PickerItem { DisplayName = "Item 4" , Value = 4 }
            };
            SelectedItem = PickerItems[0];
        }

        private string _hello = "Hello! It's a Mvx Picker Example.";
        public string Hello
        {
            get { return _hello; }
            set { SetProperty(ref _hello, value); }
        }
        
        private List<PickerItem> _pickerItems;
        public List<PickerItem> PickerItems
        {
            get { return _pickerItems; }
            set { SetProperty(ref _pickerItems, value); }
        }

        private PickerItem _selectedItem;
        public PickerItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                RaisePropertyChanged(nameof(SelectedItemFormatted));
            }
        }

        public string SelectedItemFormatted => $"Selected item: {_selectedItem?.DisplayName} ({_selectedItem?.Value})";
    }
}