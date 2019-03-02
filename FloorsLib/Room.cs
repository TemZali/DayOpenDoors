using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorsLib
{
    public class Room
    {
        public string Number { get; set; }
        public int IsBusy { get; set; }


        public Room(string number)
        {
            Number = number;
        }
    }
}
