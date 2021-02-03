using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnboardingGame.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListInfoPopup : PopupPage
    {
        public ListInfoPopup()
        {
            InitializeComponent();
        }
        async void OnOKButtonPress(object sender, EventArgs e) {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}