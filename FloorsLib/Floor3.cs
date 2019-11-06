using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor3:Floor
    {
        public Floor3()
        {
            //Значение в хеш таблице - "abccd"
            //a - row number
            //b - row span
            //cc - column number
            //d - column span

            rooms = new Hashtable();
            otherRooms = new Hashtable();

            rooms.Add("301", "03005");
            rooms.Add("304", "03053");
            rooms.Add("305", "03083");
            rooms.Add("306", "03113");
            rooms.Add("307", "03142");
            rooms.Add("308", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");

            otherRooms.Add("309", "02091");
        }
    }
}
