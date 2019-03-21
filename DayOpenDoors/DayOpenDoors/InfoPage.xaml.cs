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

        MainPage mainPage;

        ToolbarItem map, home;

        public InfoPage(List<Event> events, MainPage mainPage, ToolbarItem map, ToolbarItem home)
        {
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            mainPage.ToolbarItems.Remove(home);
            InitializeComponent();
            EventList = events;
            Event.RefreshEventList(EventList);
            this.BindingContext = this;
        }

        private async void Info_Click(object sender, EventArgs e)
        {
            Event selected = (Event)InfoList.SelectedItem;
            InfoList.SelectedItem = null;
            await DisplayAlert($"{selected.Name}",$"{selected.Info}\nАудитория {selected.Place}\n{selected.SpeakerName}","Ок");
        }
    }
}