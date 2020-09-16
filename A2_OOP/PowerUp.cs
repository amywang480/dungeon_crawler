//File Name:        PowerUp.cs
//Creation Date:    October 18, 2018
/*Description:      Create power ups that can be bought, sold, and used*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class PowerUp
    {
        //Store power up info
        protected string name;
        protected int totalCost;
        protected Random rng = new Random();
        protected string powerName = "";

        public PowerUp()
        {

        }
        
        public virtual string GetName()
        {
            return name;
        }

        public virtual string GetDPS()
        {
            return "N/A";
        }

        public virtual string GetDurability()
        {
            if (powerName == "USING")
            {
                return Convert.ToString(0);
            }
            else
            {
                return "N/A";
            }
        }

        public virtual int GetBonus()
        {
            return 0;
        }

        public virtual int GetDamage()
        {
            return 0;
        }

        public virtual int GetDefense()
        {
            return 0;
        }

        public virtual void SetDamage(int damage)
        {

        }

        public virtual void SetDefense(int defense)
        {

        }

        public void SetName(string powerName)
        {
            this.powerName = powerName;
        }

        /// <summary>
        /// Calculate cost of item
        /// </summary>
        /// <returns></returns>
        public virtual string CalcCost()
        {
            return "N/A";
        }
    }
}
