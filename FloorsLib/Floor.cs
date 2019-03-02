using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FloorsLib
{
    public class Floor
    {
        //Значение в хеш таблице - "abcd"
        //a - row number
        //b - row span
        //c - column number
        //d - column span
        protected Hashtable lrooms;
        protected Hashtable mrooms;
        protected Hashtable rrooms;

        public Hashtable Lrooms { get => lrooms; }
        public Hashtable Mrooms { get => mrooms; }
        public Hashtable Rrooms { get => rrooms; }
    }
}
