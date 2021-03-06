﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor2:Floor
    {
        public Floor2()
        {
            //Значение в хеш таблице - "abccd"
            //a - row number
            //b - row span
            //cc - column number
            //d - column span

            rooms.Add("201", "03014");
            rooms.Add("204", "03053");
            rooms.Add("205", "03083");
            rooms.Add("206", "03113");
            rooms.Add("207", "03142");
            rooms.Add("208", "03162");

            rooms.Add("WC1", "21031");
            rooms.Add("WC2", "21021");

            hall.Add(".1.2", "01019");
            hall.Add(".2.2", "01108");
            hall.Add(".3.2", "31009");
            hall.Add(".4.2", "31097");
        }
    }
}
