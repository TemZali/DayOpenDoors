using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public List<Event> EventList { get; set; }
        public MapPage(List<Event> events)
        {
            EventList = events;
            InitializeComponent();
        }

        private async void ShowFloor(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Выберите этаж", "Отмена", null, "1 Этаж", "2 Этаж", "3 Этаж", "4 Этаж", "5 Этаж", "6 Этаж");
            FloorButton.Text = result;
            switch (result[0])
            {
                case '1':
                    break;
                case '2':
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                case '6':
                    break;
            }
        }
    }
}