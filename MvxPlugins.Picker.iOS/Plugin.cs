using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using UIKit;

namespace MvxPlugins.Picker.iOS
{
    [Preserve(AllMembers = true)]
    [MvxPluginAttribute]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            MvvmCross.Mvx.IoCProvider.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(RegisterTargetBindings);
        }

        private void RegisterTargetBindings(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<Picker>(MvxPickerSelectedItemBinding.PropertyName, view => new MvxPickerSelectedItemBinding(view));
        }
    }
}