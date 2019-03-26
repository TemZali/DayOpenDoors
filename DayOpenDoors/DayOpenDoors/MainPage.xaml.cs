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
                Detail = new NavigationPage(new InfoPage(EventList, this, map, home, add, IsAdmin));
            };
            Detail = new NavigationPage(new InfoPage(EventList, this, map, home, add, IsAdmin));
            /* EventList = new List<Event>()
             {
                 new Event{Name="О факультете",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Лекция",Duration=30,EventColor=Color.Blue,Place="622"},

                 new Event{Name="Площадка для родителей",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Лекция",Duration=30,
                     EventColor =Color.Blue,Place="509",Info="Выступление представителей приемной комиссии и военной кафедры"},

                 new Event{Name="Speed-dating со студентами ФКН",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Митап",Duration=240,
                     EventColor =Color.Blue,Place="400"},

                 new Event{Name="О факультете",
                     Time =new DateTime(2019,04,07,12,30,0), Status="Ожидается",Type="Лекция",Duration=30,EventColor=Color.Blue,Place="205"},

                 new Event{Name="Презентация программы \"Прикладная математика и информатика\"",
                     Time =new DateTime(2019,04,07,12,30,0), Status="Ожидается",Type="Лекция",Duration=30,
                     EventColor =Color.Blue,Place="622"},

                 new Event{Name="Лекция \"Как создать свою первую игру\"",
                     Time =new DateTime(2019,04,07,12,30,0), Status="Ожидается",Type="Лекция",Duration=30,
                     EventColor =Color.Blue,Place="317",SpeakerName="Веселко Никита, студент ПИ"},

                 new Event{Name="Презентация программы \"Прикладной анализ данных\"",
                     Time =new DateTime(2019,04,07,13,0,0), Status="Ожидается",Type="Лекция",Duration=30,
                     EventColor =Color.Blue,Place="205"},

                 new Event{Name="Площадка для родителей",
                     Time =new DateTime(2019,04,07,13,0,0), Status="Ожидается",Type="Лекция",Duration=30,
                     EventColor =Color.Blue,Place="317",Info="Выступление представителей приемной комиссии и военной кафедры"},

                 new Event{Name="Лекция \"Машинное обучение в Яндексе\"",
                     Time =new DateTime(2019,04,07,13,30,0), Status="Ожидается",Type="Лекция",Duration=30,
                     EventColor =Color.Blue,Place="509",SpeakerName="Александр Крайнов, Руководитель Лаборатории машинного интеллекта компании \"Яндекс\""},

                 new Event{Name="О факультете(формат вопрос-ответ)",
                     Time =new DateTime(2019,04,07,13,30,0), Status="Ожидается",Type="Лекция",Duration=30,EventColor=Color.Blue,Place="509"},

                 new Event{Name="Презентация программы \"Программная инженерия\"",
                     Time =new DateTime(2019,04,07,14,0,0), Status="Ожидается",Type="Лекция",Duration=60,
                     EventColor =Color.Blue,Place="622"},

                 new Event{Name="О стажировках и практиках на ФКН",
                     Time =new DateTime(2019,04,07,15,0,0), Status="Ожидается",Type="Лекция",Duration=60,
                     EventColor =Color.Blue,Place="622",SpeakerName="Руководитель Центра практик и проектной работы Римма Ахметсафина"},

                  new Event{Name="Студенты ПИ о своих проектах",
                     Time =new DateTime(2019,04,07,15,0,0), Status="Ожидается",Type="Лекция",Duration=60,
                     EventColor =Color.Blue,Place="622"},

                  new Event{Name="Столовая",Time =new DateTime(2019,04,07,15,0,0),
                  Status="Ожидается",Duration=120,Info="Приятного аппетита!",
                     EventColor =Color.Blue,Place="Столовая",Type="Перекус"},

                   new Event{Name="О разработке игр",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Мастер-класс",Duration=240,
                     EventColor =Color.Blue,Place="412",SpeakerName="Веселко Никита, студент ПИ"},

                   new Event{Name="Введение в reverse engineering. Анализ кода программы для начинающих",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Мастер-класс",Duration=240,
                     EventColor =Color.Blue,Place="416",SpeakerName="Московская школа программистов"},

                   new Event{Name="Мастер-класс по языку Python",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Мастер-класс",Duration=240,
                     EventColor =Color.Blue,Place="420"},

                   new Event{Name="Решение олимпиадных задач по информатике",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Мастер-класс",Duration=240,
                     EventColor =Color.Blue,Place="503",SpeakerName="Центр студенческих олимпиад ФКН НИУ ВШЭ"},

                   new Event{Name="Мастер-класс по языку C++",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Мастер-класс",Duration=240,
                     EventColor =Color.Blue,Place="501"},

                   new Event{Name="Мастер-класс по языку Python \"для продвинутых\"",
                     Time =new DateTime(2019,04,07,12,0,0), Status="Ожидается",Type="Мастер-класс",Duration=240,
                     EventColor =Color.Blue,Place="605"},
             };*/
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
