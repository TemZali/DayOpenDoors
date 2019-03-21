using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DayOpenDoors
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WayPage : ContentPage
	{
		public WayPage ()
		{
			InitializeComponent ();
            WayLabel.Text = "Как до нас добраться:\n " +
                "Мы находимся по адресу г.Москва, ул.Кочновский проезд, дом 3\n " +
                "Станция метро «Аэропорт» (зеленая ветка, вторая станция от кольцевой), первый вагон " +
                "из центра. Выход к ТЦ «Галерея Аэропорт». Здание ТЦ необходимо обойти справа. " +
                "Далее идите по улице Черняховского, поверните на улицу Планетную, затем в " +
                "Кочновский проезд.";

        }
	}
}