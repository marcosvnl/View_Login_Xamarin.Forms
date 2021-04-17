using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_FirebaseCrud.Views;

namespace XF_FirebaseCrud
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
