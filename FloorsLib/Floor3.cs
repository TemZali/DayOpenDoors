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

            rooms.Add("301", "03014");
            rooms.Add("304", "03053");
            rooms.Add("305", "03083");
            rooms.Add("306", "03113");
            rooms.Add("307", "03142");
            rooms.Add("308", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");

            hall.Add(".1.3", "01019");
            hall.Add(".2.3", "01108");
            hall.Add(".3.3", "31009");
            hall.Add(".4.3", "31097");

            otherRooms.Add("309", "02091");
        }
    }
}
