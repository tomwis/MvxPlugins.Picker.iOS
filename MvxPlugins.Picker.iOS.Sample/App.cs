using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.ViewModels;
using UIKit;

namespace MvxPlugins.Picker.iOS.Sample
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}