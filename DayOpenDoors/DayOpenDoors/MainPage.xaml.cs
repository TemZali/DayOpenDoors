using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;

namespace DayOpenDoors
{
    public partial class MainPage : MasterDetailPage
    {
        App app;
        ToolbarItem map, home;

        public List<Event> EventList { get; set; }

        public MainPage(App app)
        {
            this.app = app;
            InitializeComponent();
            map = new ToolbarItem()
            {
                Text = "Карта"
            };
            map.Clicked += async (s, e) =>
            {
                await Detail.Navigation.PushAsync(new MapPage(EventList, this, map, home));
            };
            ToolbarItems.Add(map);
            home = new ToolbarItem()
            {
                Text = "Домой"
            };
            home.Clicked += (s, e) =>
            {
                if (!ToolbarItems.Contains(map))
                {
                    ToolbarItems.Add(map);
                }
                ToolbarItems.Remove(home);
                Detail = new NavigationPage(new InfoPage(EventList, this, map, home));
            };
            EventList = new List<Event>()
            {
                new Event{ Name="События с очень длинным именем, ну вы поняли",Time = new DateTime(2018,02,23,23,0,0),Status="Ожидается",Type="Лекция",Duration=60,EventColor=Color.White}
            };
            Detail = new NavigationPage(new InfoPage(EventList, this, map, home));
        }

        void CheckToolBar()
        {
            if (!ToolbarItems.Contains(map))
            {
                ToolbarItems.Add(map);
            }
            if (!ToolbarItems.Contains(home))
            {
                ToolbarItems.Add(home);
            }
        }

        private void Plan_Click(object sender, EventArgs e)
        {
            CheckToolBar();
            this.IsPresented = false;
            Detail = new NavigationPage(new PlanPage(EventList));
        }
        private void Chat_Click(object sender, EventArgs e)
        {
            CheckToolBar();
            this.IsPresented = false;
            Detail = new NavigationPage(new ChatPage());
        }
        private void DR_Click(object sender, EventArgs e)
        {
            CheckToolBar();
            this.IsPresented = false;
            Detail = new NavigationPage(new DRPage());
        }
        private async void Exit(object sender, EventArgs e)
        {
            object zero;
            this.IsPresented = false;
            if (App.Current.Properties.TryGetValue("User", out zero))
            {
                App.Current.Properties.Remove("User");
                app.MainPage = new NavigationPage(new CheckPage(app));
            }
            else
            {
                await DisplayAlert("Ошибка", "Вы не авторизованы!", "Ок");
            }
        }
        private void Some_Click(object sender, EventArgs e)
        {
            CheckToolBar();
            this.IsPresented = false;
        }
    }
}
