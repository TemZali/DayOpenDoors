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
            //Значение в хеш таблице - "abcd"
            //a - row number
            //b - row span
            //c - column number
            //d - column span
            
            #region Левая часть
            lrooms = new Hashtable();
            //четная сторона
            lrooms.Add("328", "0101");
            lrooms.Add("326", "0111");
            lrooms.Add("324", "0121");
            lrooms.Add("322", "0132");
            lrooms.Add("320", "0151");
            //нечетная сторона
            lrooms.Add("329", "2101");
            lrooms.Add("327", "2111");
            lrooms.Add("325", "2122");
            lrooms.Add("WC1", "2141");
            lrooms.Add("WC", "2151");
            #endregion

            #region Средняя часть
            mrooms = new Hashtable();
            //четная сторона
            mrooms.Add("318", "0101");
            mrooms.Add("316", "0111");
            mrooms.Add("314", "0121");
            mrooms.Add("312", "0131");
            mrooms.Add("310", "0141");
            mrooms.Add("308a", "0151");
            //нечетная сторона
            mrooms.Add("317", "2106");
            #endregion

            #region Правая часть
            rrooms = new Hashtable();
            //четная сторона
            rrooms.Add("308", "0111");
            rrooms.Add("306", "0122");
            rrooms.Add("304", "0141");
            rrooms.Add("302", "0151");

            rrooms.Add("300", "0261");
            //нечетная сторона
            rrooms.Add("313", "2101");
            rrooms.Add("311", "2111");
            rrooms.Add("309", "2121");
            rrooms.Add("307", "2131");
            rrooms.Add("305", "2141");
            rrooms.Add("301", "2152");
            #endregion
        }
    }
}
