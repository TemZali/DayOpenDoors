using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using DayOpenDoorsLibrary;

namespace DayOpenDoors
{
    public partial class CheckPage : ContentPage
    {
        public List<Event> EventList { get; set; }

        HttpClient Client { get; set; }

        MainPage mainPage;
        ToolbarItem map, home;

        public CheckPage(List<Event> events, MainPage mainPage, ToolbarItem map, ToolbarItem home)
        {
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            mainPage.ToolbarItems.Remove(map);
            mainPage.ToolbarItems.Remove(home);
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://dodserver.azurewebsites.net:80");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            EventList = events;
            InitializeComponent();
        }

        private async void CreateNew(object sender, EventArgs e)
        {
            ((Button)sender).Clicked -= CreateNew;
            if ((StatusPicker.SelectedIndex == 0 || StatusPicker.SelectedIndex == 1) && NameEntry.Text != "")
            {
                string newId = await PushUserAsync(new User { Id = 1, Username = NameEntry.Text, Userstatus = StatusPicker.SelectedItem.ToString() });
                if (newId != "-1")
                {
                    App.Current.Properties.Add("Id", newId);
                    App.Current.Properties.Add("Name", NameEntry.Text);
                    App.Current.Properties.Add("Status", StatusPicker.SelectedItem);

                    mainPage.ToolbarItems.Add(map);
                    mainPage.ToolbarItems.Add(home);
                    await Navigation.PushAsync(new InfoPage(EventList, mainPage, map, home));
                    Navigation.RemovePage(this);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Пользователь с таким именем уже существует", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите ваш статус", "Ок");
            }
            ((Button)sender).Clicked += CreateNew;
        }

        public async Task<string> PushUserAsync(User user)
        {
            var uri = new Uri(string.Format($"http://dodserver.azurewebsites.net:80/api/user/", string.Empty));
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var flag = await Client.PostAsync(uri, content);
                return await flag.Content.ReadAsStringAsync();
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Отсутствует подключение к сети", "Ок");
            }
            return null;
        }
    }
}