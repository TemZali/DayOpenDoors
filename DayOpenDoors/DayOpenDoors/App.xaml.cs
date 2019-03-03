using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DayOpenDoors
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            object zero;
            if (App.Current.Properties.TryGetValue("User", out zero))
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
