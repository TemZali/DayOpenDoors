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
            //Значение в хеш таблице - "abcd"
            //a - row number
            //b - row span
            //c - column number
            //d - column span

            #region Левая часть
            lrooms = new Hashtable();
            lrooms.Add("Типография", "0306");
            #endregion

            #region Средняя часть
            mrooms = new Hashtable();
            mrooms.Add("Вход", "0106");
            mrooms.Add("Гардероб", "2106");
            #endregion

            #region Средняя часть
            rrooms = new Hashtable();
            rrooms.Add("Столовая", "0307");
            #endregion
        }
    }
}
