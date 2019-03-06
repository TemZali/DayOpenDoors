using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DayOpenDoors
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (CrossSettings.Current.GetValueOrDefault("User", null)!=null)
            {
                MainPage = new MainPage(this);
            }
            else
            {
                MainPage = new NavigationPage(new CheckPage(this));
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
