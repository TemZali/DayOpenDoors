using System;
using System.Collections.Generic;
using DayOpenDoorsLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanPage : ContentPage
    {
        public List<Event> Lections { get; set; }

        public List<Event> Master_Classes { get; set; }

        public PlanPage(List<Event> events)
        {
            InitializeComponent();
            Event.RefreshEventList(events);
            Lections = new List<Event>();
            Master_Classes = new List<Event>();
            foreach (Event @event in events)
            {
                if (@event.Type == "Лекция")
                {
                    Lections.Add(@event);
                }
                else
                {
                    Master_Classes.Add(@event);
                }
            }
            this.BindingContext = this;
        }

        private async void ShowEvents(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Выберите тип мероприятия", "отмена", null, "Лекции", "Мастер-классы");
            TypeButton.Text = result;
            if (result == "Лекции")
            {
                EventListView.ItemsSource = Lections;
            }
            else
            {
                EventListView.ItemsSource = Master_Classes;
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            EventListView.SelectedItem = null;
        }
    }
}