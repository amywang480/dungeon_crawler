//File Name:        Melee.cs
//Creation Date:    October 18, 2018
/*Description:      Create melee weapons which are swords*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Melee : Weapon
    {
        //Store durability
        private int durabilityMelee;

        public Melee()
        {
            name = "Sword";
            durabilityMelee = rng.Next(5, 16);
            hitInterval = rng.Next(1000, 3001);
            damage = rng.Next(20, 51);
            odds = rng.Next(40, 91);
            DPS = (int)(damage / (double)(hitInterval / 1000));
        }
    
        public override string GetName()
        {
            return name;
        }

        public override string GetDPS()
        {
            return Convert.ToString(DPS);
        }

        public override string GetDurability()
        {
            return Convert.ToString(durabilityMelee);
        }

        public override int GetDamage()
        {
            return damage;
        }

        public override void SetDamage(int damage)
        {
            this.damage = damage;
        }
        
        public override string CalcCost()
        {
            totalCost = (DPS * 3) + (durabilityMelee * 2);

            return Convert.ToString(totalCost);
        }
    }
}
