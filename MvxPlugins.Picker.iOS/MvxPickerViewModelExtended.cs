using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using System.Windows.Input;
using System.Collections;
using System.Reflection;
using CoreGraphics;

namespace MvxPlugins.Picker.iOS
{
    [Preserve(AllMembers = true)]
    public class MvxPickerViewModelExtended : MvxPickerViewModel
    {
        public Picker Picker { get; }
        public string DisplayPropertyName { get; set; }
        public ICommand IconCommand { get; set; }
        public PickerDisplayContentType ContentType { get; set; } = PickerDisplayContentType.Text;

        public MvxPickerViewModelExtended(Picker picker, UIPickerView pickerView) : base(pickerView)
        {
            Picker = picker;
            SelectedItemChanged += MvxPickerViewModelExtended_SelectedItemChanged;
        }

        List<object> _items;
        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                _items = value?.Cast<object>()?.ToList();
                base.ItemsSource = value;
            }
        }

        private void MvxPickerViewModelExtended_SelectedItemChanged(object sender, EventArgs e)
        {
            Picker.SelectedItem = SelectedItem;
        }

        protected override string RowTitle(nint row, object item)
        {
            return GetTitleFromItem(item);
        }

        public string GetTitleFromItem(object item)
        {
            if (string.IsNullOrWhiteSpace(DisplayPropertyName) || item == null)
                return item?.ToString();

            var propNameTrimmed = DisplayPropertyName.Trim();
            var props = propNameTrimmed.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var currentItem = item;
            PropertyInfo currentProperty = null;
            foreach (var prop in props)
            {
                currentProperty = currentItem.GetType().GetTypeInfo().GetProperty(prop);
                var type = currentProperty?.GetValue(currentItem)?.GetType();
                var stringType = typeof(string);
                if (type.IsClass && !type.Equals(stringType))
                {
                    currentItem = currentProperty;
                }
            }
            return currentProperty?.GetValue(currentItem)?.ToString();
        }

        public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
        {
            if (ContentType == PickerDisplayContentType.Text)
            {
                return new UILabel { Text = GetTitleFromItem(_items[(int)row]), TextAlignment = UITextAlignment.Center };
            }
            else if (ContentType == PickerDisplayContentType.Image)
            {
                var filename = GetTitleFromItem(_items[(int)row]);
                return new UIImageView(new CGRect(0, 0, 24, 24)) { Image = UIImage.FromBundle(filename)?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), ContentMode = UIViewContentMode.ScaleAspectFit };
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                SelectedItemChanged -= MvxPickerViewModelExtended_SelectedItemChanged;
            }
        }
    }
}