//Author:           Amy Wang
//File Name:        Room.cs
//Project Name:     A2_OOP
//Creation Date:    October 11, 2018
//Modified Date:    October 22, 2018
/*Description:      Store rooms of dungeon. Room class itself is used for passage rooms*/

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
    class Room
    {
        //Store trap
        private Trap trap;

        //Store room info
        private string name;
        protected int row;
        protected int col;
        private int roomGridValue;
        protected string layout;

        //Store room image and rectangle
        private Texture2D roomImg;
        private Rectangle roomRec;

        //Store image size
        private double imgWH = 75.5;

        //Store possible directions to move in
        private bool[] directions = new bool[4];

        //Determine when to reveal room
        private bool reveal = false;

        public Room(string name, string row, string col, string layout)
        {
            this.name = name;
            this.row = Convert.ToInt32(row);
            this.col = Convert.ToInt32(col);
            this.layout = layout;
        }
        
        public bool[] GetDirections()
        {
            return directions;
        }
        
        public Texture2D GetRoomImage()
        {
            return roomImg;
        }

        public Rectangle GetRoomRec()
        {
            return roomRec;
        }
        
        public virtual int GetShopX()
        {
            return -1;
        }

        public virtual int GetShopY()
        {
            return -1;
        }

        public virtual int GetShopGridValue()
        {
            return -1;
        }

        public virtual string GetLayout()
        {
            return layout;
        }
        
        public int GetImageWH()
        {
            return (int)imgWH;
        }

        public bool GetReveal()
        {
            return reveal;
        }

        public Trap GetTrap()
        {
            return trap;
        }

        public int GetGridValue(int numRows)
        {
            roomGridValue = Convert.ToInt32(row) * numRows + Convert.ToInt32(col);
            return roomGridValue;
        }

        public virtual List<PowerUp> GetInventory()
        {
            List<PowerUp> powerup = new List<PowerUp>();
            return powerup;
        }
        
        public virtual int GetShopCash()
        {
            return 0;
        }

        public virtual int GetPercentResell()
        {
            return 0;
        }

        public virtual void SetGridValue(int numRows)
        {

        }

        public void SetRoomImage(Texture2D roomImg)
        {
            this.roomImg = roomImg;
        }

        public void SetRoomRec(int x, int y)
        {
            roomRec = new Rectangle(x, y, (int)imgWH, (int)imgWH);
        }
        
        public void SetReveal(bool reveal)
        {
            this.reveal = reveal;
        }
    
        public virtual void Sell(int index, PowerUp itemBought)
        {

        }

        public virtual void Buy(int newCost)
        {

        }

        public virtual void PrepInventory()
        {

        }

        public void AddTrap(Trap trap)
        {
            this.trap = trap;
        }

        public void RoomDirections()
        {
            //Determine possible directions based on room layout
            switch (layout)
            {
                case "0000":
                    directions[0] = false;
                    directions[1] = false;
                    directions[2] = false;
                    directions[3] = false;
                    break;
                case "0001":
                    directions[0] = false;
                    directions[1] = false;
                    directions[2] = false;
                    directions[3] = true;
                    break;
                case "0010":
                    directions[0] = false;
                    directions[1] = false;
                    directions[2] = true;
                    directions[3] = false;
                    break;
                case "0011":
                    directions[0] = false;
                    directions[1] = false;
                    directions[2] = true;
                    directions[3] = true;
                    break;
                case "0100":
                    directions[0] = false;
                    directions[1] = true;
                    directions[2] = false;
                    directions[3] = false;
                    break;
                case "0101":
                    directions[0] = false;
                    directions[1] = true;
                    directions[2] = false;
                    directions[3] = true;
                    break;
                case "0110":
                    directions[0] = false;
                    directions[1] = true;
                    directions[2] = true;
                    directions[3] = false;
                    break;
                case "0111":
                    directions[0] = false;
                    directions[1] = true;
                    directions[2] = true;
                    directions[3] = true;
                    break;
                case "1000":
                    directions[0] = true;
                    directions[1] = false;
                    directions[2] = false;
                    directions[3] = false;
                    break;
                case "1001":
                    directions[0] = true;
                    directions[1] = false;
                    directions[2] = false;
                    directions[3] = true;
                    break;
                case "1010":
                    directions[0] = true;
                    directions[1] = false;
                    directions[2] = true;
                    directions[3] = false;
                    break;
                case "1011":
                    directions[0] = true;
                    directions[1] = false;
                    directions[2] = true;
                    directions[3] = true;
                    break;
                case "1100":
                    directions[0] = true;
                    directions[1] = true;
                    directions[2] = false;
                    directions[3] = false;
                    break;
                case "1101":
                    directions[0] = true;
                    directions[1] = true;
                    directions[2] = false;
                    directions[3] = true;
                    break;
                case "1110":
                    directions[0] = true;
                    directions[1] = true;
                    directions[2] = true;
                    directions[3] = false;
                    break;
                case "1111":
                    directions[0] = true;
                    directions[1] = true;
                    directions[2] = true;
                    directions[3] = true;
                    break;
            }
        }
    }
}
