using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;
using Plugin.Settings;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public List<Event> EventList { get; set; }

        MainPage mainPage;
        bool IsAdmin;
        ToolbarItem map, home, add, refresh;

        #region Поворот экрана
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
                BackgroundImage = "FCSTree3.png";
            else
                BackgroundImage = "FCSTree4.png";
        }
        #endregion

        // TODO: 

        public InfoPage(MainPage mainPage, ToolbarItem map, ToolbarItem home, ToolbarItem add, bool isAdmin)
        {
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            this.add = add;
            IsAdmin = isAdmin;
            mainPage.ToolbarItems.Remove(home);
            InitializeComponent();
            if (isAdmin)
            {
                InfoList.ItemTapped += Change;
            }
            else
            {
                InfoList.ItemTapped += Display;
            }
            this.BindingContext = this;
            refresh = new ToolbarItem()
            {
                Text = "Обновить список",
                Order = ToolbarItemOrder.Secondary
            };
            refresh.Clicked += async (s, e) =>
            {
                await GetEvents();
                Event.RefreshEventList(EventList);
                InfoList.ItemsSource = null;
                InfoList.ItemsSource = EventList;
            };
        }

        private async void Change(object sender, EventArgs e)
        {
            Event selected = (Event)InfoList.SelectedItem;
            InfoList.SelectedItem = null;
            string result = await DisplayActionSheet("", "Отмена", null, "Информация", "Удалить", "Изменить");
            switch (result)
            {
                case "Информация":
                    await DisplayAlert($"{selected.Name}", $"{selected.Info}\nАудитория {selected.Place}\n{selected.SpeakerName}", "Ок");
                    break;
                case "Удалить":
                    if (await DisplayAlert("Удаление", "Вы уверены?", "Да", "Нет"))
                    {
                        await DeleteEvent(selected);
                        Event.RefreshEventList(EventList);
                        InfoList.ItemsSource = null;
                        InfoList.ItemsSource = EventList;
                    }
                    break;
                case "Изменить":
                    await Navigation.PushAsync(new CreateOrUpdatePage("update", selected));
                    break;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(add);
            ToolbarItems.Remove(refresh);
        }

        protected override async void OnAppearing()
        {
            await GetEvents();
            Event.RefreshEventList(EventList);
            InfoList.ItemsSource = null;
            InfoList.ItemsSource = EventList;
            base.OnAppearing();
            if (IsAdmin && !ToolbarItems.Contains(add))
            {
                ToolbarItems.Add(add);
            }
            if (!ToolbarItems.Contains(refresh))
            {
                ToolbarItems.Add(refresh);
            }
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
                mainPage.EventList = EventList;
                CrossSettings.Current.AddOrUpdateValue("List",JsonConvert.SerializeObject(EventList));
            }
            catch
            {
                await DisplayAlert("Ошибка", "Отсутствует подключение к сети" +
                    "\nБудет показан загруженный ранее список мероприятий", "Ок");
                EventList = JsonConvert.DeserializeObject<List<Event>>(CrossSettings.Current.GetValueOrDefault("List", null));
            }
        }

        private async void Display(object sender, EventArgs e)
        {
            Event selected = (Event)InfoList.SelectedItem;
            InfoList.SelectedItem = null;
            await DisplayAlert($"{selected.Name}", $"{selected.Info}\nАудитория {selected.Place}\n{selected.SpeakerName}", "Ок");
        }

        async Task DeleteEvent(Event ev)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri address = new Uri($"http://dodserver.azurewebsites.net/api/event/{ev.Id.ToString()}");
                await client.DeleteAsync(address);
                await GetEvents();
            }
            catch
            {
                await DisplayAlert("Ошибка", "Отсутсвует подключение к сети", "Ок");
            }
        }
    }
}