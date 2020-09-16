//Author:           Amy Wang
//File Name:        Player.cs
//Project Name:     A2_OOP
//Creation Date:    October 11, 2018
//Modified Date:    October 22, 2018
/*Description:      Control actions of the player of the game who moves around the maze
                    while buying, selling, and using items*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A2_OOP
{
    class Player
    {
        //Store player location
        private int x;
        private int y;

        //Store player info
        private int playerGridValue;
        private int playerHealth;
        private int playerCash;
        private bool isUsing;
        private PowerUp currentlyUsing;
        private List<PowerUp> playerInventory = new List<PowerUp>();
        private int itemNumSelect;
        private int maxSize = 6;

        //Generate random numbers
        Random rng = new Random();

        //Store keyboard state
        KeyboardState kb;
        KeyboardState prevKb;
       
        public Player(int startRow, int startCol, int cashMin, int cashMax, int healthMin, int healthMax, int numRows)
        {
            int adjust = 12;
            double roomSize = 75.5;
            
            x = (int)roomSize * startCol + adjust;
            y = (int)roomSize * startRow + adjust--;

            playerGridValue = Convert.ToInt32(startRow) * numRows + Convert.ToInt32(startCol);
            playerCash = rng.Next(cashMin, cashMax + 1);
            playerHealth = rng.Next(healthMin, healthMax + 1);

            playerInventory.Add(new Weapon("NO WEAPON"));
            playerInventory.Add(new Helmet("NO HELMET"));
            playerInventory.Add(new Body("NO BODY ARMOUR"));
        }

        public int GetCash()
        {
            return playerCash;
        }

        public int GetHealth()
        {
            return playerHealth;
        }

        public List<PowerUp> GetPlayerInventory()
        {
            return playerInventory;
        }
        
        public int GetPlayerGridValue()
        {
            return playerGridValue;
        }
        
        public int GetItemSelect()
        {
            return itemNumSelect;
        }
        
        public PowerUp GetPowerUpUsing()
        {
            return currentlyUsing;
        }

        public bool GetIsUsing()
        {
            return isUsing;
        }

        public int GetPlayerX()
        {
            return x;
        }

        public int GetPlayerY()
        {
            return y;
        }
        
        public int GetMaxSize()
        {
            return maxSize;
        }

        public void SetIsUsing(bool isUsing)
        {
            this.isUsing = isUsing;
        }
        
        public void SetItemSelect(int itemNumSelect)
        {
            this.itemNumSelect = itemNumSelect;
        }

        public void SetHealth(int playerHealth)
        {
            this.playerHealth = playerHealth;
        }
        
        /// <summary>
        /// Adjust player grid value
        /// </summary>
        /// <param name="gridChange"></param>
        public void Move(int gridChange)
        {
            playerGridValue += gridChange;
        }
        
        /// <summary>
        /// Determine where to place item bought
        /// </summary>
        /// <param name="itemBought"></param>
        public void Buy(PowerUp itemBought)
        {
            switch (itemBought.GetName())
            {
                case "Sword":
                    playerInventory[0] = itemBought;
                    break;
                case "Crossbow":
                    playerInventory[0] = itemBought;
                    break;
                case "Bucket":
                    playerInventory[1] = itemBought;
                    break;
                case "Cookie Sheet":
                    playerInventory[2] = itemBought;
                    break;
                default:
                    playerInventory.Add(itemBought);
                    break;
            }

            //Adjust player cash
            playerCash -= Convert.ToInt32(itemBought.CalcCost());
        }
    
        /// <summary>
        /// Determine item to be removed after selling
        /// </summary>
        /// <param name="newCost"></param>
        public void Sell(int newCost)
        {
            switch (itemNumSelect)
            {
                case 0:
                    playerInventory[0] = new Weapon("NO WEAPON");
                    break;
                case 1:
                    playerInventory[1] = new Helmet("NO HELMET");
                    break;
                case 2:
                    playerInventory[2] = new Body("NO BODY ARMOUR");
                    break;
                default:
                    playerInventory.RemoveAt(itemNumSelect);
                    break;
            }
            
            //Adjust player cash
            playerCash += newCost;
        }
   
        /// <summary>
        /// Determine item to be used
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bonus"></param>
        public void Use(string name, int bonus)
        {
            //Find item to use
            switch (name)
            {
                case "Health":
                    playerHealth += (int)(playerHealth * (bonus / 100.0));
                    break;
                case "Sword":
                case "Crossbow":
                case "Bucket":
                case "Cookie Sheet":
                    isUsing = true;
                    currentlyUsing = playerInventory[itemNumSelect];
                    currentlyUsing.SetName("USING");
                    break;
            }
            
            //Update location of item removed in inventory
            switch (itemNumSelect)
            {
                case 0:
                    playerInventory[0] = new Weapon("NO WEAPON");
                    break;
                case 1:
                    playerInventory[1] = new Helmet("NO HELMET");
                    break;
                case 2:
                    playerInventory[2] = new Body("NO BODY ARMOUR");
                    break;
                default:
                    playerInventory.RemoveAt(itemNumSelect);
                    break;
            }
        }
    }
}
