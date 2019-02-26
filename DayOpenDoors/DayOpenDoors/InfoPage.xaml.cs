using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InfoPage : ContentPage
    {
        public List<Event> EventList { get; set; }

        public InfoPage(List<Event> events)
        {
            InitializeComponent();
            EventList = events;
            Event.RefreshEventList(EventList);
            this.BindingContext = this;
        }

        private async void Info_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventPage((Event)((ItemTappedEventArgs)e).Item));
        }
    }
}