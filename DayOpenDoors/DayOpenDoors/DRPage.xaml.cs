using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DayOpenDoorsLibrary;

namespace DayOpenDoors
{

    public partial class DRPage : ContentPage
    {
        List<Food> First { get; set; }
        List<Food> Second { get; set; }
        List<Food> Dessert { get; set; }
        List<Food> Drink { get; set; }
        List<Food> Salat { get; set; }
        List<Food> Snack { get; set; }
        public DRPage()
        {
            First = new List<Food>()
            {
                new Food("Цена - 120 руб","Комплексный обед")
            };
            Second = new List<Food>()
            {

            };
            Dessert = new List<Food>()
            {

            };
            Drink = new List<Food>()
            {

            };
            Salat = new List<Food>()
            {

            };
            Snack = new List<Food>()
            {

            };
            InitializeComponent();
        }
        private async void ShowFood(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Выберите раздел меню", "Отмена", null, "Первое блюдо", "Второе блюдо", "Десерт", "Напиток", "Салат", "Снеки");
            switch (result)
            {
                case "Первое блюдо":
                    break;
                case "Второе блюдо":
                    break;
                case "Десерт":
                    break;
                case "Напиток":
                    break;
                case "Салат":
                    break;
                case "Снеки":
                    break;
            }
        }
    }
}