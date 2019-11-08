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
        //Значение в хеш таблице - "abccd"
        //a - row number
        //b - row span
        //cc - column number
        //d - column span

        protected Hashtable rooms = new Hashtable();
        protected Hashtable hall = new Hashtable();
        protected Hashtable otherRooms = new Hashtable();

        public Hashtable Rooms { get => rooms; }
        public Hashtable Hall { get => hall; }
        public Hashtable OtherRooms { get => otherRooms; }
    }
}
