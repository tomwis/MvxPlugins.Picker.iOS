using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Windows.Input;
using CoreGraphics;
using MvvmCross.Binding.Bindings.Target.Construction;

namespace Mvx.Picker.iOS
{
    [Preserve(AllMembers = true)]
    public class Picker : UITextField
    {
        public UIPickerView PickerView { get; set; }
        public MvxPickerViewModelExtended PickerViewModel { get; set; }
        public ICommand ItemIconCommand { get => PickerViewModel.IconCommand; set => PickerViewModel.IconCommand = value; }
        public System.Collections.IEnumerable ItemsSource { get => PickerViewModel.ItemsSource; set => PickerViewModel.ItemsSource = value; }
        public event EventHandler<object> SelectedItemChanged;
        UIBarButtonItem _doneButton;
        public string DoneButtonTitle { get => _doneButton.Title; set => _doneButton.Title = value; }
        public object SelectedItem
        {
            get => PickerViewModel.SelectedItem;
            set
            {
                PickerViewModel.SelectedItem = value;
                Text = PickerViewModel.GetTitleFromItem(value);
                SelectedItemChanged?.Invoke(this, value);
            }
        }
        public string DisplayPropertyName
        {
            get => PickerViewModel.DisplayPropertyName;
            set => PickerViewModel.DisplayPropertyName = value;
        }

        public Picker()
        {
            PickerView = new UIPickerView();
            PickerViewModel = new MvxPickerViewModelExtended(this, PickerView);
            PickerView.Model = PickerViewModel;
            PickerView.ShowSelectionIndicator = true;

            var toolbar = new UIToolbar();
            toolbar.SizeToFit();
            _doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Plain, OnDone);
            toolbar.SetItems(new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                _doneButton
            }, false);
            toolbar.UserInteractionEnabled = true;

            InputView = PickerView;
            InputAccessoryView = toolbar;
            Delegate = new PickerTextFieldDelegate();
            RightView = new UIImageView(UIImage.FromFile("ic_keyboard_arrow_down_48pt.png").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)) { Frame = new CoreGraphics.CGRect(0, 0, 24, 24) };
            RightViewMode = UITextFieldViewMode.Always;
            TintColor = UIColor.Black;
        }

        private void OnDone(object sender, EventArgs e)
        {
            ResignFirstResponder();
        }

        // To hide caret at cursor's current position in text view
        public override CGRect GetCaretRectForPosition(UITextPosition position)
        {
            return CGRect.Empty;
        }

        private class PickerTextFieldDelegate : UITextFieldDelegate
        {
            // To disable editing of text view's text with keyboard
            public override bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
            {
                return false;
            }
        }
    }
}