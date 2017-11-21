using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding;

namespace MvxPlugins.Picker.iOS
{
    [Preserve(AllMembers = true)]
    public class MvxPickerSelectedItemBinding : MvxConvertingTargetBinding
    {
        public static string PropertyName => "SelectedItem";
        public override Type TargetType => typeof(object);
        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;
        public Picker View => Target as Picker;
        bool _subscribed;

        public MvxPickerSelectedItemBinding(object target) : base(target)
        {
        }

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            if (View == null)
                return;

            _subscribed = true;
            View.SelectedItemChanged += View_SelectedItemChanged;
        }

        private void View_SelectedItemChanged(object sender, object e)
        {
            if (View == null)
                return;

            FireValueChanged(View.SelectedItem);
        }

        protected override void SetValueImpl(object target, object value)
        {
            if (View == null)
                return;

            View.SelectedItem = value;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                if (View != null && _subscribed)
                {
                    View.SelectedItemChanged -= View_SelectedItemChanged;
                    _subscribed = false;
                }
            }
        }
    }
}