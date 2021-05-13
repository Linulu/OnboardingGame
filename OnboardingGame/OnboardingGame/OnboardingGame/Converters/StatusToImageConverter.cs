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

            if (v < 0)
            {
                return "TransparentStart.png";
            }
            else if (v == 0)
            {
                return "TransparentOngoing.png";
            }
            else if (v > 0)
            {
                return "Clear_Star_Formation.png";
            }

            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
