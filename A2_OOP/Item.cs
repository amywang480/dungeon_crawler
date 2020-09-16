//File Name:        Item.cs
//Creation Date:    October 11, 2018
/*Description:      Create items which includes health and time*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Item : PowerUp
    {
        //Store item info
        protected int coin;
        protected int addition;

        public Item() : base()
        {

        }
    }
}
