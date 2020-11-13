using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;
using OnboardingGame.Models;

namespace OnboardingGame.Converters
{
    class CompletionToProgressConverted : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<TaskItem> items = (List<TaskItem>)value;
            double progress = 0;

            for (int i = 0; i < items.Count; i++) {
                if (items[i].Status > 0) {
                    progress++;
                }
            }

            return progress/items.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0.0;
        }
    }
}
