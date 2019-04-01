using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Plugin.Settings;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanPage : ContentPage
    {
        public List<Event> Lections { get; set; }

        public List<Event> Master_Classes { get; set; }

        public List<Event> EventList;

        ToolbarItem refresh;

        #region Поворот экрана
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
                BackgroundImage = "PlanBackground2.jpg";
            else
                BackgroundImage = "PlanBackground.jpg";
        }
        #endregion

        public PlanPage(List<Event> events)
        {
            EventList = events;

            refresh = new ToolbarItem()
            {
                Text = "Обновить список",
                Order = ToolbarItemOrder.Secondary
            };
            refresh.Clicked += async (s, e) =>
            {
                await GetEvents();
            };
            InitializeComponent();
            BackgroundImage = "PlanBackground.jpg";

            RefreshEvents();

            this.BindingContext = this;
        }

        public void RefreshEvents()
        {
            Event.RefreshEventList(EventList);
            Lections = new List<Event>();
            Master_Classes = new List<Event>();
            foreach (Event @event in EventList)
            {
                if (@event.Type == "Лекция")
                {
                    Lections.Add(@event);
                }
                else
                {
                    Master_Classes.Add(@event);
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToolbarItems.Add(refresh);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(refresh);
        }

        private async Task GetEvents()
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri("http://dodserver.azurewebsites.net/api/event/");
                var response = await client.GetAsync(uri);
                var content = await response.Content.ReadAsStringAsync();
                EventList = JsonConvert.DeserializeObject<List<Event>>(content);
                Event.RefreshEventList(EventList);
                RefreshEvents();
                //ShowEvents(this, new EventArgs());
                CrossSettings.Current.AddOrUpdateValue("List", JsonConvert.SerializeObject(EventList));
            }
            catch
            {
                await DisplayAlert("Ошибка", "Отсутствует подключение к сети" +
                    "\nБудет показан загруженный ранее список мероприятий", "Ок");
                EventList = JsonConvert.DeserializeObject<List<Event>>(CrossSettings.Current.GetValueOrDefault("List", null));
            }
        }

        private void Lections_Click(object sender, EventArgs e)
        {
            BackgroundImage = "background.jpg";
            EventListView.ItemsSource = Lections;
        }

        private void Master_Click(object sender, EventArgs e)
        {
            BackgroundImage = "background.jpg";
            EventListView.ItemsSource = Master_Classes;
        }

        private void Info_Click(object sender, EventArgs e)
        {
            EventListView.SelectedItem = null;
        }
    }
}