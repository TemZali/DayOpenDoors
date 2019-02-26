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

	public partial class ChatPage : ContentPage
	{
        private bool CheckCountOfMessages()
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
                    if (list[i].UserId == App.Current.Properties["Id"].ToString())
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
                MessagesView.BeginRefresh();
                MessagesView.EndRefresh();
            }
            return true;
        }
        public int n { get; set; }

        public bool IsUserExist { get; set; }

        public List<Message> list;

        HttpClient client { get; set; }

        List<Message> Messages { get; set; }

        public ChatPage()
        {
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
            var uri = new Uri(string.Format($"http://dodserver.azurewebsites.net:80/api/user/{ App.Current.Properties["Id"].ToString()}", string.Empty));
            string s = App.Current.Properties["Id"].ToString();
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


        private async void Click(object sender, EventArgs e)
        {
            if (IsUserExist)
            {
                if (TextEntry.Text != "")
                {
                    await PushMessageAsync(new Message
                    {
                        Mess = TextEntry.Text,
                        UserId = App.Current.Properties["Id"].ToString(),
                        Username = App.Current.Properties["Name"].ToString(),
                        Userstatus = App.Current.Properties["Status"].ToString(),
                        Time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()
                    });
                    TextEntry.Text = "";
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Незарегистрированный пользователь не может использовать чат!", "Ок");
            }

        }
    }
}