using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using System.Net.Http;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public List<Event> EventList { get; set; }

        MainPage mainPage;

        ToolbarItem map, home,add;

        public InfoPage(List<Event> events, MainPage mainPage, ToolbarItem map, ToolbarItem home,ToolbarItem add, bool isAdmin)
        {
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            this.add = add;
            mainPage.ToolbarItems.Remove(home);
            EventList = events;
            Event.RefreshEventList(EventList);
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!ToolbarItems.Contains(add))
            {
                ToolbarItems.Add(add);
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
            EventList.Remove(ev);
            InfoList.ItemsSource = null;
            InfoList.ItemsSource = EventList;
            HttpClient client = new HttpClient();
            Uri address = new Uri($"http://dodserver.azurewebsites.net/api/event/{ev.Id.ToString()}");
            await client.DeleteAsync(address);
        }
    }
}