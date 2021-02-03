using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using OnboardingGame.Renderers;
using OnboardingGame.Droid.AndroidRenderers;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CutomProgressBar), typeof(CustomProgressBarRendererAndroid))]
namespace OnboardingGame.Droid.AndroidRenderers
{
    public class CustomProgressBarRendererAndroid : ProgressBarRenderer
    {
        public CustomProgressBarRendererAndroid(Context context) : base(context) {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null) { 
                
                var backgroundGradient = new GradientDrawable();
                backgroundGradient.SetCornerRadius(20);
                backgroundGradient.SetColor(Android.Graphics.Color.Gray);
                Control.SetBackground(backgroundGradient);

                var progressGradient = new GradientDrawable();
                progressGradient.SetCornerRadius(20);
                progressGradient.SetColor(Android.Graphics.Color.DeepSkyBlue);
                progressGradient.SetStroke(0, Android.Graphics.Color.White);
                
                ScaleDrawable scale = new ScaleDrawable(progressGradient, GravityFlags.NoGravity, 0, 0);
                
                ScaleDrawable scale1 = new ScaleDrawable(progressGradient, GravityFlags.Start, ScaleX, Height);
                scale1.SetVisible(true, true);

                scale.SetCallback(scale1);
                scale1.SetCallback(scale);
                LayerDrawable layer = new LayerDrawable(new Drawable[] {scale, scale1});
                
                Control.SetProgressDrawableTiled(layer);

                //ScaleDrawable current = (ScaleDrawable)((LayerDrawable)Control.ProgressDrawable).GetDrawable(2);
                //Console.WriteLine(current)
            }
        }
    }
}