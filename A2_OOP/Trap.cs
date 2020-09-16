//File Name:        Trap.cs
//Creation Date:    October 11, 2018
/*Description:      Store traps that damage the player by reducing their health. Traps include active
                    and passive traps*/

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Trap
    {
        //Store trap info
        protected string name;
        protected int row;
        protected int col;
        protected int damage;
        protected int gridValue;
        protected bool isArmed = true;
        protected int x;
        protected int y;
        protected int newHealth = 0;
        protected bool isVisited = false;

        //Generate random numbers
        protected Random rng = new Random();
    
        public Trap(string name, string row, string col, string dmgMin, string dmgMax)
        {
            this.name = name;
            this.row = Convert.ToInt32(row);
            this.col = Convert.ToInt32(col);
            this.damage = rng.Next(Convert.ToInt32(dmgMin), Convert.ToInt32(dmgMax) + 1);
        }
        
        public virtual int GetPassiveX()
        {
            return -1;
        }

        public virtual int GetPassiveY()
        {
            return -1;
        }

        public virtual int GetActiveX()
        {
            return -1;
        }

        public virtual int GetActiveY()
        {
            return -1;
        }

        public virtual int GetTimeLeft()
        {
            return -1;
        }

        public virtual bool GetIsArmed()
        {
            return true;
        }

        public virtual int GetGridValue(int numRows)
        {
            return gridValue;
        }

        public virtual string GetName()
        {
            return name;
        }

        public virtual int GetDamage()
        {
            return 0;
        }

        public virtual int GetHealth()
        {
            return -1;
        }
        
        public virtual void SetArmed(bool isArmed)
        {

        }
        
        public virtual void ArmTrap(int health)
        {
            
        }

        public virtual void DisarmTrap()
        {

        }
    }
}
