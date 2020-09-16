//Author:           Amy Wang
//File Name:        Dungeon.cs
//Project Name:     A2_OOP
//Creation Date:    October 11, 2018
//Modified Date:    October 22, 2018
/*Description:      Control all parts of the dungeon which consists of the main game elements, 
                    including the player and rooms*/

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
    class Dungeon
    {
        //Store data from file
        private string[] data;
        private string[] dungeonInfo;
    
        //Store main elements of dungeon
        private List<Room> rooms = new List<Room>();
        private Player player;
        private Trap currentTrap;
        private List<Trap> tempTrap = new List<Trap>();
        
        //Store player info
        private int cashMin;
        private int cashMax;
        private int healthMin;
        private int healthMax;

        //Track player position
        private bool isInShop = false;
        private bool isInPassiveTrap = false;
        private bool isInActiveTrap = false;

        //Store total rows, columns, and rooms
        private int numRows;
        private int numCols;
        private int numRooms;

        //Store start and end positions
        private int startRow;
        private int startCol;
        private int endRow;
        private int endCol;
        private int endGridValue;
        
        //Determine when time is paused
        private bool isPaused = false;

        //Store new cost when selling to shop
        private double newCost;

        //Generate random numbers
        private Random rng = new Random();

        //Store room images
        private Texture2D[] roomImg;

        //Store coordinates of end image
        private int x;
        private int y;

        //Store time left in game
        private int timeLeft;

        //Determine when player loses or wins
        private bool isWinningGame = false;
        private bool isLosingGame = false;

        public Dungeon(Texture2D[] roomImg, string[] dungeonInfo)
        {
            //Store image values
            int adjust = 12;
            double roomSize = 75.5;

            //Store elements of dungeon
            this.roomImg = roomImg;
            this.dungeonInfo = dungeonInfo;

            //Determine time left
            this.timeLeft = Convert.ToInt32(dungeonInfo[2]) * 1000;

            //Store game data
            for (int i = 3; i < 8; ++i)
            {
                //Split data from file
                data = dungeonInfo[i].Split(',');

                //Determine data to store
                switch (i)
                {
                    case 3:
                        numRows = Convert.ToInt32(data[0]);
                        numCols = Convert.ToInt32(data[1]);
                        break;
                    case 4:
                        startRow = Convert.ToInt32(data[0]);
                        startCol = Convert.ToInt32(data[1]);
                        break;
                    case 5:
                        endRow = Convert.ToInt32(data[0]);
                        endCol = Convert.ToInt32(data[1]);
                        break;
                    case 6:
                        cashMin = Convert.ToInt32(data[0]);
                        cashMax = Convert.ToInt32(data[1]);
                        break;
                    case 7:
                        healthMin = Convert.ToInt32(data[0]);
                        healthMax = Convert.ToInt32(data[1]);
                        break;
                }
            }

            //Determine image coordinates for end position
            x = (int)roomSize * endCol + adjust;
            y = (int)roomSize * endRow + adjust--;

            //Determine end position grid value
            endGridValue = Convert.ToInt32(endRow) * numRows + Convert.ToInt32(endCol);

            //Create player
            player = new Player(startRow, startCol, cashMin, cashMax, healthMin, healthMax, numRows);

            //Set total number of rooms
            numRooms = numRows * numCols;
        }

        /// <summary>
        /// Access isWinningGame to determine if player is winning
        /// </summary>
        /// <returns>isWinningGame</returns>
        public bool GetIsWinningGame()
        {
            return isWinningGame;
        }

        /// <summary>
        /// Access isLosingGame to determine if player is losing
        /// </summary>
        /// <returns>isLosingGame</returns>
        public bool GetIsLosingGame()
        {
            return isLosingGame;
        }

        /// <summary>
        /// Access timeLeft to determine time left in game
        /// </summary>
        /// <returns>timeLeft</returns>
        public int GetTimeLeft()
        {
            return timeLeft;
        }

        /// <summary>
        /// Access active trap time left
        /// </summary>
        /// <returns>Active trap time left</returns>
        public int GetActiveTimeLeft()
        {
            return rooms[player.GetPlayerGridValue()].GetTrap().GetTimeLeft();
        }

        /// <summary>
        /// Access passive trap time left
        /// </summary>
        /// <returns>Passive trap time left</returns>
        public int GetPassiveTimeLeft()
        {
            return rooms[player.GetPlayerGridValue()].GetTrap().GetTimeLeft();
        }
        
        /// <summary>
        /// Access player grid value
        /// </summary>
        /// <returns>Player grid value</returns>
        public int GetPlayerGridValue()
        {
            return player.GetPlayerGridValue();
        }
        
        /// <summary>
        /// Access number of rooms in dungeon
        /// </summary>
        /// <returns>numRooms</returns>
        public int GetNumRooms()
        {
            return numRooms;
        }

        /// <summary>
        /// Store room images
        /// </summary>
        /// <returns>img</returns>
        public Texture2D[] GetRoomImage()
        {
            Texture2D[] img = new Texture2D[numRooms];

            for (int i = 0; i < numRooms; ++i)
            {
                img[i] = rooms[i].GetRoomImage();
            }

            return img;
        }

        /// <summary>
        /// Store room rectangles
        /// </summary>
        /// <returns>rec</returns>
        public Rectangle[] GetRoomRec()
        {
            Rectangle[] rec = new Rectangle[numRooms];

            for (int i = 0; i < numRooms; ++i)
            {
                rec[i] = rooms[i].GetRoomRec();
            }

            return rec;
        }

        /// <summary>
        /// Access total number of rows
        /// </summary>
        /// <returns>numRows</returns>
        public int GetNumRows()
        {
            return numRows;
        }

        /// <summary>
        /// Access player cash
        /// </summary>
        /// <returns>Player cash</returns>
        public int GetPlayerCash()
        {
            return player.GetCash();
        }

        /// <summary>
        /// Access player health
        /// </summary>
        /// <returns>Player health</returns>
        public int GetPlayerHealth()
        {
            return player.GetHealth();
        }

        /// <summary>
        /// Access player inventory
        /// </summary>
        /// <returns>Player inventory</returns>
        public List<PowerUp> GetPlayerInventory()
        {
            return player.GetPlayerInventory();
        }
        
        /// <summary>
        /// Access player image X value
        /// </summary>
        /// <returns>Player image X values</returns>
        public int GetPlayerX()
        {
            return player.GetPlayerX();
        }

        /// <summary>
        /// Access player image Y value
        /// </summary>
        /// <returns>Player image Y values</returns>
        public int GetPlayerY()
        {
            return player.GetPlayerY();
        }

        /// <summary>
        /// Access end image X value
        /// </summary>
        /// <returns>End image x value</returns>
        public int GetEndX()
        {
            return x;
        }

        /// <summary>
        /// Access end image Y value
        /// </summary>
        /// <returns>End image y value</returns>
        public int GetEndY()
        {
            return y;
        }

        /// <summary>
        /// Access shop image X values
        /// </summary>
        /// <returns>shopX</returns>
        public List<int> GetShopX()
        {
            List<int> shopX = new List<int>();
            
            for (int i = 0; i < rooms.Count; ++i)
            {
                shopX.Add(rooms[i].GetShopX());
            }

            return shopX;
        }

        /// <summary>
        /// Access shop image Y values
        /// </summary>
        /// <returns>shopY</returns>
        public List<int> GetShopY()
        {
            List<int> shopY = new List<int>();

            for (int i = 0; i < rooms.Count; ++i)
            {
                shopY.Add(rooms[i].GetShopY());
            }

            return shopY;
        }

        /// <summary>
        /// Access passive trap X values
        /// </summary>
        /// <returns>passiveX</returns>
        public List<int> GetPassiveX()
        {
            List<int> passiveX = new List<int>();
            
            for (int i = 0; i < rooms.Count; ++i)
            {
                if (rooms[i].GetTrap() != null)
                {
                    passiveX.Add(rooms[i].GetTrap().GetPassiveX());
                }
            }

            return passiveX;
        }

        /// <summary>
        /// Access passive trap Y values
        /// </summary>
        /// <returns>passiveY</returns>
        public List<int> GetPassiveY()
        {
            List<int> passiveY = new List<int>();

            for (int i = 0; i < rooms.Count; ++i)
            {
                if (rooms[i].GetTrap() != null)
                {
                    passiveY.Add(rooms[i].GetTrap().GetPassiveY());
                }
            }

            return passiveY;
        }

        /// <summary>
        /// Access active trap X values
        /// </summary>
        /// <returns>activeX</returns>
        public List<int> GetActiveX()
        {
            List<int> activeX = new List<int>();

            for (int i = 0; i < rooms.Count; ++i)
            {
                if (rooms[i].GetTrap() != null)
                {
                    activeX.Add(rooms[i].GetTrap().GetActiveX());
                }
            }

            return activeX;
        }

        /// <summary>
        /// Access active trap Y values
        /// </summary>
        /// <returns>activeY</returns>
        public List<int> GetActiveY()
        {
            List<int> activeY = new List<int>();

            for (int i = 0; i < rooms.Count; ++i)
            {
                if (rooms[i].GetTrap() != null)
                {
                    activeY.Add(rooms[i].GetTrap().GetActiveY());
                }
            }

            return activeY;
        }

        /// <summary>
        /// Access current trap player is in
        /// </summary>
        /// <returns>currentTrap</returns>
        public Trap GetCurrentTrap()
        {
            return currentTrap;
        }
        
        /// <summary>
        /// Access all rooms of dungeon
        /// </summary>
        /// <returns>rooms</returns>
        public List<Room> GetRooms()
        {
            return rooms;
        }

        /// <summary>
        /// Access if player is in a shop
        /// </summary>
        /// <returns>isInShop</returns>
        public bool GetInShop()
        {
            return isInShop;
        }
        
        /// <summary>
        /// Access if time is paused
        /// </summary>
        /// <returns>isPaused</returns>
        public bool GetPause()
        {
            return isPaused;
        }
  
        /// <summary>
        /// Access if player is in a passive trap
        /// </summary>
        /// <returns>isInPassiveTrap</returns>
        public bool GetInPassiveTrap()
        {
            return isInPassiveTrap;
        }
        
        /// <summary>
        /// Access if player is in an active trap
        /// </summary>
        /// <returns>isInActiveTrap</returns>
        public bool GetInActiveTrap()
        {
            return isInActiveTrap;
        }
    
        /// <summary>
        /// Calculate new cost when player sells item to shop
        /// </summary>
        /// <param name="shop"></param>
        /// <returns>newCost</returns>
        public int GetNewCost(Room shop)
        {
            int num = player.GetItemSelect();
            int oldCost = Convert.ToInt32(player.GetPlayerInventory()[num].CalcCost());
            newCost = oldCost * (shop.GetPercentResell() / 100.0);

            return (int)newCost;
        }
        
        /// <summary>
        /// Access maximum player inventory size
        /// </summary>
        /// <returns>player.GetMaxSize()</returns>
        public int GetPlayerMaxSize()
        {
            return player.GetMaxSize();
        }

        /// <summary>
        /// Access power up used
        /// </summary>
        /// <returns>Power up used</returns>
        public PowerUp GetPowerUpUsing()
        {
            return player.GetPowerUpUsing();
        }
        
        /// <summary>
        /// Access if player is using item
        /// </summary>
        /// <returns>If player is using item</returns>
        public bool GetIsUsing()
        {
            return player.GetIsUsing();
        }

        /// <summary>
        /// Access player health
        /// </summary>
        /// <returns>Player health</returns>
        public int GetHealth()
        {
            return player.GetHealth();
        }

        /// <summary>
        /// Access if player is in trap
        /// </summary>
        /// <param name="isInActiveTrap"></param>
        public void SetInActiveTrap(bool isInActiveTrap)
        {
            this.isInActiveTrap = isInActiveTrap;
        }

        /// <summary>
        /// Access if player is in trap
        /// </summary>
        /// <param name="isInPassiveTrap"></param>
        public void SetInPassiveTrap(bool isInPassiveTrap)
        {
            this.isInPassiveTrap = isInPassiveTrap;
        }

        /// <summary>
        /// Access if player is using item
        /// </summary>
        /// <param name="isUsing"></param>
        public void SetIsUsing(bool isUsing)
        {
            player.SetIsUsing(isUsing);
        }

        public void SetPlayerItemSelect(int num)
        {
            player.SetItemSelect(num);
        }
        
        public void SetInShop(bool isInShop)
        {
            this.isInShop = isInShop;
        }

        public void SetPause(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        /// <summary>
        /// Add rooms to rooms list
        /// </summary>
        /// <param name="room"></param>
        public void AddRoom(Room room)
        {
            //Prepare inventory if shop
            room.PrepInventory();

            //Add to rooms list
            rooms.Add(room);
        }
        
        /// <summary>
        /// Add traps
        /// </summary>
        /// <param name="trap"></param>
        public void AddInTrap(Trap trap)
        {
            //Use list to hold traps
            tempTrap.Add(trap);
        }

        /// <summary>
        /// Store added traps in rooms
        /// </summary>
        public void StoreTrap()
        {
            //Allow rooms to own traps
            for (int i = 0; i < tempTrap.Count; ++i)
            {
                for (int j = 0; j < numRooms; ++j)
                {
                    //Determine which rooms own a trap
                    if (tempTrap[i].GetGridValue(numRows) == rooms[j].GetGridValue(numRows))
                    {
                        rooms[j].AddTrap(tempTrap[i]);
                    }
                }
            }
        }
    
        /// <summary>
        /// Set initial data
        /// </summary>
        public void InitialData()
        {
            //Reveal first room at player's starting position
            rooms[GetPlayerGridValue()].SetReveal(true);

            //Set grid values of rooms
            for (int i = 0; i < rooms.Count; ++i)
            {
                rooms[i].SetGridValue(GetNumRows());
            }

        }
       
        /// <summary>
        /// Set up room image locations
        /// </summary>
        public void SetRoomImages()
        {
            //Move to next room to continue setting images
            int nextRoom = 0;

            //Store coordinates of room rectangles
            int x = 0;
            int y = 0;
        
            //Determine image to display based on room layout
            for (int i = 0; i < rooms.Count; ++i)
            {
                switch (rooms[i].GetLayout())
                {
                    case "0000":
                        rooms[nextRoom].SetRoomImage(roomImg[0]);
                        break;
                    case "0001":
                        rooms[nextRoom].SetRoomImage(roomImg[1]);
                        break;
                    case "0010":
                        rooms[nextRoom].SetRoomImage(roomImg[2]);
                        break;
                    case "0011":
                        rooms[nextRoom].SetRoomImage(roomImg[3]);
                        break;
                    case "0100":
                        rooms[nextRoom].SetRoomImage(roomImg[4]);
                        break;
                    case "0101":
                        rooms[nextRoom].SetRoomImage(roomImg[5]);
                        break;
                    case "0110":
                        rooms[nextRoom].SetRoomImage(roomImg[6]);
                        break;
                    case "0111":
                        rooms[nextRoom].SetRoomImage(roomImg[7]);
                        break;
                    case "1000":
                        rooms[nextRoom].SetRoomImage(roomImg[8]);
                        break;
                    case "1001":
                        rooms[nextRoom].SetRoomImage(roomImg[9]);
                        break;
                    case "1010":
                        rooms[nextRoom].SetRoomImage(roomImg[10]);
                        break;
                    case "1011":
                        rooms[nextRoom].SetRoomImage(roomImg[11]);
                        break;
                    case "1100":
                        rooms[nextRoom].SetRoomImage(roomImg[12]);
                        break;
                    case "1101":
                        rooms[nextRoom].SetRoomImage(roomImg[13]);
                        break;
                    case "1110":
                        rooms[nextRoom].SetRoomImage(roomImg[14]);
                        break;
                    case "1111":
                        rooms[nextRoom].SetRoomImage(roomImg[15]);
                        break;
                }
            
                //Set room rectangle
                rooms[nextRoom].SetRoomRec(x, y);

                //Store image size
                int imgWH = rooms[nextRoom].GetImageWH();

                //Move on to next room
                nextRoom++;

                //Add image size to X coordinate
                x += imgWH;

                //After displaying image at end of row, move to next row
                if (x == imgWH * numRows)
                {
                    y += imgWH;
                    x = 0;
                }
            }
        }
        
        /// <summary>
        /// Check directions available to move in
        /// </summary>
        /// <returns></returns>
        public bool[] CheckDirecAvail()
        {
            //Store player grid value
            int grid = player.GetPlayerGridValue();

            //Determine directions player can move in
            rooms[grid].RoomDirections();

            //Return set of directions of room
            return rooms[grid].GetDirections();
        }
    
        /// <summary>
        /// Change grid value of player
        /// </summary>
        /// <param name="gridChange"></param>
        public void MovePlayer(int gridChange)
        {
            //Change grid value of player
            player.Move(gridChange);

            //Reveal room player moves into
            rooms[player.GetPlayerGridValue()].SetReveal(true);
        }

        /// <summary>
        /// Track player position
        /// </summary>
        public void TrackPlayer()
        {
            //Store shop grid values
            List<int> shopValues = new List<int>();

            //Determine player location
            for (int i = 0; i < rooms.Count; ++i)
            {
                //Store shop grid values
                if (rooms[i].GetShopGridValue() >= 0)
                {
                    shopValues.Add(rooms[i].GetShopGridValue());
                }

                //Determine if player is in active or passive trap
                if (rooms[i].GetTrap() != null)
                {
                    if (rooms[i].GetTrap().GetGridValue(numRows) == player.GetPlayerGridValue())
                    {
                        if (rooms[i].GetTrap().GetName() == "PassiveTrap")
                        {
                            currentTrap = rooms[i].GetTrap();
                            isInPassiveTrap = true;
                        }
                        else if (rooms[i].GetTrap().GetName() == "ActiveTrap")
                        {
                            currentTrap = rooms[i].GetTrap();
                            isInActiveTrap = true;
                        }
                    }
                }
            }

            //Determine if player is in shop and pause time
            for (int i = 0; i < shopValues.Count; ++i)
            {
                if (shopValues[i] == player.GetPlayerGridValue())
                {
                    isInShop = true;
                    isPaused = true;
                }
            }
        }

        /// <summary>
        /// Allow player to buy item
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="shop"></param>
        public void PlayerBuy(List<PowerUp> inventory, Room shop)
        {
            //Store item number selected
            int addIndex = player.GetItemSelect();

            //Add to player inventory
            player.Buy(inventory[addIndex]);

            //Remove from shop inventory
            shop.Sell(addIndex, inventory[addIndex]);
        }

        /// <summary>
        /// Allow player to sell item
        /// </summary>
        /// <param name="shop"></param>
        public void PlayerSell(Room shop)
        {
            //Remove from player inventory
            player.Sell((int)newCost);

            //Increase shop cash
            shop.Buy((int)newCost);
        }

        /// <summary>
        /// Allow player to use item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bonus"></param>
        public void PlayerUse(string name, int bonus)
        {
            player.Use(name, bonus);
        }
    
        /// <summary>
        /// Set new health value when in trap
        /// </summary>
        /// <param name="newHealth"></param>
        public void InTrap(int newHealth)
        {
            //Set new health value when taking trap damage
            if (newHealth != 0)
            {
                player.SetHealth(newHealth);
            }
        }

        /// <summary>
        /// Determine when game is over
        /// </summary>
        /// <param name="time"></param>
        public void DetermineGameEnd(int time)
        {
            //Determine end of game
            if (player.GetHealth() < 0 || time <= 0 && player.GetPlayerGridValue() != endGridValue)
            { 
                //When player has no health or when time is finished, player loses
                isLosingGame = true;
            }
            else
            {
                if (player.GetPlayerGridValue() == endGridValue)
                {
                    //When player reaches end position, player wins
                    isWinningGame = true;
                }
            }
        }
    }
}
