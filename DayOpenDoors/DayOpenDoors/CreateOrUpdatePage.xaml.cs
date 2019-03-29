using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOrUpdatePage : ContentPage
    {
        Event old;

        public CreateOrUpdatePage(string type, Event ev = null)
        {
            InitializeComponent();
            if (type == "update")
            {
                old = ev;
                NameEntry.Text = ev.Name;
                PlaceEntry.Text = ev.Place;
                SpeakerEntry.Text = ev.SpeakerName;
                MonthEntry.Text = ev.Time.Month.ToString();
                DayEntry.Text = ev.Time.Day.ToString();
                HourEntry.Text = ev.Time.Hour.ToString();
                MinuteEntry.Text = ev.Time.Minute.ToString();
                DurationEntry.Text = ev.Duration.ToString();
                InfoEditor.Text = ev.Info;
                TypeEntry.Text = ev.Type;
                ChangeButton.Text = "Изменить";
                ChangeButton.Clicked += Update;
            }
            else
            {
                ChangeButton.Text = "Добавить";
                ChangeButton.Clicked += Add;
            }
        }

        async void Update(object sender, EventArgs e)
        {
            try
            {
                Event ev = new Event(PlaceEntry.Text, int.Parse(DurationEntry.Text),
                    new DateTime(2019, int.Parse(MonthEntry.Text), int.Parse(DayEntry.Text), int.Parse(HourEntry.Text), int.Parse(MinuteEntry.Text), 0),
                    NameEntry.Text, TypeEntry.Text, SpeakerEntry.Text, InfoEditor.Text)
                { Id = old.Id };
                HttpClient client = new HttpClient();
                Uri address = new Uri($"http://dodserver.azurewebsites.net/api/event/");
                var json = JsonConvert.SerializeObject(ev);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(address, content);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ок");
            }
        }

        async void Add(object sender, EventArgs e)
        {
            try
            {
                Event ev = new Event(PlaceEntry.Text, int.Parse(DurationEntry.Text),
                   new DateTime(2019, int.Parse(MonthEntry.Text), int.Parse(DayEntry.Text), int.Parse(HourEntry.Text), int.Parse(MinuteEntry.Text), 0),
                   NameEntry.Text, TypeEntry.Text, SpeakerEntry.Text, InfoEditor.Text);
                HttpClient client = new HttpClient();
                Uri address = new Uri($"http://dodserver.azurewebsites.net/api/event/");
                var json = JsonConvert.SerializeObject(ev);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(address, content);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "Ок");
            }
        }
    }
}