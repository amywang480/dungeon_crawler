//File Name:        Armour.cs
//Creation Date:    October 18, 2018
/*Description:      Create armour which includes helment and body armour*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Armour : PowerUp
    {
        //Store armour info
        protected int defenseModifier;
        protected int durabilityArmour;

        public Armour() : base()
        {

        }
    }
}
