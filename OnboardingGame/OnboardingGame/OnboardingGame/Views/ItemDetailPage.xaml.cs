using System.ComponentModel;
using Xamarin.Forms;
using OnboardingGame.ViewModels;

namespace OnboardingGame.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}