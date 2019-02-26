using Xamarin.Forms;
using DayOpenDoorsLibrary;

namespace DayOpenDoors
{
    public partial class EventPage : ContentPage
    {
        public EventPage(Event ev)
        {
            InitializeComponent();
            Title = ev.Name;
            TimeLabel.Text = ev.Time.ToString();
            InfoLabel.Text = ev.Info;
            SpeakerNameLabel.Text = ev.SpeakerName;
        }
    }
}