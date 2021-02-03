using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using OnboardingGame.Renderers;
using OnboardingGame.iOS.iOSRenderers;

[assembly: ExportRenderer(typeof(CutomProgressBar), typeof(CustomProgressBarRendereriOS))]
namespace OnboardingGame.iOS.iOSRenderers
{
    public class CustomProgressBarRendereriOS : ProgressBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) {

                Control.Layer.CornerRadius = 20;
                Control.Layer.BorderWidth = 3f;
                Control.Layer.BackgroundColor = Color.Gray.ToCGColor();

            }
        }
    }
}