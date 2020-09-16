//Author:           Amy Wang
//File Name:        Weapon.cs
//Project Name:     A2_OOP
//Creation Date:    October 18, 2018
//Modified Date:    October 22, 2018
/*Description:      Create weapons which include melee and ranged*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Weapon : PowerUp
    {
        //Store weapon info
        protected int hitInterval;
        protected int damage;
        protected int DPS;
        protected int odds;

        public Weapon() : base()
        {

        }
        
        public Weapon(string name)
        {
            this.name = name;
        }
    }
}
