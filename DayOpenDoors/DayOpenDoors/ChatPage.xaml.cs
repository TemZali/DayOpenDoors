using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using DayOpenDoorsLibrary;
using Plugin.Settings;

namespace DayOpenDoors
{

    public partial class ChatPage : ContentPage
    {
        private bool CheckCountOfMessages()
        {
            try
            {
                var j = Task.Run(GetCountOfMessagesAsync);
                j.Wait();
                if (n > Messages.Count)
                {
                    var lst = Task.Run(RefreshDataAsync);
                    lst.Wait();
                    int CountOfMessages = Messages.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].UserId == ThisUser.Id.ToString())
                        {
                            list[i].Orientation = LayoutOptions.End;
                        }
                        else
                        {
                            list[i].Orientation = LayoutOptions.Start;
                        }
                        list[i].Userstatus = "[" + list[i].Userstatus + "]";
                        Messages.Add(list[i]);
                    }
                    MessagesView.ItemsSource = null;
                    MessagesView.ItemsSource = Messages;
                    if (list.Count == Messages.Count)
                    {
                        MessagesView.ScrollTo(Messages[Messages.Count - 1], ScrollToPosition.End, true);
                    }
                }
                return true;
            }
            catch
            {
                ChatLayout.Children.Add(new Label { Text = "Отсутствует подключение к сети, перезайдите на страницу" });
                return false;
            }
        }
        public int n { get; set; }

        public bool IsUserExist { get; set; }

        public List<Message> list;

        User ThisUser { get; set; }

        HttpClient client { get; set; }

        List<Message> Messages { get; set; }

        public ChatPage()
        {
            string[] userstr = CrossSettings.Current.GetValueOrDefault("User", null).Split(',');
            ThisUser = new User() { Id = int.Parse(userstr[0]), Username = userstr[1], Userstatus = userstr[2] };
            Messages = new List<Message>();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://dodserver.azurewebsites.net:80");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            Task.Run(CheckUser);
            Device.StartTimer(TimeSpan.FromSeconds(5), CheckCountOfMessages);
            this.BindingContext = this;
        }

        public async Task CheckUser()
        {
            var uri = new Uri(string.Format($"http://dodserver.azurewebsites.net:80/api/user/{ThisUser.Id}", string.Empty));
            HttpResponseMessage answer = await client.GetAsync(uri);
            if (answer.ReasonPhrase == "Bad Request")
            {
                IsUserExist = false;
            }
            else
            {
                IsUserExist = true;
            }
        }

        public async Task<int> RefreshDataAsync()
        {
            list = new List<Message>();

            var uri = new Uri(string.Format($"http://dodserver.azurewebsites.net:80/api/message/{Messages.Count + 1}", string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Message>>(content);
                }
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Отсутствует подключение к сети", "Ок");
            }
            return 0;
        }

        public async Task<int> GetCountOfMessagesAsync()
        {
            var uri = new Uri(string.Format($"http://dodserver.azurewebsites.net:80/api/message/", string.Empty));

            Message message = new Message();

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    message = JsonConvert.DeserializeObject<Message>(content);
                }
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Отсутствует подключение к сети", "Ок");
            }
            n = message.Id;
            return 0;
        }

        public async Task PushMessageAsync(Message message)
        {
            var uri = new Uri(string.Format($"http://dodserver.azurewebsites.net:80/api/message/", string.Empty));
            try
            {
                var json = JsonConvert.SerializeObject(message);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(uri, content);
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Отсутствует подключение к сети", "Ок");
            }
        }

        private void Select(object sender, EventArgs e)
        {
            MessagesView.SelectedItem = null;
        }

        private async void Click(object sender, EventArgs e)
        {
            if (IsUserExist)
            {
                if (TextEntry.Text != null&&TextEntry.Text.Trim().Length>0)
                {
                    await PushMessageAsync(new Message
                    {
                        Mess = TextEntry.Text.Trim(),
                        UserId = ThisUser.Id.ToString(),
                        Username = ThisUser.Username,
                        Userstatus = ThisUser.Userstatus,
                        Time = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00")
                    });
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Незарегистрированный пользователь не может использовать чат!", "Ок");
            }
            TextEntry.Text = null;

        }
    }
}