using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public List<Event> EventList { get; set; }

        MainPage mainPage;
        bool IsAdmin;
        ToolbarItem map, home, add, refresh;

        public InfoPage(List<Event> events, MainPage mainPage, ToolbarItem map, ToolbarItem home, ToolbarItem add, bool isAdmin)
        {
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            this.add = add;
            IsAdmin = isAdmin;
            mainPage.ToolbarItems.Remove(home);
            EventList = events;
            Event.RefreshEventList(EventList);
            InitializeComponent();
            BackgroundColor = Color.White;
            InfoList.IsVisible = false;
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
            refresh.Clicked += (s, e) =>
            {
                GetEvents(this, new EventArgs());
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
                        await DeleteEvent(selected);
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

        protected override void OnAppearing()
        {
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

        private async void GetEvents(object sender, EventArgs e)
        {
            GetButton.IsVisible = false;
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://dodserver.azurewebsites.net/api/event/");
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            EventList = JsonConvert.DeserializeObject<List<Event>>(content);
            mainPage.EventList = EventList;
            Event.RefreshEventList(EventList);
            InfoList.ItemsSource = null;
            InfoList.ItemsSource = EventList;
            InfoList.IsVisible = true;
            BackgroundImage = "FCSTree.png";
        }

        private async void Display(object sender, EventArgs e)
        {
            Event selected = (Event)InfoList.SelectedItem;
            InfoList.SelectedItem = null;
            await DisplayAlert($"{selected.Name}", $"{selected.Info}\nАудитория {selected.Place}\n{selected.SpeakerName}", "Ок");
        }

        async Task DeleteEvent(Event ev)
        {
            EventList.Remove(ev);
            InfoList.ItemsSource = null;
            InfoList.ItemsSource = EventList;
            HttpClient client = new HttpClient();
            Uri address = new Uri($"http://dodserver.azurewebsites.net/api/event/{ev.Id.ToString()}");
            await client.DeleteAsync(address);
        }
    }
}