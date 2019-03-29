﻿using System;
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

        /*private async void ShowEvents(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Выберите тип мероприятия", "Отмена", null, "Лекции", "Мастер-классы");
            if (result != "Отмена" && result != null)
            {
                TypeButton.Text = result;
                if (result == "Лекции")
                {
                    EventListView.ItemsSource = Lections;
                }
                else
                {
                    EventListView.ItemsSource = Master_Classes;
                }
            }
        }*/

        private void Lections_Click(object sender, EventArgs e)
        {
            EventListView.ItemsSource = Lections;
        }

        private void Master_Click(object sender, EventArgs e)
        {
            EventListView.ItemsSource = Master_Classes;
        }

        private void Info_Click(object sender, EventArgs e)
        {
            EventListView.SelectedItem = null;
        }
    }
}