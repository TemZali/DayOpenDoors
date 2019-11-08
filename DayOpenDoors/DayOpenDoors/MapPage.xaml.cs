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
        Hashtable walls = new Hashtable();
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
                new Setter { Property = Button.BorderWidthProperty, Value = 0.1 },
                new Setter { Property = Button.FontSizeProperty, Value = 10 }
           }
        };

        MainPage mainPage;
        ToolbarItem map, home;


        public MapPage(List<Event> events, MainPage mainPage, ToolbarItem map, ToolbarItem home)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.map = map;
            this.home = home;
            Events = events;
            time = DateTime.Now;
            first = true;
            if (!mainPage.ToolbarItems.Contains(home))
            {
                mainPage.ToolbarItems.Add(home);
            }
            mainPage.ToolbarItems.Remove(map);

            image.Source = "Base.jpg";
            FlatGrid.IsVisible = false;
            BindingContext = image.Source;

            MainGrid.Children.Add(FlatGrid, 0, 2);
            Grid.SetColumnSpan(FlatGrid, 6);

            FlatGrid.Children.Add(UpperGrid, 1, 3);
            FlatGrid.Children.Add(MiddleGrid, 1, 5);
            FlatGrid.Children.Add(BottomGrid, 1, 7);

            for (int i = 0; i < 3; i += 2)
            {
                BoxView v = new BoxView();
                v.Color = Color.Black;
                FlatGrid.Children.Add(v, i, 2);
                Grid.SetRowSpan(v, 7);
            }

            for (int i = 2; i < 9; i += 2)
            {
                BoxView v = new BoxView();
                v.Color = Color.Black;
                FlatGrid.Children.Add(v, 0, i);
                Grid.SetColumnSpan(v, 3);
            }

            BoxView s = new BoxView();
            s.Color = Color.Black;
            walls.Add("wall", s);
            BottomGrid.Children.Add(s, 7, 0);
            Grid.SetRowSpan(s, 2);

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
            mainPage.ToolbarItems.Remove(home);
            if (!mainPage.ToolbarItems.Contains(map))
            {
                mainPage.ToolbarItems.Add(map);
            }
        }
        #endregion

        private void InAppearing()
        {
            FlatGrid.IsVisible = true;

            CreateMap(floor.Rooms, floor.Hall, floor.OtherRooms);
        }

        #region Кнопки этажей
        private void FirstButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor1();
            InAppearing();
            FloorLabel.Text = "Корпус R. 1 Этаж";
        }

        private void SecondButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor2();
            InAppearing();
            FloorLabel.Text = "Корпус R. 2 Этаж";
        }

        private void ThirdButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor3();
            InAppearing();
            FloorLabel.Text = "Корпус R. 3 Этаж";
        }

        private void FourthButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor4();
            InAppearing();
            FloorLabel.Text = "Корпус R. 4 Этаж";
        }

        private void FifthButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor5();
            InAppearing();
            FloorLabel.Text = "Корпус R. 5 Этаж";
        }

        private void SixthButton_Clicked(object sender, EventArgs e)
        {
            floor = new Floor6();
            InAppearing();
            FloorLabel.Text = "Корпус R. 6 Этаж";
        }
        #endregion


        private void CreateMap(Hashtable map, Hashtable hall, Hashtable extra)
        {
            Invalidate();

            AddAPart(map, UpperGrid);
            AddAPart(hall, MiddleGrid);
            AddAPart(extra, BottomGrid);

            foreach (var item in BottomGrid.Children)
                if (item is Button)
                    item.Style = plainButton;

            if (!(floor is Floor1))
                UpdateWCs(map);
        }

        private void UpdateWCs(Hashtable map)
        {
            if (flatroomsButtons.Contains("WC1"))
            {
                UpperGrid.Children.Remove(((Button)flatroomsButtons["WC1"]));
                flatroomsButtons.Remove("WC1");
                Rooms.Remove("WC1");
                UpperGrid.Children.Remove(((Button)flatroomsButtons["WC2"]));
                flatroomsButtons.Remove("WC2");
                Rooms.Remove("WC2");
            }

            for (int i = 1; i <= 2; i++)
            {
                string key = "WC" + i;

                flatroomsButtons.Add(key, new Button { Text = "WC", Style = plainButton });
                ((Button)flatroomsButtons[key]).CornerRadius = 0;
                Rooms.Add(key, new Room(key));

                AddToGrid(UpperGrid, key, map[key].ToString());
                ChangeColor();
            }
        }


        //добавление части этажа
        private void AddAPart(Hashtable part, Grid grid)
        {
            foreach (string key in part.Keys)
            {
                if (key[0] == '.')
                {
                    if (walls.ContainsKey(key))
                        ((BoxView)walls[key]).IsVisible = true;
                    else
                    {
                        walls.Add(key, new BoxView() { Color = Color.Black });
                        goto addition;
                    }
                }

                //проверка на нахождение кнопки на экране
                if (flatroomsButtons.ContainsKey(key))
                {
                    ((Button)flatroomsButtons[key]).IsVisible = true;
                }
                else
                {
                    flatroomsButtons.Add(key, new Button { Style = plainButton, 
                        Text = key[0] == 'W' ? "WC" : 
                               key[0] == '!' ? "" : key});

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
                            currentIndex = room.EventIndex == -1 ? 0 : room.EventIndex;

                            EventLabel.Text = room.ToString() + ": " + Events[currentIndex].Name;
                            StartTime.Text = "Время начала\n" + Events[currentIndex].Time.ToShortTimeString();
                            EndTime.Text = "Время окончания\n" + (Events[currentIndex].Time + new TimeSpan(0, Events[currentIndex].Duration, 0)).ToShortTimeString();

                            EventLabel.IsVisible = true;
                            InfoGrid.IsVisible = true;
                            //EventButton.IsVisible = true;
                        }
                        else
                        {
                            EventLabel.IsVisible = false;
                            InfoGrid.IsVisible = false;
                            //EventButton.IsVisible = false;
                        }
                    };
                }

                if (first || DateTime.Now - time > new TimeSpan(0, 1, 0))
                {
                    time = DateTime.Now;
                    Update();
                    ChangeColor();
                }

                addition:
                AddToGrid(grid, key, part[key].ToString());
            }
        }

        private void AddToGrid(Grid grid, string key, string val)
        {
            int row = int.Parse(val.Substring(0, 1));
            int rowspan = int.Parse(val.Substring(1, 1));
            int column = int.Parse(val.Substring(2, 2));
            int columnspan = int.Parse(val.Substring(4, 1));
            
            if (key[0] == '.')
            {
                grid.Children.Add((BoxView)walls[key], column, column + columnspan, row, row + rowspan);
                return;
            }

            //добавление на карту
            grid.Children.Add((Button)flatroomsButtons[key], column, column + columnspan, row, row + rowspan);
        }

        //скрытие кнопок
        private void Invalidate()
        {
            foreach (Button button in flatroomsButtons.Values)
                button.IsVisible = false;

            foreach (BoxView wall in walls.Values)
                wall.IsVisible = false;

            ((BoxView)walls["wall"]).IsVisible = !(floor is Floor1);
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
            await DisplayAlert($"{Events[currentIndex].Name}", $"{Events[currentIndex].Info}" +
                $"\nАудитория {Events[currentIndex].Place}" +
                $"\n{Events[currentIndex].SpeakerName}", "Ок");
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