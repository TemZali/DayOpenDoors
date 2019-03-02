using System;
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
            //Значение в хеш таблице - "abcd"
            //a - row number
            //b - row span
            //c - column number
            //d - column span

            #region Левая часть
            lrooms = new Hashtable();
            //четная сторона
            lrooms.Add("222", "0303");
            lrooms.Add("219", "0132");
            lrooms.Add("214", "0151");
            //нечетная сторона
            lrooms.Add("216", "2131");
            lrooms.Add("217", "2141");
            lrooms.Add("WC", "2151");
            #endregion

            #region Средняя часть
            mrooms = new Hashtable();
            //четная сторона
            mrooms.Add("204", "0101");
            mrooms.Add("206", "0111");
            mrooms.Add("208", "0122");
            mrooms.Add("210", "0141");
            mrooms.Add("212", "0151");
            //нечетная сторона
            mrooms.Add("205", "2106");
            #endregion

            #region Правая часть
            rrooms = new Hashtable();
            //четная сторона
            rrooms.Add("Библиотека", "0307");
            #endregion
        }
    }
}
