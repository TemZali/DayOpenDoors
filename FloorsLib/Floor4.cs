using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorsLib
{
    public class Floor4 : Floor
    {
        public Floor4()
        {
            //Значение в хеш таблице - "abccd"
            //a - row number
            //b - row span
            //cc - column number
            //d - column span

            rooms = new Hashtable();
            otherRooms = new Hashtable();

            rooms.Add("401", "03005");
            rooms.Add("404", "03053");
            rooms.Add("405", "03083");
            rooms.Add("406", "03113");
            rooms.Add("407", "03142");
            rooms.Add("408", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");

            otherRooms.Add("409", "02091");
        }
    }
}
