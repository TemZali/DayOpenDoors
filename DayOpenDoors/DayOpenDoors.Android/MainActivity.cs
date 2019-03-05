using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace DayOpenDoors.Droid
{
    [Activity(Label = "DayOpenDoors", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region MapPage ScreenOrientation Subscribe
            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<MapPage>(this, "allowLandScapePortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            //during page opened setting to landscape
            MessagingCenter.Subscribe<MapPage>(this, "preventPortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });
            #endregion

            #region CheckPage and MainPage ScreenOrientation Subscribe
            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<MainPage>(this, "allowLandScapePortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            //during page opened setting to portrait
            MessagingCenter.Subscribe<CheckPage>(this, "preventLandscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });
            #endregion

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}