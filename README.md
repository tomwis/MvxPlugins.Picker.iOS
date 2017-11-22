# What is it?
It is a picker for Xamarin.iOS for MvvmCross. It is using UITextField to display selected item and UIPickerView in UITextField's InputView (where the keybaord is) as a picker.

It was tested with MvvmCross 4.4. If you have issues with newer versions let me know.

# Download
Link to nuget: https://www.nuget.org/packages/MvxPlugins.Picker.iOS

# How to use
There's a sample project in the repo.  Here's also a small sample code:

```
var picker = new Picker() 
{ 
    DisplayPropertyName = nameof(PickerItem.DisplayName) 
};
Add(picker);

var set = this.CreateBindingSet<MyView, MyViewModel>();
set.Bind(picker).For(v => v.ItemsSource).To(vm => vm.PickerItems);
set.Bind(picker).For(v => v.SelectedItem).To(vm => vm.SelectedItem).TwoWay();
set.Apply();
```

# Features
- Binding for ItemsSource and SelectedItem
- DisplayPropertyName property - can be used to specify which property in our binded items should be displayed
- DoneButtonTitle property - for localization of "Done" button
- It inherits from UITextField and exposes UIPickerView as public property, so many things can be easily adjusted

# External Dependencies
- MvvmCross
- I also used arrow down icon from [material.io](https://material.io/icons/#ic_keyboard_arrow_down)
