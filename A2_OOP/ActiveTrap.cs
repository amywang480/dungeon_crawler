//File Name:        ActiveTrap.cs
//Creation Date:    October 11, 2018
/*Description:      Store active traps that damage player immediately and continue to 
                    damage player until they leave the room*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class ActiveTrap : Trap
    {
        //Store escape interval
        private int escapeInterval;

        public ActiveTrap(string name, string row, string col, string dmgMin, string dmgMax, string escMin, string escMax) : 
                          base(name, row, col, dmgMin, dmgMax)
        {
            int adjust = 12;
            double roomSize = 75.5;

            this.x = (int)roomSize * this.col + adjust;
            this.y = (int)roomSize * this.row + adjust--;
            
            this.escapeInterval = rng.Next(Convert.ToInt32(escMin), Convert.ToInt32(escMax) + 1);
        }
        
        public override int GetActiveX()
        {
            return x;
        }

        public override int GetActiveY()
        {
            return y;
        }

        public override int GetTimeLeft()
        {
            return escapeInterval;
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

        public override string GetName()
        {
            return name;
        }

        public override int GetDamage()
        {
            return damage;
        }
        
        public override void SetArmed(bool isArmed)
        {
            this.isArmed = isArmed;
        }

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

        public override void DisarmTrap()
        {
            //Disarm trap
            isArmed = false;
        }
    }
}
