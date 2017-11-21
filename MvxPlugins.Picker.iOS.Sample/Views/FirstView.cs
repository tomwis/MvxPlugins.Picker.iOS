using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvxPlugins.Picker.iOS.Sample.ViewModels;
using UIKit;

namespace MvxPlugins.Picker.iOS.Sample.Views
{
    [Register(nameof(FirstView))]
    public class FirstView : MvxViewController<FirstViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBarHidden = true;
            View.BackgroundColor = UIColor.White;

            var headerLabel = new UILabel();
            headerLabel.TextColor = UIColor.Black;
            View.Add(headerLabel);

            var picker = new Picker() { DisplayPropertyName = nameof(PickerItem.DisplayName) };
            Add(picker);

            View.AddGestureRecognizer(new UITapGestureRecognizer(() => picker.ResignFirstResponder()));

            var selectedItem = new UILabel();
            selectedItem.TextColor = UIColor.Black;
            View.Add(selectedItem);

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(headerLabel).To(vm => vm.Hello);
            set.Bind(picker).For(v => v.ItemsSource).To(vm => vm.PickerItems);
            set.Bind(picker).For(v => v.SelectedItem).To(vm => vm.SelectedItem).TwoWay();
            set.Bind(selectedItem).To(vm => vm.SelectedItemFormatted);
            set.Apply();

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                headerLabel.AtTopOf(View, 32),
                headerLabel.AtLeftOf(View, 12),
                headerLabel.AtRightOf(View, 12),
                picker.AtLeftOf(View, 12),
                picker.Below(headerLabel, 12),
                selectedItem.AtLeftOf(View, 12),
                selectedItem.Below(picker, 12)
                );

        }
    }
}
