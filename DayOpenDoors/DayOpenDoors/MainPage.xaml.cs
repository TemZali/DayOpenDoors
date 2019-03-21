using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Plugin.Settings;

namespace DayOpenDoors
{
    public partial class MainPage : MasterDetailPage
    {
        App app;
        ToolbarItem map, home;

        public List<Event> EventList { get; set; }

        public string[] ThisUser { get; set; }

        #region Изменение разрешений на поворот экрана
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }
        #endregion

        public MainPage(App app)
        {
            ThisUser = CrossSettings.Current.GetValueOrDefault("User", null).Split(',');
            this.app = app;
            InitializeComponent();
            NameLabel.Text = ThisUser[1];
            StatusLabel.Text = ThisUser[2];
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
