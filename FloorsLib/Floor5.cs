using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor5:Floor
    {
        public Floor5()
        {
            //Значение в хеш таблице - "abcd"
            //a - row number
            //b - row span
            //c - column number
            //d - column span

            #region Левая часть
            lrooms = new Hashtable();
            //четная сторона
            lrooms.Add("536", "0101");
            lrooms.Add("534", "0111");
            lrooms.Add("532", "0121");
            lrooms.Add("530", "0132");
            lrooms.Add("528", "0151");
            lrooms.Add("526", "0151");

            lrooms.Add("540", "1101");
            //нечетная сторона
            lrooms.Add("515", "2101");
            lrooms.Add("513", "2112");
            lrooms.Add("511", "2131");
            lrooms.Add("WC1", "2141");
            lrooms.Add("WC", "2151");
            #endregion

            #region Средняя часть
            mrooms = new Hashtable();
            //четная сторона
            mrooms.Add("526", "0101");
            mrooms.Add("524", "0111");
            mrooms.Add("522", "0121");
            mrooms.Add("520", "0131");
            mrooms.Add("518", "0141");
            mrooms.Add("516", "0151");
            //нечетная сторона
            mrooms.Add("509", "2106");
            #endregion

            #region Правая часть
            rrooms = new Hashtable();
            //четная сторона
            rrooms.Add("514", "0101");
            rrooms.Add("512", "0111");
            rrooms.Add("510", "0121");
            rrooms.Add("508", "0131");
            rrooms.Add("506", "0141");
            rrooms.Add("504", "0151");
            rrooms.Add("502", "0161");

            rrooms.Add("500", "1161");
            //нечетная сторона
            rrooms.Add("507", "2101");
            rrooms.Add("505", "2112");
            rrooms.Add("503", "2132");
            rrooms.Add("501", "2152");
            #endregion
        }
    }
}
