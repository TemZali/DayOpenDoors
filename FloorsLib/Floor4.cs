using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorsLib
{
    public class Floor4 : Floor
    {
        public Floor4()
        {
            //Значение в хеш таблице - "abcd"
            //a - row number
            //b - row span
            //c - column number
            //d - column span

            #region Левая часть
            lrooms = new Hashtable();
            //четная сторона
            lrooms.Add("435", "0102");
            lrooms.Add("433", "0121");
            lrooms.Add("431", "0131");
            lrooms.Add("429", "0141");
            lrooms.Add("427", "0151");

            lrooms.Add("436", "1101");
            //нечетная сторона
            lrooms.Add("434", "2101");
            lrooms.Add("432", "2112");
            lrooms.Add("430", "2131");
            lrooms.Add("WC1", "2141");
            lrooms.Add("WC", "2151");
            #endregion

            #region Средняя часть
            mrooms = new Hashtable();
            //четная сторона
            mrooms.Add("421", "0101");
            mrooms.Add("419", "0111");
            mrooms.Add("417", "0122");
            mrooms.Add("415", "0141");
            mrooms.Add("413", "0151");
            //нечетная сторона
            mrooms.Add("422", "2101");
            mrooms.Add("420", "2111");
            mrooms.Add("418", "2121");
            mrooms.Add("416", "2131");
            mrooms.Add("414", "2141");
            mrooms.Add("412", "2151");
            #endregion

            #region Правая часть
            rrooms = new Hashtable();
            //четная сторона
            rrooms.Add("411", "0101");
            rrooms.Add("409", "0112");
            rrooms.Add("403", "0133");

            rrooms.Add("400", "0261");
            //нечетная сторона
            rrooms.Add("408", "2101");
            rrooms.Add("406", "2112");
            rrooms.Add("404", "2132");
            rrooms.Add("402", "2152");
            #endregion
        }
    }
}
