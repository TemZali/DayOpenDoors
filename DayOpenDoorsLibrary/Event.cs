using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DayOpenDoorsLibrary
{
    public class Event
    {
        public Event(string place, int duration, DateTime time, string name, string type, string speakerName, string info)
        {
            Id = 0;
            Place = place;
            Duration = duration;
            Time = time;
            StartTime = "";
            EndTime = "";
            Name = name;
            Type = type;
            Status = "Ожидается";
            SpeakerName = speakerName;
            Info = info;
            EventColor = Color.Blue;
        }
        public int Id { get; set; }

        public string Place { get; set; }

        public int Duration { get; set; }

        public DateTime Time { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string SpeakerName { get; set; }

        public string Info { get; set; }

        public Color EventColor { get; set; }

        public static void RefreshEventList(List<Event> EventList)
        {
            for (int i = 0; i < EventList.Count; i++)
            {
                EventList[i].EndTime = (EventList[i].Time + new TimeSpan(0, EventList[i].Duration, 0)).Hour.ToString("00") + ":" +
                    (EventList[i].Time + new TimeSpan(0, EventList[i].Duration, 0)).Minute.ToString("00");
                EventList[i].StartTime = EventList[i].Time.Hour.ToString("00") + ":" + EventList[i].Time.Minute.ToString("00");
                if (EventList[i].Time > DateTime.Now)
                {
                    if (EventList[i].Time - DateTime.Now < new TimeSpan(1, 0, 0))
                    {
                        EventList[i].Status = "Скоро начнется";
                        EventList[i].EventColor = Color.Orange;
                    }
                    else
                    {
                        EventList[i].Status = "Ожидается";
                        EventList[i].EventColor = Color.Blue;
                    }
                }
                else if (DateTime.Now - EventList[i].Time < new TimeSpan(0, EventList[i].Duration, 0))
                {
                    EventList[i].Status = "Уже идет";
                    EventList[i].EventColor = Color.Red;
                }
                else
                {
                    if (EventList[i].EventColor != Color.Gray)
                    {
                        EventList[i].Status = "Прошло";
                        EventList[i].EventColor = Color.Gray;
                        Event ev = EventList[i];
                    }
                }
                EventList.Sort((Event a, Event b) =>
                {
                    if (a.Time < b.Time)
                    {
                        if (b.EventColor == Color.Red && a.EventColor == Color.Orange
                        || b.EventColor == Color.Orange && a.EventColor == Color.Blue
                        || b.EventColor == Color.Blue && a.EventColor == Color.Gray)
                        {
                            return 1;
                        }
                        return -1;
                    }
                    else if (b.Time < a.Time)
                    {
                        if (a.EventColor == Color.Red && b.EventColor == Color.Orange
                        || a.EventColor == Color.Orange && b.EventColor == Color.Blue
                        || a.EventColor == Color.Blue && b.EventColor == Color.Gray)
                        {
                            return -1;
                        }
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });
            }
        }

    }
}
