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

            rooms.Add("101", "21003");
            rooms.Add("102", "21043");
            rooms.Add("103", "21072");
            rooms.Add("104", "21103");

            rooms.Add("!3!", "01004");
            rooms.Add("!4!", "01045");
            rooms.Add("!5!", "01094");

            hall.Add(".1.1", "01003");
            hall.Add(".2.1", "01045");
            hall.Add(".3.1", "01108");

            rooms.Add("Гардероб", "03135");
        }
    }
}
