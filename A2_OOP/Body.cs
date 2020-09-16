//Author:           Amy Wang
//File Name:        Body.cs
//Project Name:     A2_OOP
//Creation Date:    October 18, 2018
//Modified Date:    October 22, 2018
/*Description:      Create body armour which is a cookie sheet*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Body : Armour
    {
        public Body()
        {
            name = "Cookie Sheet";
            defenseModifier = rng.Next(15, 31);
            durabilityArmour = rng.Next(10, 16);
        }

        public Body(string name)
        {
            this.name = name;
        }

        public override string GetName()
        {
            return name;
        }
        
        public override string GetDurability()
        {
            if (name == "NO BODY ARMOUR" && powerName != "USING")
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

        public override string CalcCost()
        {
            if (name == "NO BODY ARMOUR")
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
