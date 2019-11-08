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

            rooms.Add("!3!", "03003");
            rooms.Add("!4!", "03033");
            rooms.Add("!5!", "03063");
            rooms.Add("!6!", "03093");
            rooms.Add("Гардероб", "03126");
        }
    }
}
