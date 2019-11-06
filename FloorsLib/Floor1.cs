using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor1:Floor
    {
        public Floor1()
        {
            //Значение в хеш таблице - "abccd"
            //a - row number
            //b - row span
            //cc - column number
            //d - column span

            rooms = new Hashtable();
            otherRooms = new Hashtable();

            rooms.Add("401", "03014");
            rooms.Add("404", "03053");
            rooms.Add("405", "03083");
            rooms.Add("406", "03113");
            rooms.Add("407", "03142");
            rooms.Add("408", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");
        }
    }
}
