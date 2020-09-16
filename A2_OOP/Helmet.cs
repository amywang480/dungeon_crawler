//Author:           Amy Wang
//File Name:        Helmet.cs
//Project Name:     A2_OOP
//Creation Date:    October 18, 2018
//Modified Date:    October 22, 2018
/*Description:      Create helmets which are buckets*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Helmet : Armour
    {
        public Helmet()
        {
            name = "Bucket";
            defenseModifier = rng.Next(10, 21);
            durabilityArmour = rng.Next(5, 11);
        }

        public Helmet(string name)
        {
            this.name = name;
        }

        public override string GetName()
        {
            return name;
        }

        public override string GetDurability()
        {
            if (name == "NO HELMET" && powerName != "USING")
            {
                return "N/A";
            }
            else
            {
                return Convert.ToString(durabilityArmour);
            }
        }

        public override int GetDefense()
        {
            return defenseModifier;
        }

        public override void SetDefense(int defenseModifier)
        {
            this.defenseModifier = defenseModifier;
        }

        /// <summary>
        /// Calculate cost of helmet
        /// </summary>
        /// <returns></returns>
        public override string CalcCost()
        {
            if (name == "NO HELMET")
            {
                return "N/A";
            }
            else
            {
                totalCost = (defenseModifier * 3) + durabilityArmour;
                return Convert.ToString(totalCost);
            }
        }
    }
}
