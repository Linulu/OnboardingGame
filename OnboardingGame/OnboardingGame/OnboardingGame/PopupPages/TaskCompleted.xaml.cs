using OnboardingGame.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCompleted : PopupPage
    {
        private Models.TaskItem task;

        public TaskCompleted(Models.TaskItem item)
        {
            InitializeComponent();

            task = item;
        }

        private async void OnBackground(object sender, EventArgs e) {
            await PopupNavigation.Instance.PopAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TitleLabel.Text = task.title;
            DescriptionLabel.Text = task.description;

            //3 second animation
            uint duration = 3000;

            await System.Threading.Tasks.Task.WhenAll(
                Image1.RotateTo(360, duration/3),
                Image1.FadeTo(1, duration/3)
            );

            await System.Threading.Tasks.Task.WhenAll(
                Image2.RotateTo(360, duration / 3),
                Image2.FadeTo(1, duration / 3)
            );

            await System.Threading.Tasks.Task.WhenAll(
                Image3.RotateTo(360, duration / 3),
                Image3.FadeTo(1, duration / 3)
            );

            await PopupNavigation.Instance.PopAsync();
        }
    }
}