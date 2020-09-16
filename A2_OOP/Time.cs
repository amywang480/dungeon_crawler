//Author:           Amy Wang
//File Name:        Time.cs
//Project Name:     A2_OOP
//Creation Date:    October 11, 2018
//Modified Date:    October 22, 2018
/*Description:      Create time item*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Time : Item
    {
        public Time() : base()
        {
            name = "Time";
            coin = 3;
            addition = rng.Next(5, 11);
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
            totalCost = coin * addition;

            return Convert.ToString(totalCost);
        }
    }
}
