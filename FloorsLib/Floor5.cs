using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor5:Floor
    {
        public Floor5()
        {
            //Значение в хеш таблице - "abccd"
            //a - row number
            //b - row span
            //cc - column number
            //d - column span

            rooms.Add("!1!", "03014");
            rooms.Add("!2!", "03053");
            rooms.Add("503", "03083");
            rooms.Add("504", "03113");
            rooms.Add("505", "03142");
            rooms.Add("506", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");

            hall.Add(".1.5", "01019");
            hall.Add(".2.5", "01108");
            hall.Add(".3.5", "31009");
            hall.Add(".4.5", "31097");

            otherRooms.Add("507", "02091");
        }
    }
}
