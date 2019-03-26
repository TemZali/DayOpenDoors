using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorsLib
{
    public class Room
    {
        public int EventIndex { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// 0 - Не занят
        /// 1 - Скоро будет занят
        /// 2 - Занят
        /// </summary>
        public int IsBusy { get; set; }
        public string Color { get; private set; }

        public Room(string number)
        {
            Number = number;
            IsBusy = 0;
            EventIndex = -1;

            if (Number == "WC1" || Number == "WC2")
                Color = Number[2] == '1' ? "#ff66ff" : "#42aaff"; //Розовый(женский) : Голубой(мужской)
            else
                Color = "#eeeeee";
        }

        public void ChangeColor()
        {
            if (Number[0] != 'W')
                switch (IsBusy)
                {
                    case 1:
                        Color = "#faf88e"; //желтый
                        return;
                    case 2:
                        Color = "#ff6666"; //красный
                        return;
                    default:
                        Color = "#eeeeee"; //серый
                        return;
                }
        }

        public override string ToString()
        {
            return (Number[0] >= '0' && Number[0] <= '9') ? "Кабинет - " + Number : (Number[0] == 'W') ? "Туалет" : Number;
        }
    }
}
