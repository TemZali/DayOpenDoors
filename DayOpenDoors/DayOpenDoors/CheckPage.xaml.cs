using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using DayOpenDoorsLibrary;

namespace DayOpenDoors
{
    public partial class CheckPage : ContentPage
    {
        App app;

        public CheckPage(App app)
        {
            this.app = app;
            InitializeComponent();
        }

        private void Sign_in(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AskOrCreatePage("sign_in", app));
        }

        private void Sign_up(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AskOrCreatePage("sign_up", app));
        }

    }
}