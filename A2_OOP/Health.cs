//File Name:        Health.cs
//Creation Date:    October 11, 2018
/*Description:      Create health item*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Health : Item
    {
        public Health() : base()
        {
            name = "Health";
            coin = 1;
            addition = rng.Next(10, 31);
        }

        public override string GetName()
        {
            return name;
        }
        
        public override int GetBonus()
        {
            return addition;
        }
        
        public override string CalcCost()
        {
            totalCost = addition;

            return Convert.ToString(totalCost);
        }
    }
}
