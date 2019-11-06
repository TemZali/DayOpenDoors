using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor6:Floor
    {
        public Floor6()
        {
            //Значение в хеш таблице - "abccd"
            //a - row number
            //b - row span
            //cc - column number
            //d - column span
            
            rooms = new Hashtable();
            otherRooms = new Hashtable();

            rooms.Add("601", "01002");
            rooms.Add("604", "21002");
            rooms.Add("602", "01023");
            rooms.Add("603", "11023");

            rooms.Add("607", "03051");
            rooms.Add("608", "03061");
            rooms.Add("609", "02071");
            rooms.Add("610", "03082");
            rooms.Add("611", "03102");
            rooms.Add("612", "03122");
            rooms.Add("613", "03142");
            rooms.Add("614", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");

            otherRooms.Add("615", "02091");
        }
    }
}
