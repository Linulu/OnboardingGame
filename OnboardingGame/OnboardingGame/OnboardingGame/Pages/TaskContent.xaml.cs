using OnboardingGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskContent : ContentPage
    {
        private static Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        TaskItem task = null;

        public TaskContent()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            task = (TaskItem)BindingContext;

            if (linkParser.Match(task.Description).Success)
            {
                string s = task.Description;
                Description_Label.FormattedText = s.Replace(linkParser.Match(task.Description).Value, "");
                Description_Label.FormattedText.Spans.Add(CreateSpan(linkParser.Match(task.Description).Value));
            }
            else {
                Description_Label.FormattedText = task.Description;
            }

            if (task.Status < 0)
            {
                StatusBar.Color = Color.Red;
            }
            else if (task.Status == 0)
            {
                StatusBar.Color = Color.Yellow;
            }
            else {
                StatusBar.Color = Color.Green;
            }
        }

        async void OnStartButtonClicked(object sender, EventArgs e) {
            task.Status = 0;
            await App.Database.SaveItemAsync(task);
            await Navigation.PopAsync();
        }
        async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            task.Status = 1;
            await App.Database.SaveItemAsync(task);
            await Navigation.PopAsync();
        }

        private Span CreateSpan(string url) {

            var span = new Span()
            {
                Text = url
            };

            span.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = _navigationCommand,
                CommandParameter = url
            });
            span.TextColor = Color.Blue;

            return span;
        }

        private ICommand _navigationCommand = new Command<string>((url) => {
            Device.OpenUri(new Uri(url));
        });
    }
}