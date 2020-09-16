//File Name:        Ranged.cs
//Creation Date:    October 18, 2018
//Description:      Create ranged weapons

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Ranged : Weapon
    {
        private double dps;
        private double determineInterval;

        public Ranged()
        {
            name = "Crossbow";
            hitInterval = rng.Next(250, 1001);
            damage = rng.Next(5, 21);
            odds = rng.Next(30, 91);
            determineInterval = Math.Round(hitInterval / 1000.0, 2);
            dps = damage / determineInterval;
        }

        public override string GetName()
        {
            return name;
        }

        public override string CalcCost()
        {
            double determineOdds = Math.Round(odds / 100.0, 2);
            double cost = dps * 3.0 * determineOdds;
            totalCost = Convert.ToInt32(cost);
            return Convert.ToString(totalCost);
        }
    
        public override string GetDPS()
        {
            DPS = Convert.ToInt32(dps);
            return Convert.ToString(DPS);
        }
    }
}
