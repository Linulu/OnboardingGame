using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace OnboardingGame.Converters
{
    class StatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = (int)value;

            if (Application.Current.RequestedTheme == OSAppTheme.Light)
            {
                if (v < 0)
                {
                    
                    return "light_play_button.png";
                }
                else if (v == 0)
                {
                    return "light_hourglass.png";
                }
                else if (v > 0)
                {
                    return "light_confirmed.png";
                }
            }
            else if (Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                if (v < 0)
                {
                    return "dark_play_button.png";
                }
                else if (v == 0)
                {
                    return "dark_hourglass.png";
                }
                else if (v > 0)
                {
                    return "dark_confirmed.png";
                }
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
