//Author:           Amy Wang
//File Name:        DungeonCrawler.cs
//Project Name:     A2_OOP
//Creation Date:    October 8, 2018
//Modified Date:    October 22, 2018
/*Description:      Run the game, Dungeon Crawler, where the user moves their way through a dungeon as they
                    try to reach the exit. During the game, the user may encounter traps and the user is 
                    able to buy, sell, and use powerups.*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace A2_OOP
{
    public class DungeonCrawler : Game
    {
        //Store keyboard state
        KeyboardState kb;
        KeyboardState prevKb;

        //Store mouse state
        MouseState mouse;
        MouseState prevMouse;

        //Store screen sizes
        int screenWidth;
        int screenHeight;

        //Read dungeon file
        string filePath = "";
        StreamReader inFile;

        //Store data when reading in file
        string line = "";
        string[] data;
        string[] dungeonInfo = new string[8];

        //Create dungeon object
        Dungeon dungeon;
        
        //Store fonts
        SpriteFont gameFont;
        SpriteFont shopFont;
        SpriteFont btnFont;
        SpriteFont invenFont;

        //Store room images
        Texture2D[] roomImg = new Texture2D[16];
        Texture2D roomHiddenImg;
        Texture2D roomRevealImg;

        //Store player image
        Texture2D playerImg;
        Rectangle playerRec;
        
        //Store end position image
        Texture2D endImg;
        Rectangle endRec;

        //Store images used for shop
        Texture2D shopImg;
        Texture2D whiteShopBg;
        Rectangle whiteShopRec;
        Rectangle dividerRec;

        //Store button rectangles
        Rectangle buyBtnRec;
        Rectangle sellBtnRec;
        Rectangle backBtnRec;
        Rectangle sellItemBtnRec;
        Rectangle useItemBtnRec;

        //Store trap images
        Texture2D passiveImg;
        Texture2D activeImg;

        //Store shop and trap rectangles
        List<Rectangle> shopRec = new List<Rectangle>();
        List<Rectangle> passiveTrapRec = new List<Rectangle>();
        List<Rectangle> activeTrapRec = new List<Rectangle>();

        //Store image size
        int imgSize = 53;

        //Store time left in game
        int timeLeft;

        //Store trap timers
        int passiveTime;
        int activeTime;

        //Store trap time left
        int activeTimeLeft;
        int passiveTimeLeft;

        //Store shop player is in
        Room shop;

        //Store messages
        string buySellNum = "";
        string useNum = "";
        string sellMessage = "";
        string buyError = "";

        //Store states for player while in shop
        bool isButtonPressed = false;
        bool isBuying = false;
        bool isSelling = false;
        bool isUsing = false;
        bool isShopDisplayed = false;
        
        //Determine when active trap is activated
        bool isActiveTrapActivated = true;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       
        public DungeonCrawler()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Show mouse on screen
            IsMouseVisible = true;

            //Set width and height of game window
            this.graphics.PreferredBackBufferWidth = 1400;
            this.graphics.PreferredBackBufferHeight = 750;
            graphics.ApplyChanges();
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Continue adding to room images array
            int nextImg = 0;
            
            //Load room images
            for (int i = 0; i <= 1; ++i)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    for (int k = 0; k <= 1; ++k)
                    {
                        for (int l = 0; l <= 1; ++l)
                        {
                            roomImg[0 + nextImg] = Content.Load<Texture2D>
                            ("Room_" + i + j + k + l);
                            nextImg++;
                        }
                    }
                }
            }

            //Load hidden and revealing room images
            roomRevealImg = Content.Load<Texture2D>("Room_Reveal");
            roomHiddenImg = Content.Load<Texture2D>("Room_Hidden");

            //Read data from file
            ReadFile();

            //Determine initial data
            dungeon.InitialData();

            //Store shop
            shop = dungeon.GetRooms()[dungeon.GetPlayerGridValue()];

            //Set up available room directions and images
            dungeon.SetRoomImages();
            
            //Store time left in game
            timeLeft = dungeon.GetTimeLeft();

            //Load shop image
            shopImg = Content.Load<Texture2D>("SHOP");

            //Load trap images
            passiveImg = Content.Load<Texture2D>("TrapPassive");
            activeImg = Content.Load<Texture2D>("TrapActive");

            //Load end position image
            endImg = Content.Load<Texture2D>("EndPosition");
            endRec = new Rectangle(dungeon.GetEndX(), dungeon.GetEndY(), imgSize, imgSize);

            //Load player image
            playerImg = Content.Load<Texture2D>("Player");
            playerRec = new Rectangle(dungeon.GetPlayerX(), dungeon.GetPlayerY(), imgSize, imgSize);

            //Load shop background
            whiteShopBg = Content.Load<Texture2D>("WhiteSquare");
            whiteShopRec = new Rectangle(750, 75, whiteShopBg.Width, (int)(whiteShopBg.Height * 0.45));

            //Load divider rectangle used in shop
            dividerRec = new Rectangle(750, 370, whiteShopBg.Width, (int)(whiteShopBg.Height * 0.03));

            //Load button rectangles
            buyBtnRec = new Rectangle(780, 150, (int)(whiteShopBg.Width * 0.2), (int)(whiteShopBg.Height * 0.2));
            sellBtnRec = new Rectangle(1100, 150, (int)(whiteShopBg.Width * 0.2), (int)(whiteShopBg.Height * 0.2));
            backBtnRec = new Rectangle(1180, 310, (int)(whiteShopBg.Width * 0.1), (int)(whiteShopBg.Height * 0.05));
            sellItemBtnRec = new Rectangle(780, 310, (int)(whiteShopBg.Width * 0.1), (int)(whiteShopBg.Height * 0.05));
            useItemBtnRec = new Rectangle(780, 670, (int)(whiteShopBg.Width * 0.1), (int)(whiteShopBg.Height * 0.05));

            //Determine shop rectangles
            for (int i = 0; i < dungeon.GetShopX().Count; ++i)
            {
                if (dungeon.GetShopX()[i] >= 0)
                {
                    shopRec.Add(new Rectangle(dungeon.GetShopX()[i], dungeon.GetShopY()[i], imgSize, imgSize));
                }
            }

            //Determine passive trap rectangles
            for (int i = 0; i < dungeon.GetPassiveX().Count; ++i)
            {
                if (dungeon.GetPassiveX()[i] >= 0)
                {
                    passiveTrapRec.Add(new Rectangle(dungeon.GetPassiveX()[i], dungeon.GetPassiveY()[i], imgSize, imgSize));
                }
            }

            //Determine active trap rectangles
            for (int i = 0; i < dungeon.GetActiveX().Count; ++i)
            {
                if (dungeon.GetActiveX()[i] >= 0)
                {
                    activeTrapRec.Add(new Rectangle(dungeon.GetActiveX()[i], dungeon.GetActiveY()[i], imgSize, imgSize));
                }
            }

            //Load fonts
            gameFont = Content.Load<SpriteFont>("GameFont");
            shopFont = Content.Load<SpriteFont>("ShopFont");
            btnFont = Content.Load<SpriteFont>("BtnFont");
            invenFont = Content.Load<SpriteFont>("InventoryFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //Store previous mouse state and retrieve new mouse state
            prevMouse = mouse;
            mouse = Mouse.GetState();

            //Store previous keyboard state and retrieve new state
            prevKb = kb;
            kb = Keyboard.GetState();

            //Check if player can move
            bool[] roomDirections = dungeon.CheckDirecAvail();
            MovePlayerImage(roomDirections, dungeon.GetNumRows());

            //Determine shop player is in
            for (int i = 0; i < dungeon.GetRooms().Count; ++i)
            {
                if (dungeon.GetRooms()[i].GetShopGridValue() == dungeon.GetPlayerGridValue())
                {
                    shop = dungeon.GetRooms()[i];
                }
            }

            //Track player position
            dungeon.TrackPlayer();
            
            //Track time left in game
            if (timeLeft > 0 && !dungeon.GetPause())
            {
                timeLeft -= gameTime.ElapsedGameTime.Milliseconds;
            }

            //Determine if player is in active trap
            ActiveTrap(gameTime);

            //Determine if player is in passive trap
            PassiveTrap(gameTime);
            
            //Retrieve user input
            Input();

            //Determine results of player using power ups
            if (dungeon.GetIsUsing())
            {
                UsingPowerUps();
            }

            //Determine when game ends
            dungeon.DetermineGameEnd(timeLeft);

            base.Update(gameTime);
        }
    
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Set background colour
            GraphicsDevice.Clear(Color.Violet * 0.6f);

            //Begin drawing images
            spriteBatch.Begin();
        
            //If player has not yet lost or won the game, display game
            if (!dungeon.GetIsLosingGame() && !dungeon.GetIsWinningGame())
            {
                //Display game
                DisplayGame();

                //Display player inventory
                DisplayPlayerInventory();

                //Display shop menu
                DisplayShopMenu();
                
                //Display active and passive traps
                if (dungeon.GetInActiveTrap())
                {
                    DisplayActiveTrap();
                }
                else if (dungeon.GetInPassiveTrap())
                {
                    DisplayPassiveTrap();
                }
            }
            else
            {
                //Display winning or losing screen
                if (dungeon.GetIsWinningGame())
                {
                    DisplayWin();
                }
                else if (dungeon.GetIsLosingGame())
                {
                    DisplayLose();
                }
            }
        
            //Finish displaying images
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        /// <summary>
        ///Pre: None
        ///Post: Dungeon data is read from dungeon file 
        ///Description: Using file IO, read dungeon data from dungeon file
        /// </summary>
        private void ReadFile()
        {
            //Determine and retrieve file path 
            filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);
            filePath = filePath.Substring(6);
            filePath = filePath + "/dungeon1.txt";

            //Read data from dungeon file
            inFile = File.OpenText(filePath);

            //Retrieve dungeon information
            for (int i = 0; i < 8; ++i)
            {
                line = inFile.ReadLine();
                dungeonInfo[i] = line;
            }

            //Pass all file data into dungeon
            dungeon = new Dungeon(roomImg, dungeonInfo);

            //Read line
            line = inFile.ReadLine();

            //Read file until "TrapEnd"
            while ((line = inFile.ReadLine()) != "TrapEnd")
            {
                //Split line
                data = line.Split(',');

                //Store traps
                if (data[0] == "PassiveTrap")
                {
                    dungeon.AddInTrap(new PassiveTrap(data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
                }
                else
                {
                    dungeon.AddInTrap(new ActiveTrap(data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
                }
            }

            //Read until end of file
            while (!inFile.EndOfStream)
            {
                //Split line
                line = inFile.ReadLine();
                data = line.Split(',');

                //Store rooms
                if (data[0] == "PassageRoom")
                {
                    dungeon.AddRoom(new Room(data[0], data[1], data[2], data[3]));
                }
                else
                {
                    dungeon.AddRoom(new Shop(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7],
                    data[8], data[9]));
                }
            }

            //Store traps
            dungeon.StoreTrap();

            //Close file
            inFile.Close();
        }

        /// <summary>
        /// Display main elements of game
        /// </summary>
        private void DisplayGame()
        {
            //Store all room images and rectangles
            Texture2D[] allImg;
            Rectangle[] allRec;
            
            //Retrieve room images and rectangles
            allImg = dungeon.GetRoomImage();
            allRec = dungeon.GetRoomRec();

            //Display revealed and hidden rooms
            for (int i = 0; i < dungeon.GetNumRooms(); ++i)
            {
                spriteBatch.Draw(allImg[i], allRec[i], Color.White);

                if (dungeon.GetRooms()[i].GetReveal())
                {
                    spriteBatch.Draw(roomRevealImg, allRec[i], Color.White * 0.5f);
                }
                else
                {
                    spriteBatch.Draw(roomHiddenImg, allRec[i], Color.White);
                }
            }

            //Display shops
            for (int i = 0; i < shopRec.Count; ++i)
            {
                spriteBatch.Draw(shopImg, shopRec[i], Color.White);
            }

            //Display player and end image
            spriteBatch.Draw(playerImg, playerRec, Color.White);
            spriteBatch.Draw(endImg, endRec, Color.White);

            //Display traps
            DisplayTraps();

            //Display player stats
            spriteBatch.DrawString(gameFont, "Time Left: " + timeLeft / 1000 + " s", new Vector2(780, 20), Color.White);
            spriteBatch.DrawString(gameFont, "Cash: $" + dungeon.GetPlayerCash(), new Vector2(1025, 20), Color.White);
            spriteBatch.DrawString(gameFont, "Health: " + dungeon.GetPlayerHealth(), new Vector2(1230, 20), Color.White);
        }
   
        /// <summary>
        /// Display passive and active traps
        /// </summary>
        private void DisplayTraps()
        {
            //Display passive traps
            for (int i = 0; i < passiveTrapRec.Count; ++i)
            {
                if (passiveTrapRec[i].X == playerRec.X && passiveTrapRec[i].Y == playerRec.Y)
                {
                    spriteBatch.Draw(passiveImg, passiveTrapRec[i], Color.White);
                }
            }

            //Display active traps
            for (int i = 0; i < activeTrapRec.Count; ++i)
            {
                if (activeTrapRec[i].X == playerRec.X && activeTrapRec[i].Y == playerRec.Y)
                {
                    spriteBatch.Draw(activeImg, activeTrapRec[i], Color.White);
                }
            }
        }

        /// <summary>
        /// Display player inventory
        /// </summary>
        private void DisplayPlayerInventory()
        {
            //Shift items downwards on screen
            int moveDown = 0;

            //Display inventory title
            spriteBatch.DrawString(gameFont, "Player Inventory", new Vector2(780, 420), Color.White);

            //Display player inventory
            for (int i = 0; i < dungeon.GetPlayerInventory().Count; ++i)
            {
                spriteBatch.DrawString(invenFont, "" + (i + 1) + ". " + dungeon.GetPlayerInventory()[i].GetName() + 
                " - Cost: $" + dungeon.GetPlayerInventory()[i].CalcCost() + " - DPS: " + dungeon.GetPlayerInventory()[i].GetDPS() + 
                " - Durability: " + dungeon.GetPlayerInventory()[i].GetDurability(), new Vector2(780, 465 + moveDown), Color.White);

                moveDown += 30;
            }
            
            //Display use button
            ButtonClick(useItemBtnRec.X, useItemBtnRec.Right, useItemBtnRec.Y, useItemBtnRec.Bottom, useItemBtnRec);
            spriteBatch.Draw(whiteShopBg, useItemBtnRec, Color.Honeydew);
            spriteBatch.DrawString(gameFont, "Use", new Vector2(825, 673), Color.DarkViolet);

            //Display text for user to select item
            if (isUsing)
            {
                spriteBatch.DrawString(gameFont, "Enter Item # to Use: " + useNum, new Vector2(925, 673), Color.White);
                spriteBatch.DrawString(invenFont, "(Press Enter when done)", new Vector2(925, 703), Color.White);
            }
        }

        /// <summary>
        /// Display shop menu
        /// </summary>
        private void DisplayShopMenu()
        {
            //Display images according to if user is in shop
            if (dungeon.GetInShop() || isShopDisplayed)
            {
                //Display shop
                DisplayShop();
                isShopDisplayed = false;
            }
            else
            {
                //When not in shop, user cannot buy or sell
                isButtonPressed = false;
                isBuying = false;
                isSelling = false;

                //Display shop sign
                spriteBatch.Draw(whiteShopBg, whiteShopRec, Color.Honeydew);
                spriteBatch.DrawString(shopFont, "SHOP", new Vector2(950, 165), Color.Violet * 0.6f);
            }

            //Display screens for buying and selling
            if (isBuying)
            {
                DisplayBuy();
            }
            else if (isSelling)
            {
                DisplaySell();
            }
        }

        /// <summary>
        /// Display shop
        /// </summary>
        private void DisplayShop()
        {
            //Allow user to click buy and sell buttons
            ButtonClick(buyBtnRec.X, buyBtnRec.Right, buyBtnRec.Y, buyBtnRec.Bottom, buyBtnRec);
            ButtonClick(sellBtnRec.X, sellBtnRec.Right, sellBtnRec.Y, sellBtnRec.Bottom, sellBtnRec);

            //Before pressing a button:
            if (!isButtonPressed)
            {
                //Display instructions
                spriteBatch.DrawString(gameFont, "Welcome to the Shop! Click a button to buy or sell", new Vector2(780, 80), 
                Color.White);

                //Display buttons
                spriteBatch.Draw(whiteShopBg, buyBtnRec, Color.Honeydew);
                spriteBatch.Draw(whiteShopBg, sellBtnRec, Color.Honeydew);
                spriteBatch.DrawString(btnFont, "Buy", new Vector2(865, 185), Color.DarkViolet);
                spriteBatch.DrawString(btnFont, "Sell", new Vector2(1190, 185), Color.DarkViolet);
            }

            //Display divider between shop and player inventory
            spriteBatch.Draw(whiteShopBg, dividerRec, Color.Honeydew);
        }
    
        /// <summary>
        /// Display buying screen to allow user to buy items
        /// </summary>
        private void DisplayBuy()
        {
            //Display title
            spriteBatch.DrawString(gameFont, "Shop Inventory - Cash: $" + shop.GetShopCash(), new Vector2(780, 80), Color.White);

            //Shift items downwards on screen
            int moveDown = 0;
        
            //Display shop inventory
            for (int i = 0; i < shop.GetInventory().Count; ++i)
            {
                spriteBatch.DrawString(invenFont, "" + (i + 1) + ". " + shop.GetInventory()[i].GetName() + " - Cost: $" +
                shop.GetInventory()[i].CalcCost() + " - DPS: " + shop.GetInventory()[i].GetDPS() + " - Durability: " + 
                shop.GetInventory()[i].GetDurability(), new Vector2(780, 120 + moveDown), Color.White);

                moveDown += 30;
            }
            
            //Display text for user to buy item
            spriteBatch.DrawString(gameFont, "Enter Item #: " + buySellNum, new Vector2(1180, 80), Color.Yellow);
            spriteBatch.DrawString(invenFont, "(Press Enter when done)", new Vector2(1180, 110), Color.Yellow);

            //Display back button
            ButtonClick(backBtnRec.X, backBtnRec.Right, backBtnRec.Y, backBtnRec.Bottom, backBtnRec);
            spriteBatch.Draw(whiteShopBg, backBtnRec, Color.Honeydew);
            spriteBatch.DrawString(gameFont, "Back", new Vector2(1217, 313), Color.DarkViolet);

            //Display error message
            spriteBatch.DrawString(invenFont, buyError, new Vector2(1180, 140), Color.Yellow);
        }

        /// <summary>
        /// Display selling screen to allow user to sell items
        /// </summary>
        private void DisplaySell()
        {
            //Display text for user to enter item
            spriteBatch.DrawString(gameFont, "Enter Item # from Player Inventory: " + buySellNum, new Vector2(780, 80), 
            Color.White);
            spriteBatch.DrawString(invenFont, "(Press Enter when done)", new Vector2(780, 110), Color.White);
        
            //Display selling message
            spriteBatch.DrawString(gameFont, sellMessage, new Vector2(780, 160), Color.White);

            //Display sell button
            ButtonClick(sellItemBtnRec.X, sellItemBtnRec.Right, sellItemBtnRec.Y, sellItemBtnRec.Bottom, sellItemBtnRec);
            spriteBatch.Draw(whiteShopBg, sellItemBtnRec, Color.Honeydew);
            spriteBatch.DrawString(gameFont, "Sell", new Vector2(825, 313), Color.DarkViolet);

            //Display back button
            ButtonClick(backBtnRec.X, backBtnRec.Right, backBtnRec.Y, backBtnRec.Bottom, backBtnRec);
            spriteBatch.Draw(whiteShopBg, backBtnRec, Color.Honeydew);
            spriteBatch.DrawString(gameFont, "Back", new Vector2(1217, 313), Color.DarkViolet);
        }

        /// <summary>
        /// Display active traps
        /// </summary>
        private void DisplayActiveTrap()
        {
            //Display active trap text
            spriteBatch.DrawString(gameFont, "Active Trap!", new Vector2(100, 100), Color.White);
        }

        /// <summary>
        /// Display passive traps
        /// </summary>
        private void DisplayPassiveTrap()
        {
            //Display passive trap text
            spriteBatch.DrawString(gameFont, "Passive Trap!", new Vector2(100, 100), Color.White);
        }

        /// <summary>
        /// Display winning screen
        /// </summary>
        private void DisplayWin()
        {
            //Display winning message
            spriteBatch.DrawString(gameFont, "You won." + "\n" + "Good for you." + "\n" + "Go do something " +
            "productive with your life.", new Vector2(100, 100), Color.White);
        }
    
        /// <summary>
        /// Display losing screen
        /// </summary>
        private void DisplayLose()
        {
            //Display losing message
            spriteBatch.DrawString(gameFont, "I had to put in all my time, effort, and tears only for you to lose." +
            "\n" + "Thanks for everything.", new Vector2(100, 100), Color.White);
        }

        /// <summary>
        ///Determine which screen to display after a click using the X and Y values of the box to determine the area
        ///of the click
        /// </summary>
        private void ButtonClick(int x1, int x2, int y1, int y2, Rectangle box)
        {
            //If the left mouse button is pressed while in a button area, display corresponding images
            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Determine if area is clicked
                if (x1 <= mouse.X && mouse.X <= x2 && y1 <= mouse.Y && mouse.Y <= y2)
                {
                    //A button has been pressed
                    isButtonPressed = true;
                    
                    //Determine button pressed
                    if (box == buyBtnRec)
                    {
                        //Allow user to buy
                        isBuying = true;
                    }
                    else if (box == sellBtnRec)
                    {
                        //Allow user to sell
                        isSelling = true;
                    }
                    else if (box == backBtnRec)
                    {
                        //Go back
                        BackButton();
                    }
                    else if (box == sellItemBtnRec)
                    {
                        //Allow user to sell, then go back
                        dungeon.PlayerSell(shop);
                        BackButton();
                    }
                    else if (box == useItemBtnRec)
                    {
                        //Allow user to use item
                        useNum = "";
                        isUsing = true;
                        isBuying = false;
                        isSelling = false;
                    }
                }
            }
        }
    
        /// <summary>
        /// Reset values when going back from a screen
        /// </summary>
        private void BackButton()
        {
            sellMessage = "";
            buySellNum = "";
            buyError = "";
            isShopDisplayed = true;
            isBuying = false;
            isSelling = false;
            isButtonPressed = false;
        }

        /// <summary>
        /// Retrieve user input and determine actions based on input
        /// </summary>
        private void Input()
        {
            //Display corresponding number when key is pressed
            InputNums(Keys.D1, 1);
            InputNums(Keys.D2, 2);
            InputNums(Keys.D3, 3);
            InputNums(Keys.D4, 4);
            InputNums(Keys.D5, 5);
            InputNums(Keys.D6, 6);
            InputNums(Keys.D7, 7);
            InputNums(Keys.D8, 8);

            //Determine player action after pressing enter key
            if (!prevKb.IsKeyDown(Keys.Enter) && kb.IsKeyDown(Keys.Enter))
            {
                if (isBuying)
                {
                    Buying();
                }
                else if (isSelling)
                {
                    Selling();   
                }
                else if (isUsing)
                {
                    Using();
                }
            }
        }
   
        /// <summary>
        /// Retrieve number selected when buying or selling items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="num"></param>
        private void InputNums(Keys key, int num)
        {
            //Determine if key is pressed
            if (!prevKb.IsKeyDown(key) && kb.IsKeyDown(key))
            {
                if (isBuying || isSelling)
                {
                    //Allow user to enter number when buying or selling
                    buySellNum = Convert.ToString(num);
                }
                else if (isUsing)
                {
                    //Allow user to enter num when using an item
                    useNum = Convert.ToString(num);
                }
            }
        }

        /// <summary>
        /// Allow user to buy item
        /// </summary>
        private void Buying()
        {
            //Store number chosen
            int num = Convert.ToInt32(buySellNum) - 1;
            dungeon.SetPlayerItemSelect(num);

            //Store cost of item
            int cost = Convert.ToInt32(shop.GetInventory()[num].CalcCost());
            
            //Determine if user can buy item
            if (dungeon.GetPlayerInventory().Count < dungeon.GetPlayerMaxSize() &&
                dungeon.GetPlayerCash() > cost)
            {
                //Buy item
                dungeon.PlayerBuy(shop.GetInventory(), shop);
                buyError = "";
            }
            else if (dungeon.GetPlayerCash() < cost)
            {
                //If user does not have enough cash, display message
                buyError = "Not enough cash left" + "\n" + "to buy item";
            }
            else if (dungeon.GetPlayerInventory().Count == dungeon.GetPlayerMaxSize())
            {
                //If user's inventory is full, display message
                buyError = "Reached max" + "\n" + "size of 6 in" + "\n" + "player inventory";
            }

            //Clear number selected
            buySellNum = "";
        }

        /// <summary>
        /// Allow user to sell item
        /// </summary>
        private void Selling()
        {
            //Store number chosen
            int num = Convert.ToInt32(buySellNum) - 1;
            dungeon.SetPlayerItemSelect(num);

            //Display message showing new value of item
            sellMessage = "The item selected sells for $" + dungeon.GetNewCost(shop) + "\n" +
                          "Select 'Sell' to sell item or 'Back' to return to shop";
        }

        /// <summary>
        /// Allow user to use item
        /// </summary>
        private void Using()
        {
            //Store number chosen
            int num = Convert.ToInt32(useNum) - 1;
            dungeon.SetPlayerItemSelect(num);

            //Store name and added bonus of item
            string name = dungeon.GetPlayerInventory()[num].GetName();
            int bonus = dungeon.GetPlayerInventory()[num].GetBonus();

            //Add to time left if time item is used
            if (name == "Time")
            {
                timeLeft += bonus * 1000;
            }

            //Allow player to use item
            dungeon.PlayerUse(name, bonus);

            //Player is no longer using item after item bonuses are applied
            isUsing = false;
        }
        
        /// <summary>
        /// Move player image when arrow keys are pressed
        /// </summary>
        /// <param name="directions"></param>
        /// <param name="side"></param>
        private void MovePlayerImage(bool[] directions, int side)
        {
            //Store adjustment and image length/width
            int imgLW = 53;
            int adjustment = 22;
        
            //Determine if arrow keys are pressed
            ArrowPressed(Keys.Up, directions[0], -1 * side, 0, -1 * (imgLW + adjustment));
            ArrowPressed(Keys.Right, directions[1], 1, imgLW + adjustment, 0);
            ArrowPressed(Keys.Down, directions[2], side, 0, imgLW + adjustment);
            ArrowPressed(Keys.Left, directions[3], -1, -1 * (imgLW + adjustment), 0);
        }

        /// <summary>
        /// Adjust player image when moving and update values
        /// </summary>
        /// <param name="key"></param>
        /// <param name="direction"></param>
        /// <param name="gridChange"></param>
        /// <param name="xChange"></param>
        /// <param name="yChange"></param>
        private void ArrowPressed(Keys key, bool direction, int gridChange, int xChange, int yChange)
        {
            //If an arrow key is pressed:
            if (!prevKb.IsKeyDown(key) && kb.IsKeyDown(key) && direction)
            {
                //Change player grid value
                dungeon.MovePlayer(gridChange);

                //Set default player positions
                dungeon.SetInShop(false);
                dungeon.SetInActiveTrap(false);
                dungeon.SetInPassiveTrap(false);

                //Time is not paused
                dungeon.SetPause(false);
            
                //Update player image X and Y values
                playerRec.X += xChange;
                playerRec.Y += yChange;
            }
        }

        /// <summary>
        /// Apply damage when in active trap
        /// </summary>
        /// <param name="gameTime"></param>
        private void ActiveTrap(GameTime gameTime)
        {
            //Check if player is in active trap
            if (dungeon.GetInActiveTrap())
            {
                if (isActiveTrapActivated)
                {
                    //Store time left
                    activeTimeLeft = dungeon.GetActiveTimeLeft();

                    //Start timer
                    activeTime += gameTime.ElapsedGameTime.Milliseconds;

                    //Arm and disarm trap at escape intervals
                    if (activeTime < activeTimeLeft)
                    {
                        dungeon.GetCurrentTrap().SetArmed(true);
                        dungeon.GetCurrentTrap().ArmTrap(dungeon.GetPlayerHealth());
                        dungeon.InTrap(dungeon.GetCurrentTrap().GetHealth());
                        dungeon.GetCurrentTrap().DisarmTrap();
                    }
                    else if (activeTime > activeTimeLeft)
                    {
                        dungeon.GetCurrentTrap().DisarmTrap();
                        activeTime = 0;
                    }
                }
                else
                {
                    //Ensure player is damaged when first in trap
                    isActiveTrapActivated = true;
                }
            }
            else
            {
                //Trap is not activated when player leaves
                isActiveTrapActivated = false;
            }
        }

        /// <summary>
        /// Apply damage when in passive trap
        /// </summary>
        /// <param name="gameTime"></param>
        private void PassiveTrap(GameTime gameTime)
        {
            //Check if player is in passive trap
            if (dungeon.GetInPassiveTrap())
            {
                //Store time left
                passiveTimeLeft = dungeon.GetPassiveTimeLeft();

                //Start timer
                passiveTime += gameTime.ElapsedGameTime.Milliseconds;

                //Check if timer is finished and player is still in trap
                if (passiveTime > passiveTimeLeft)
                {
                    //Arm trap and cause damage
                    dungeon.GetCurrentTrap().ArmTrap(dungeon.GetPlayerHealth());
                    dungeon.InTrap(dungeon.GetCurrentTrap().GetHealth());

                    //Disarm trap
                    dungeon.GetCurrentTrap().DisarmTrap();

                    //Reset time
                    passiveTime = 0;
                }
            }
            else
            {
                //After player leaves trap, disarm trap
                if (passiveTime < passiveTimeLeft)
                {
                    dungeon.GetCurrentTrap().DisarmTrap();
                }
            }
        }
    
        /// <summary>
        /// Determine adjusted values after using power ups
        /// </summary>
        private void UsingPowerUps()
        {
            //Store durability
            int durability = Convert.ToInt32(dungeon.GetPowerUpUsing().GetDurability());

            //Control data for using weapons and armour
            if (dungeon.GetPowerUpUsing().GetName() == "Sword" && durability == 0)
            {
                //Apply damage to sword when durability is at 0
                if ((int)(dungeon.GetPowerUpUsing().GetDamage() * 0.25) >= 1)
                {
                    int newDamage = (int)(dungeon.GetPowerUpUsing().GetDamage() * 0.25);
                    dungeon.GetPowerUpUsing().SetDamage(newDamage);
                }

                dungeon.SetIsUsing(false);

            }
            else if (dungeon.GetPowerUpUsing().GetName() == "Cookie Sheet" && durability == 0)
            {
                //Adjust defense modifier of cookie sheet
                if ((int)(dungeon.GetPowerUpUsing().GetDefense() * 0.2) >= 1)
                {
                    int newDefense = (int)(dungeon.GetPowerUpUsing().GetDefense() * 0.2);
                    dungeon.GetPowerUpUsing().SetDefense(newDefense);
                }

                dungeon.SetIsUsing(false);
            }
            else if (dungeon.GetPowerUpUsing().GetName() == "Bucket" && durability == 0)
            {
                //Adjust defense modifier of bucket
                if ((int)(dungeon.GetPowerUpUsing().GetDefense() * 0.25) >= 1)
                {
                    int newDefense = (int)(dungeon.GetPowerUpUsing().GetDefense() * 0.25);
                    dungeon.GetPowerUpUsing().SetDefense(newDefense);
                }

                dungeon.SetIsUsing(false);
            }
        }
    }
}
