using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartShop.Controls
{
    public class SkeletonView : BoxView
    {
        public SkeletonView()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                this.FadeTo(0.8, 750, Easing.CubicInOut).ContinueWith((x) =>
                {
                    this.FadeTo(1, 750, Easing.CubicInOut);
                });

                return true;
            });
        }
    }
}
