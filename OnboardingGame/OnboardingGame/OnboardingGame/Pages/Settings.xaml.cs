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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        async void OnButtonPress(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Reset", "Do you really wish to reset all the data?", "Yes", "No");

            if (answer)
            {
                //Resets the status portion of every TaskItem in the TaskItem table
                List<TaskItem> items = await App.Database.GetTaskItem();

                foreach (var item in items)
                {
                    item.Status = -1;
                    await App.Database.SaveItemAsync(item);
                }
            }
        }
    }
}