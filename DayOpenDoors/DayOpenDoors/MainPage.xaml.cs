using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Plugin.Settings;
using System.Threading.Tasks;

namespace DayOpenDoors
{
    public partial class MainPage : MasterDetailPage
    {
        App app;
        bool IsAdmin;
        ToolbarItem map, home, add;

        public List<Event> EventList { get; set; }

        public User ThisUser { get; set; }

        #region Изменение разрешений на поворот экрана
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }
        #endregion

        public MainPage(App app)
        {
            EventList = new List<Event>();
            string[] userstr = CrossSettings.Current.GetValueOrDefault("User", null).Split(',');
            ThisUser = new User() { Id = int.Parse(userstr[0]), Username = userstr[1], Userstatus = userstr[2] };
            this.app = app;
            InitializeComponent();
            NameLabel.Text = ThisUser.Username;
            StatusLabel.Text = ThisUser.Userstatus;
            IsAdmin = false;
            if (ThisUser.Userstatus == "Админ")
            {
                add = new ToolbarItem()
                {
                    Text = "Добавить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 2
                };
                add.Clicked += async (s, e) =>
                 {
                     await Detail.Navigation.PushAsync(new CreateOrUpdatePage("create"));
                 };
                IsAdmin = true;
            }
            map = new ToolbarItem()
            {
                Text = "Карта",
                Priority = 1

            };
            map.Clicked += async (s, e) =>
            {
                await Detail.Navigation.PushAsync(new MapPage(EventList, this, map, home));
            };
            ToolbarItems.Add(map);
            home = new ToolbarItem()
            {
                Text = "Домой",
                Priority = 3
            };
            home.Clicked += (s, e) =>
            {
                if (!ToolbarItems.Contains(map))
                {
                    ToolbarItems.Add(map);
                }
                ToolbarItems.Remove(home);
                Detail = new NavigationPage(new InfoPage(this, map, home, add, IsAdmin));
            };
            Detail = new NavigationPage(new InfoPage(this, map, home, add, IsAdmin));

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
            ToolbarItems.Remove(add);
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
        private async void Exit(object sender, EventArgs e)
        {
            this.IsPresented = false;
            if (CrossSettings.Current.GetValueOrDefault("User", null) != null)
            {
                app.MainPage = new NavigationPage(new CheckPage(app));
                CrossSettings.Current.Remove("User");
            }
            else
            {
                await DisplayAlert("Ошибка", "Вы не авторизованы!", "Ок");
            }
        }
        private void Way_Click(object sender, EventArgs e)
        {
            CheckToolBar();
            Detail = new NavigationPage(new WayPage());
            this.IsPresented = false;
        }
    }
}
