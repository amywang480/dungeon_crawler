//Author:           Amy Wang
//File Name:        Shop.cs
//Project Name:     A2_OOP
//Creation Date:    October 11, 2018
//Modified Date:    October 22, 2018
/*Description:      Store shops where the player can buy items and sell items*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_OOP
{
    class Shop : Room
    {
        //Store shop info
        private string name;
        private int shopCash;
        private int percentResell;
        private List<PowerUp> shopInventory = new List<PowerUp>();
        private int invSize;
        private int shopGridValue;

        //Store shop image coordinates
        private int x;
        private int y;
        
        //Add random items to inventory
        private Random rng = new Random();
        
        public Shop(string name, string row, string col, string layout, string cashMin, string cashMax, string profMin,
                    string profMax, string invMin, string invMax) : base(name, row, col, layout)
        {
            int adjust = 12;
            double roomSize = 75.5;

            this.name = name;
            this.row = Convert.ToInt32(row);
            this.col = Convert.ToInt32(col);

            x = (int)roomSize * this.col + adjust;
            y = (int)roomSize * this.row + adjust--;

            invSize = rng.Next(Convert.ToInt32(invMin), Convert.ToInt32(invMax) + 1);
            shopCash = rng.Next(Convert.ToInt32(cashMin), Convert.ToInt32(cashMax) + 1);
            percentResell = rng.Next(Convert.ToInt32(profMin), Convert.ToInt32(profMax) + 1);
        }
        
        public override int GetShopX()
        {
            return x;
        }

        public override int GetShopY()
        {
            return y;
        }

        public override List<PowerUp> GetInventory()
        {
            return shopInventory;
        }

        public override int GetPercentResell()
        {
            return percentResell;
        }

        public override int GetShopCash()
        {
            return shopCash;
        }

        public override int GetShopGridValue()
        {
            return shopGridValue;
        }

        public override string GetLayout()
        {
            return layout;
        }

        public override void SetGridValue(int numRows)
        {
            shopGridValue = Convert.ToInt32(row) * numRows + Convert.ToInt32(col);
        }

        /// <summary>
        /// Remove items when sold to player
        /// </summary>
        /// <param name="index"></param>
        /// <param name="itemBought"></param>
        public override void Sell(int index, PowerUp itemBought)
        {
            shopInventory.RemoveAt(index);
            shopCash += Convert.ToInt32(itemBought.CalcCost());
        }

        /// <summary>
        /// When buying item from player, adjust shop cash
        /// </summary>
        /// <param name="newCost"></param>
        public override void Buy(int newCost)
        {
            shopCash -= newCost;
        }

        /// <summary>
        /// Prepare shop inventory
        /// </summary>
        public override void PrepInventory()
        {
            for (int i = 0; i < invSize; ++i)
            {
                int determineItems = rng.Next(1, 8);

                switch (determineItems)
                {
                    case 1:
                        shopInventory.Add(new Health());
                        break;
                    case 2:
                        shopInventory.Add(new Health());
                        break;
                    case 3:
                        shopInventory.Add(new Time());
                        break;
                    case 4:
                        shopInventory.Add(new Time());
                        break;
                    case 5:
                        DetermineWeapon();
                        break;
                    case 6:
                        shopInventory.Add(new Helmet());
                        break;
                    case 7:
                        shopInventory.Add(new Body());
                        break;
                }
            }
        }
  
        /// <summary>
        /// Determine which weapon will be in inventory
        /// </summary>
        public void DetermineWeapon()
        {
            int determineWeapon = rng.Next(1, 3);

            if (determineWeapon == 1)
            {
                shopInventory.Add(new Melee());
            }
            else
            {
                shopInventory.Add(new Ranged());
            }
        }
    }
}
