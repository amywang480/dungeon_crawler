//Author:           Amy Wang
//File Name:        Item.cs
//Project Name:     A2_OOP
//Creation Date:    October 11, 2018
//Modified Date:    October 22, 2018
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
