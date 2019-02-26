using System;
using System.Collections.Generic;
using System.Text;

namespace DayOpenDoorsLibrary
{
    public class Food
    {
        public Food(string price, string name)
        {
            Price = price ?? throw new ArgumentNullException(nameof(price));

            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Price { get; set; }

        public string Name { get; set; }

    }
}
