using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.Binding.Bindings.Target.Construction;

namespace Mvx.Picker.iOS
{
    [Preserve(AllMembers = true)]
    public class PluginLoader : MvvmCross.Platform.Plugins.IMvxPluginLoader
    {
        public static readonly PluginLoader Instance = new PluginLoader();

        public void EnsureLoaded()
        {
            var manager = MvvmCross.Platform.Mvx.Resolve<MvvmCross.Platform.Plugins.IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();

            MvvmCross.Platform.Mvx.CallbackWhenRegistered<MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry>(RegisterTargetBindings);
        }

        private void RegisterTargetBindings(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<Picker>(MvxPickerSelectedItemBinding.PropertyName, view => new MvxPickerSelectedItemBinding(view));
        }
    }
}