using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnboardingGame.Converters
{
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int p = (int)value;
            Color c = new Color();
            if (p < 0)
            {
                c = Color.Red;
            }
            else if (p == 0)
            {
                c = Color.Yellow;
            }
            else if (p > 0) {
                c = Color.Green;
            }
            return c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            Color c = (Color)value;
            int p = 0;

            if (c.R == 255)
            {
                p = -1;
            }
            else if (c == Color.Yellow)
            {
                p = 0;
            }
            else if (c.G == 255) {
                p = 1;
            }

            return p;
        }
    }
}
