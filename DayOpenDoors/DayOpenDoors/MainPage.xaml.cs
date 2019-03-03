using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;

namespace DayOpenDoors
{
    public partial class MainPage : MasterDetailPage
    {
        new object Id;
        public List<Event> EventList { get; set; }
        public MainPage()
        {
            InitializeComponent();
            ToolbarItem map = new ToolbarItem()
            {
                Text = "Карта"
            };
            map.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new MapPage());
            };
            ToolbarItems.Add(map);
            ToolbarItem home = new ToolbarItem()
            {
                Text = "Домой"
            };
            home.Clicked += (s, e) =>
            {
                Detail = new NavigationPage(new InfoPage(EventList, this, map, home));
            };
            ToolbarItems.Add(home);
            EventList = new List<Event>()
            {
                new Event{ Name="События с очень длинным именем, ну вы поняли",Time = new DateTime(2018,02,23,23,0,0),Status="Ожидается",Type="Лекция",Duration=60,EventColor=Color.White}
            };
            if (App.Current.Properties.TryGetValue("Id", out Id))
            {
                Detail = new NavigationPage(new InfoPage(EventList, this, map, home));
            }
            else
            {
                Detail = new NavigationPage(new CheckPage(EventList, this, map, home));
            }

        }

        private void Plan_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PlanPage(EventList));
        }
        private void Chat_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ChatPage());
        }
        private void DR_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new DRPage());
        }
        private void Some_Click(object sender, EventArgs e)
        {

        }
    }
}
