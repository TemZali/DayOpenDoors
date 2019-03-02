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
            //Значение в хеш таблице - "abcd"
            //a - row number
            //b - row span
            //c - column number
            //d - column span

            #region Левая часть
            lrooms = new Hashtable();
            //четная сторона
            lrooms.Add("626", "0101");
            lrooms.Add("624", "0111");
            lrooms.Add("620", "0121");
            lrooms.Add("618", "0132");
            lrooms.Add("616", "0151");
            //нечетная сторона
            lrooms.Add("625", "2101");
            lrooms.Add("623", "2111");
            lrooms.Add("621", "2121");
            lrooms.Add("619", "2131");
            lrooms.Add("617", "2141");
            lrooms.Add("WC", "2151");
            #endregion

            #region Средняя часть
            mrooms = new Hashtable();
            mrooms.Add("622", "0306");
            #endregion

            #region Правая часть
            rrooms = new Hashtable();
            //четная сторона
            rrooms.Add("614", "0101");
            rrooms.Add("612", "0111");
            rrooms.Add("610", "0121");
            rrooms.Add("608", "0131");
            rrooms.Add("606", "0141");
            rrooms.Add("604", "0151");
            rrooms.Add("602", "0161");

            rrooms.Add("600", "1161");
            //нечетная сторона
            rrooms.Add("607", "2101");
            rrooms.Add("605", "2112");
            rrooms.Add("603", "2131");
            rrooms.Add("601а", "2141");
            rrooms.Add("601", "2152");
            #endregion
        }
    }
}
