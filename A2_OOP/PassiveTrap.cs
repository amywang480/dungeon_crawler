//File Name:        PassiveTrap.cs
//Creation Date:    October 11, 2018
/*Description:      Create passive traps that allow the player to first leave the trap
                    within a certain amount of time before damaging the player*/

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class PassiveTrap : Trap
    {
        //Store hit interval
        private int hitInterval;

        public PassiveTrap(string name, string row, string col, string dmgMin, string dmgMax, string hitMin, string hitMax) : 
                           base(name, row, col, dmgMin, dmgMax)
        {
            int adjust = 12;
            double roomSize = 75.5;

            this.x = (int)roomSize * this.col + adjust;
            this.y = (int)roomSize * this.row + adjust--;

            this.hitInterval = rng.Next(Convert.ToInt32(hitMin), Convert.ToInt32(hitMax) + 1);
        }
        
        public override int GetPassiveX()
        {
            return x;
        }

        public override int GetPassiveY()
        {
            return y;
        }

        public override int GetTimeLeft()
        {
            return hitInterval;
        }

        public override string GetName()
        {
            return name;
        }

        public override int GetDamage()
        {
            return damage;
        }

        public override int GetHealth()
        {
            return newHealth;
        }

        public override int GetGridValue(int numRows)
        {
            gridValue = Convert.ToInt32(row) * numRows + Convert.ToInt32(col);
            return gridValue;
        }

        public override bool GetIsArmed()
        {
            return isArmed;
        }
        
        public override void SetArmed(bool isArmed)
        {
            this.isArmed = isArmed;
        }

        /// <summary>
        /// Arm the trap by decreasing player health
        /// </summary>
        /// <param name="health"></param>
        public override void ArmTrap(int health)
        {
            //When armed, decrease player health
            if (isArmed)
            {
                newHealth = health - damage;
            }
            else
            {
                newHealth = 0;
            }
        }

        /// <summary>
        /// Disarm trap after player enters trap room
        /// </summary>
        public override void DisarmTrap()
        {
            //Disarm trap
            isArmed = false;
        }
    }
}
