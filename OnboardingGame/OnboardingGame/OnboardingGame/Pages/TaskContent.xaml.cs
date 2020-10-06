using OnboardingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskContent : ContentPage
    {
        public TaskContent()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var task = (TaskItem)BindingContext;
        }

        private Span CreateSpan(TaskItem context) {

            var span = new Span();

            span.GestureRecognizers.Add(new TapGestureRecognizer() {
                //Command = navigationCommand
                //CommandParameter = context.link
            });

            return span;
        }
    }
}