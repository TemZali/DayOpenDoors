using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Security.Cryptography;
using DayOpenDoorsLibrary;
using Plugin.Settings;

namespace DayOpenDoors
{

    public partial class AskOrCreatePage : ContentPage
    {
        App app;
        HttpClient Client;
        string Status;

        public AskOrCreatePage(string status, App app)
        {
            Status = "";
            this.app = app;
            Client = new HttpClient();
            //Client.BaseAddress = new Uri("https://dodserver.azurewebsites.net");
            Client.BaseAddress = new Uri("http://dodserver.azurewebsites.net:80");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            if (status == "sign_in")
            {
                StatusButton.IsVisible = false;
                NameEntry.Placeholder = "Введите имя";
                PasswordEntry.Placeholder = "Введите пароль";
                Title = "Авторизация";
                AskOrCreateButton.Clicked += new EventHandler(CheckExist);
                AskOrCreateButton.Text = "Авторизоваться";
            }
            else
            {
                NameEntry.Placeholder = "Введите имя";
                PasswordEntry.Placeholder = "Придумайте пароль";
                Title = "Регистрация";
                AskOrCreateButton.Clicked += new EventHandler(CreateNew);
                AskOrCreateButton.Text = "Зарегистрироваться";
            }
        }

        private async void CreateNew(object sender, EventArgs e)
        {
            ((Button)sender).Clicked -= CreateNew;
            if (CheckPassword())
            {
                if ((Status != "") && NameEntry.Text != "" && PasswordEntry.Text != "")
                {
                    try
                    {
                        string newId = await PushUserAsync(new User
                        {
                            Id = 1,
                            Username = NameEntry.Text,
                            Userpassword = GetMd5Hash(MD5.Create(), PasswordEntry.Text),
                            Userstatus = Status
                        });

                        if (newId != "-1")
                        {

                            CrossSettings.Current.AddOrUpdateValue("User", $"{newId},{NameEntry.Text},{Status}");

                            app.MainPage = new MainPage(app);
                        }
                        else
                        {
                            await DisplayAlert("Ошибка", "Пользователь с таким именем уже существует", "Ok");
                        }
                    }
                    catch(Exception ex)
                    {
                        await DisplayAlert("Ошибка!", ex.Message, "Ок");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", "Заполните все поля", "Ок");
                }
           ((Button)sender).Clicked += CreateNew;
            }
            else
            {
                await DisplayAlert("Ошибка","Пароль должен быть от 4 до 8 символов длиной, без пробелов","Ок");
            }
        }

        private async void CheckExist(object sender, EventArgs e)
        {
            if (NameEntry.Text.Length > 0 && PasswordEntry.Text.Length > 0)
            {
                ((Button)sender).Clicked -= CheckExist;
                try
                {
                    User instance = await CheckUserAsync(NameEntry.Text + " " + GetMd5Hash(MD5.Create(), PasswordEntry.Text));
                    if (instance == null)
                    {
                        await DisplayAlert("Ошибка", "Неверный логин или пароль", "Ок");
                    }
                    else
                    {
                        CrossSettings.Current.AddOrUpdateValue("User", $"{instance.Id},{instance.Username},{instance.Userstatus}");
                        app.MainPage = new MainPage(app);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "Ок");
                }
            ((Button)sender).Clicked += CheckExist;
            }
            else
            {
                await DisplayAlert("Ошибка","Вы не заполнили поля ввода","Ок");
            }
        }

        private void CheckLength(object sender, EventArgs e)
        {
            string password = PasswordEntry.Text;
            if (password.Length > 8)
            {
                password.Remove(password.Length - 1);
                PasswordEntry.Text = password;
            }
        }

        bool CheckPassword()
        {
            if (!PasswordEntry.Text.Contains(" ")&&PasswordEntry.Text.Length>4)
            {
                return true;
            }
            return false;
        }

        private async void ChooseStatus(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Выберите статус", "отмена", null, "Абитуриент", "Родственник абитуриента");
            StatusButton.Text = result;
            Status = result;
        }

        async Task<string> PushUserAsync(User user)
        {
            //var uri = new Uri(string.Format($"https://dodserver.azurewebsites.net/api/user/", string.Empty));
            var uri = new Uri("http://dodserver.azurewebsites.net:80/api/user/");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var flag = await Client.PostAsync(uri, content);
            return await flag.Content.ReadAsStringAsync();
        }

        async Task<User> CheckUserAsync(string package)
        {
            //var uri = new Uri(string.Format($"https://dodserver.azurewebsites.net/api/user/{package}", string.Empty));
            var uri = new Uri($"http://dodserver.azurewebsites.net:80/api/user/{package}");
            var response = await Client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User instance = JsonConvert.DeserializeObject<User>(content);
                return instance;
            }
            else
            {
                throw new ApplicationException("Отсутсвует подключение к сети");
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}