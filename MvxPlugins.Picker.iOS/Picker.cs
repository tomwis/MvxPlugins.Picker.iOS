using System;
using Foundation;
using UIKit;
using System.Windows.Input;
using CoreGraphics;
using System.Collections;
using System.Collections.Generic;

namespace MvxPlugins.Picker.iOS
{
    [Preserve(AllMembers = true)]
    public class Picker : UITextField
    {
        UIToolbar _toolbar;

        public UIPickerView PickerView { get; set; }
        public MvxPickerViewModelExtended PickerViewModel { get; set; }
        public ICommand ItemIconCommand { get => PickerViewModel.IconCommand; set => PickerViewModel.IconCommand = value; }
        public IEnumerable ItemsSource { get => PickerViewModel.ItemsSource; set => PickerViewModel.ItemsSource = value; }
        public PickerDisplayContentType ContentType { get => PickerViewModel.ContentType; set => PickerViewModel.ContentType = value; }
        public CGSize ContentImageSize { get; set; } = new CGSize(32, 32);
        public UIControlContentHorizontalAlignment ContentImageAlignment { get; set; } = UIControlContentHorizontalAlignment.Left;

        public event EventHandler<object> SelectedItemChanged;
        public object SelectedItem
        {
            get => PickerViewModel.SelectedItem;
            set
            {
                PickerViewModel.SelectedItem = value;

                var title = PickerViewModel.GetTitleFromItem(value);
                if (ContentType == PickerDisplayContentType.Text)
                {
                    Text = title;
                    LeftViewMode = UITextFieldViewMode.Never;
                }
                else
                {
                    LeftView = new UIImageView(new CGRect(0, 0, ContentImageSize.Width, ContentImageSize.Height))
                    {
                        Image = UIImage.FromBundle(title)?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                        ContentMode = 
                        (ContentImageAlignment == UIControlContentHorizontalAlignment.Center || ContentImageAlignment == UIControlContentHorizontalAlignment.Fill ? UIViewContentMode.Center : 
                        (ContentImageAlignment == UIControlContentHorizontalAlignment.Leading || ContentImageAlignment == UIControlContentHorizontalAlignment.Left ? UIViewContentMode.Left : UIViewContentMode.Right))
                    };
                    LeftViewMode = UITextFieldViewMode.Always;
                }

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

            _toolbar = new UIToolbar();
            _toolbar.SizeToFit();
            _toolbar.SetItems(new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, OnDone)
            }, false);
            _toolbar.UserInteractionEnabled = true;

            InputView = PickerView;
            InputAccessoryView = _toolbar;
            Delegate = new PickerTextFieldDelegate();

            CGRect rightViewFrame = new CGRect(0, 0, 24, 24);
            var rightView = new UIView(rightViewFrame);
            rightView.Add(
                new UIImageView(UIImage.FromFile("ic_keyboard_arrow_down_48pt.png")
                .ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate))
                {
                    Frame = rightViewFrame
                });
            RightView = rightView;
            RightViewMode = UITextFieldViewMode.Always;
            TintColor = UIColor.Black;
        }

        private void OnDone(object sender, EventArgs e)
        {
            ResignFirstResponder();
        }

        public void AddButtonToToolbar(UIBarButtonItem item)
        {
            var items = new List<UIBarButtonItem>(_toolbar.Items);
            items.Insert(1, new UIBarButtonItem(UIBarButtonSystemItem.FixedSpace) { Width = 12 }); // We need some spacing, because there is none on iOS <= 10
            items.Insert(1, item);
            _toolbar.SetItems(items.ToArray(), true);
        }

        // To hide caret at cursor's current position in text view
        public override CGRect GetCaretRectForPosition(UITextPosition position)
        {
            return CGRect.Empty;
        }

        // When we show image content instead of text we want it to take all space except for RigthView width
        public override CGRect LeftViewRect(CGRect forBounds)
        {
            var rect = base.LeftViewRect(forBounds);
            if (ContentImageAlignment == UIControlContentHorizontalAlignment.Left || ContentImageAlignment == UIControlContentHorizontalAlignment.Leading)
            {
                return rect;
            }
            return new CGRect(0, 0, forBounds.Width - RightView.Bounds.Width, forBounds.Height);
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