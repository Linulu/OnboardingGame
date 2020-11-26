using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnboardingGame.Converters
{
    class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = (bool)value;
            if (v) {
                return Color.Green;
            }
            return Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color v = (Color)value;
            if (v.G == 255) {
                return true;
            }
            return false;
        }
    }
}
