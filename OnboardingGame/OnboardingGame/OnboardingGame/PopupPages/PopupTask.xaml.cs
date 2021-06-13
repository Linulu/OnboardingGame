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
    public partial class PopupTask : PopupPage
    {
        private TaskItem task;

        public PopupTask(TaskItem item)
        {
            InitializeComponent();

            task = item;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Title_Label.Text = task.title + " (" + task.points + ": Stars)";

            Description_Label.Text = task.description;

            iButton.Command = new Command(OnCancel);

            if (task.status < 0)
            {
                BorderFrame.BorderColor = Color.Red;
                DescriptionFrame.BorderColor = Color.Red;
                BoxColor.Color = Color.Red;
            }
            else if (task.status == 0)
            {
                BorderFrame.BorderColor = Color.DeepSkyBlue;
                DescriptionFrame.BorderColor = Color.DeepSkyBlue;
                BoxColor.Color = Color.DeepSkyBlue;
            }
            else
            {
                BorderFrame.BorderColor = Color.Green;
                DescriptionFrame.BorderColor = Color.Green;
                BoxColor.Color = Color.Green;
            }
        }

        private async void OnCancel() {
            await PopupNavigation.Instance.PopAsync();
            App.ObserverUpdate();
        }

        async void OnStartButtonClicked(object sender, EventArgs e)
        {
            if (task.status < 0)
            {
                bool answer = await DisplayAlert("Hold up!", "Are you sure you wish to brave the dangers that this misson entails? To take on the challenges this misson brings? If so go ahead and make your choice.", "Yes", "No");
                if (answer)
                {
                    task.UpdateStatus(0);
                    await App.UpdateTask(task);
                    await App.Database.SaveItemAsync(task);
                    await PopupNavigation.Instance.PopAsync();
                    App.ObserverUpdate();
                }
            }
            else if (task.status > 0)
            {
                await DisplayAlert("Misson Accomplished", "No need to start something you've already finished right? How about you jump on the next misson instead?", "Return");
            }
        }

        async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            if (task.status <= 0)
            {
                bool answer = await DisplayAlert("Hold up!", "Once you pass the finish line there's no turning back. Are you sure you've done it all? Left no stone unturned? What's your answer?", "Yes", "No");
                if (answer)
                {
                    task.UpdateStatus(1);
                    PlayerProfile pp = await App.Database.GetPlayerProfile();
                    pp.AddPoints(task.points);
                    await App.UpdateTask(task);
                    await App.Database.SavePlayerAsync(pp);
                    await App.Database.SaveItemAsync(task);
                    await PopupNavigation.Instance.PopAsync();
                    App.ObserverUpdate();
                    await PopupNavigation.Instance.PushAsync(new TaskCompleted(task));
                }
            }
            else if (task.status > 0)
            {
                await DisplayAlert("Misson Accomplished", "I like your enthusiasm but you can't complete something that's already finished. How about you move on to the next misson already?", "Return");
            }
        }
    }
}