using System;
using System.Collections.Generic;
using MvxPlugins.Picker.iOS.Sample.Models;
using MvvmCross.ViewModels;

namespace MvxPlugins.Picker.iOS.Sample.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        public FirstViewModel()
        {
            PickerTextItems = new List<PickerItem>
            {
                new PickerItem { DisplayName = "Item 1" , Value = 1 },
                new PickerItem { DisplayName = "Item 2" , Value = 2 },
                new PickerItem { DisplayName = "Item 3" , Value = 3 },
                new PickerItem { DisplayName = "Item 4" , Value = 4 }
            };
            SelectedTextItem = PickerTextItems[0];

            PickerImageItems = new List<PickerItem>
            {
                new PickerItem { DisplayName = "AlarmIcon" , Value = 1 },
                new PickerItem { DisplayName = "AlarmAddIcon" , Value = 2 },
                new PickerItem { DisplayName = "ic_alarm_on.png" , Value = 3 },
                new PickerItem { DisplayName = "ic_alarm_off.png" , Value = 4 }
            };
            SelectedImageItem = PickerImageItems[0];
        }

        private string _hello = "Hello! It's a Mvx Picker Example.";
        public string Hello
        {
            get { return _hello; }
            set { SetProperty(ref _hello, value); }
        }
        
        private List<PickerItem> _pickerTextItems;
        public List<PickerItem> PickerTextItems
        {
            get { return _pickerTextItems; }
            set { SetProperty(ref _pickerTextItems, value); }
        }

        private PickerItem _selectedTextItem;
        public PickerItem SelectedTextItem
        {
            get { return _selectedTextItem; }
            set
            {
                SetProperty(ref _selectedTextItem, value);
                RaisePropertyChanged(nameof(SelectedTextItemFormatted));
            }
        }

        public string SelectedTextItemFormatted => $"Selected item: {_selectedTextItem?.DisplayName} ({_selectedTextItem?.Value})";
        
        private List<PickerItem> _pickerImageItems;
        public List<PickerItem> PickerImageItems
        {
            get { return _pickerImageItems; }
            set { SetProperty(ref _pickerImageItems, value); }
        }

        private PickerItem _selectedImageItem;
        public PickerItem SelectedImageItem
        {
            get { return _selectedImageItem; }
            set { SetProperty(ref _selectedImageItem, value); }
        }
    }
}