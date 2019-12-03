using System;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvxPlugins.Picker.iOS.Sample.Models;
using MvxPlugins.Picker.iOS.Sample.ViewModels;
using UIKit;

namespace MvxPlugins.Picker.iOS.Sample.Views
{
    [Register(nameof(FirstView))]
    public class FirstView : MvxViewController<FirstViewModel>
    {
        Picker _pickerText;
        Picker _pickerImage;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = true;
            View.BackgroundColor = UIColor.White;

            var headerLabel = new UILabel();
            headerLabel.TextColor = UIColor.Black;
            View.Add(headerLabel);

            _pickerText = new Picker()
            {
                DisplayPropertyName = nameof(PickerItem.DisplayName)
            };
            _pickerText.AddButtonToToolbar(new UIBarButtonItem(UIBarButtonSystemItem.Cancel, OnCancel));
            Add(_pickerText);

            var selectedTextItem = new UILabel();
            selectedTextItem.TextColor = UIColor.Black;
            View.Add(selectedTextItem);

            _pickerImage = new Picker()
            {
                DisplayPropertyName = nameof(PickerItem.DisplayName),
                ContentType = PickerDisplayContentType.Image, // Default is PickerDisplayContentType.Text
                ContentImageSize = new CGSize(32, 32) // Default is 32x32
            };
            _pickerImage.PickerView.TintColor = UIColor.Purple;
            _pickerImage.TintColor = UIColor.Magenta;
            Add(_pickerImage);

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                if (_pickerText.CanResignFirstResponder)
                    _pickerText.ResignFirstResponder();

                if (_pickerImage.CanResignFirstResponder)
                    _pickerImage.ResignFirstResponder();
            }));

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(headerLabel).To(vm => vm.Hello);
            set.Bind(_pickerText).For(v => v.ItemsSource).To(vm => vm.PickerTextItems);
            set.Bind(_pickerText).For(v => v.SelectedItem).To(vm => vm.SelectedTextItem).TwoWay();
            set.Bind(_pickerImage).For(v => v.ItemsSource).To(vm => vm.PickerImageItems);
            set.Bind(_pickerImage).For(v => v.SelectedItem).To(vm => vm.SelectedImageItem).TwoWay();
            set.Bind(selectedTextItem).To(vm => vm.SelectedTextItemFormatted);
            set.Apply();

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                headerLabel.AtTopOf(View, 32),
                headerLabel.AtLeftOf(View, 12),
                headerLabel.AtRightOf(View, 12),

                _pickerText.AtLeftOf(View, 12),
                _pickerText.Below(headerLabel, 12),

                selectedTextItem.AtLeftOf(View, 12),
                selectedTextItem.Below(_pickerText, 12),

                _pickerImage.AtLeftOf(View, 12),
                _pickerImage.Below(selectedTextItem, 12)
                );

        }

        private void OnCancel(object sender, EventArgs e)
        {
            _pickerText.ResignFirstResponder();
        }
    }
}
