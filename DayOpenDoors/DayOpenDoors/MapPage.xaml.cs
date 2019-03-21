using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using FloorsLib;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        Image image = new Image();
        Hashtable flatroomsButtons = new Hashtable();
        Hashtable Rooms = new Hashtable();
        DateTime time;
        Floor floor;
        List<Event> Events;
        bool first;
        int currentIndex = -1;

        private Style plainButton = new Style(typeof(Button))
        {
            Setters = {
                new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#eee") },
                new Setter { Property = Button.TextColorProperty, Value = Color.Black },
                new Setter { Property = Button.BorderColorProperty, Value = Color.Black },
                new Setter { Property = Button.PaddingProperty, Value = "0, 0, 0, 0" },
                new Setter { Property = Button.BorderWidthProperty, Value = 1 },
                new Setter { Property = Button.FontSizeProperty, Value = 10 }
           }
        };

        MainPage mainPage;
        ToolbarItem map, home;


        public MapPage(List<Event> events, MainPage mainPage,ToolbarItem map, ToolbarItem home)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            Events = events;
            time = DateTime.Now;
            first = true;

            mainPage.ToolbarItems.Add(home);
            mainPage.ToolbarItems.Remove(map);

            image.Source = "Base.jpg";
            FlatGrid.IsVisible = false;
            BindingContext = image.Source;

            MainGrid.Children.Add(FlatGrid, 0, 2);
            Grid.SetColumnSpan(FlatGrid, 6);

            FlatGrid.Children.Add(LeftGrid, 1, 3);
            FlatGrid.Children.Add(MiddleGrid, 5, 3);
            FlatGrid.Children.Add(RightGrid, 9, 3);

            for (int i = 0; i < 11; i += 2)
            {
                BoxView v = new BoxView();
                v.Color = Color.Black;
                FlatGrid.Children.Add(v, i, 2);
                Grid.SetRowSpan(v, 3);
                if (i < 10)
                    for (int j = 2; j < 5; j += 2)
                    {
                        BoxView h = new BoxView();
                        h.Color = Color.Black;
                        FlatGrid.Children.Add(h, i, j);
                        Grid.SetColumnSpan(h, 3);
                    }
            }

            FirstButton_Clicked(new object(), new EventArgs());
        }

        #region Изменение разрешений на поворот экрана
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "preventPortrait");
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }
        #endregion

        private void InAppearing()
        {
            FlatGrid.IsVisible = true;
            CreateMap(floor.Lrooms, floor.Mrooms, floor.Rrooms);
        }

        #region Кнопки этажей
        private void FirstButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor1();
            InAppearing();
            FloorLabel.Text = "1 Этаж";
        }

        private void SecondButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor2();
            InAppearing();
            FloorLabel.Text = "2 Этаж";
        }

        private void ThirdButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor3();
            InAppearing();
            FloorLabel.Text = "3 Этаж";
        }

        private void FourthButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor4();
            InAppearing();
            FloorLabel.Text = "4 Этаж";
        }

        private void FifthButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor5();
            InAppearing();
            FloorLabel.Text = "5 Этаж";
        }

        private void SixthButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor6();
            InAppearing();
            FloorLabel.Text = "6 Этаж";
        }
        #endregion


        private void CreateMap(Hashtable left, Hashtable middle, Hashtable right)
        {
            Invalidate();

            AddAPart(left, LeftGrid);
            AddAPart(middle, MiddleGrid);
            AddAPart(right, RightGrid);
        }

        //добавление части этажа
        private void AddAPart(Hashtable part, Grid grid)
        {
            foreach (string key in part.Keys)
            {
                //проверка на нахождение кнопки на экране
                if (flatroomsButtons.ContainsKey(key))
                {
                    ((Button)flatroomsButtons[key]).IsVisible = true;
                }
                else
                {
                    flatroomsButtons.Add(key, new Button { Text = (key[0] == 'W') ? "WC" : key, Style = plainButton });
                    ((Button)flatroomsButtons[key]).CornerRadius = 0;
                    Rooms.Add(key, new Room(key));

                    //добавление анонимного обаботчика
                    ((Button)flatroomsButtons[key]).Clicked += (o, e) =>
                    {
                        Room room = ((Room)Rooms[key]);

                        InfoLabel.Text = room.ToString();
                        ((Button)flatroomsButtons[key]).BackgroundColor = Color.FromHex(room.Color);

                        if (room.IsBusy != 0)
                        {
                            currentIndex = room.EventIndex == -1? 0: room.EventIndex;

                            EventLabel.Text = room.ToString() + ": " + Events[currentIndex].Name;

                            EventLabel.IsVisible = true;
                            EventButton.IsVisible = true;
                        }
                        else
                        {
                            EventLabel.IsVisible = false;
                            EventButton.IsVisible = false;
                        }
                    };
                }

                if (first || DateTime.Now - time > new TimeSpan(0, 1, 0))
                {
                    time = DateTime.Now;
                    Update();
                    ChangeColor();
                }

                string val = part[key].ToString();

                int row = int.Parse(val.Substring(0, 1));
                int rowspan = int.Parse(val.Substring(1, 1));
                int column = int.Parse(val.Substring(2, 1));
                int columnspan = int.Parse(val.Substring(3, 1));

                //добавление на карту
                grid.Children.Add((Button)flatroomsButtons[key], column, row);
                Grid.SetColumnSpan((Button)flatroomsButtons[key], columnspan);
                Grid.SetRowSpan((Button)flatroomsButtons[key], rowspan);
            }
        }

        //скрытие кнопок
        private void Invalidate()
        {
            foreach (Button button in flatroomsButtons.Values)
            {
                button.IsVisible = false;
            }
        }

        //смена цвета занятых кабинетов
        private void ChangeColor()
        {
            foreach (Room room in Rooms.Values)
            {
                ((Button)flatroomsButtons[room.Number]).BackgroundColor = Color.FromHex(room.Color);
            }
        }

        private async void EventButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert(Events[currentIndex].Type, Events[currentIndex].Info, "Ок");
        }

        public void Update()
        {
            Event.RefreshEventList(Events);
            foreach (Event @event in Events)
            {
                if (Rooms.Contains(@event.Place))
                {
                    Room room = (Room)Rooms[@event.Place];
                    if (room.EventIndex == -1 || !(Events[room.EventIndex].Status == "Скоро начнется" || Events[room.EventIndex].Status == "Уже идет"))
                        room.EventIndex = Events.IndexOf(@event);
                }
            }

            foreach (Room room in Rooms.Values)
            {
                if (room.EventIndex == -1)
                {
                    room.IsBusy = 0;
                    room.ChangeColor();
                    continue;
                }
                
                switch (Events[room.EventIndex].Status)
                {
                    case "Скоро начнется":
                        room.IsBusy = 1;
                        break;
                    case "Уже идет":
                        room.IsBusy = 2;
                        break;
                    default:
                        room.IsBusy = 0;
                        room.EventIndex = -1;
                        break;
                }
                room.ChangeColor();
            }
        }
    }
}