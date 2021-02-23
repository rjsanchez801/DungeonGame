using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

/// <summary>
/// @Author Trevor Davis, Lucas Montoya, Ramon Sanchez
/// </summary>
namespace DungeonGame
{
    /// <summary>
    /// Game represents a class that initializes all the objects used in the game
    /// </summary>
    class Game
    {
        public static bool gameOver = false;
        // May not need this. Game may not be able to do multiple sounds without forms
        public static Thread MenuMusicThread;
        public static Thread BGThread;
        public static int points = 0;
        public static int gridXLength = 60;
        public static int gridYHeight = 20;
        public static int leftMargin = 5;
        public static int topMargin = 5;
        public int level = 0;
        public int levelCounter = 1;
        private int monX;
        private int monY;
        private int coinX;
        private int coinY;
        private int wallArraySize = 4;
        private ConsoleColor mainGridBackgroundClr;
        private ConsoleColor mainGridWallClr;
        public static Attributes character;
        public static IList<Attributes> listOfMonsters = new List<Attributes>();
        public static IList<IList<Obstacles>> allWalls = new List<IList<Obstacles>>();
        private IList<Obstacles> topWall = new List<Obstacles>();
        private IList<Obstacles> bottomWall = new List<Obstacles>();
        private IList<Obstacles> rightWall = new List<Obstacles>();
        private IList<Obstacles> leftWall = new List<Obstacles>();
        private IList<Item> coins = new List<Item>();
        private IList<Item> swingUp = new List<Item>();
        private IList<Item> swingLeft = new List<Item>();
        private IList<Item> swingRight = new List<Item>();
        private IList<Item> swingDown = new List<Item>();
        public static IList<Item> magicList = new List<Item>();
        public static IList<Item> arrowList = new List<Item>();
        public static IList<IList<Item>> sword = new List<IList<Item>>();
        private Item magicIceUp;
        private Item magicIceDown;
        private Item magicIceLeft;
        private Item magicIceRight;    
        private Collision collisions = new Collision();
        Display display = new Display();
        private Monster monsters = new Monster();
        private Player hero = new Player();
        private bool monMoveYet = false;
        private IList<Item> door = new List<Item>();
        public static System.Timers.Timer monTimer;
        public static int freezeCount = 0;
        private int monTimerInterval = 500;
        Random rand = new Random();
        ConsoleKeyInfo keyInfo;
        private static WMPLib.WindowsMediaPlayer audio = new WMPLib.WindowsMediaPlayer();
        public WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)audio.controls;

        System.Media.SoundPlayer backgroundMusic = new System.Media.SoundPlayer(@"Possible music.wav");
        System.Media.SoundPlayer menuMusic = new System.Media.SoundPlayer(@"CalmMusic.wav");

        /// <summary>
        /// May not need this
        /// </summary>
        public void Setup()
        {            
            MenuMusicThread = new Thread(PlayMenuMusic);
            MenuMusicThread.Start();
            display.Setup();
            
        }

        /// <summary>
        /// Starts the game by initiating all the components and running the data.
        /// In particular it initializes the player, monsters, walls, item locations, 
        /// and the door.
        /// It also calls the display methods to print the walls and grid.
        /// </summary>
        public void startGame(String name, int hp, string weapon, ConsoleColor color, String symbol)
        {
            gameOver = false;
            menuMusic.Stop();
            BGThread = new Thread(PlayBGMusic);
            BGThread.Start();
            Display.LoadTextFile("Level.txt", 38, 1, ConsoleColor.Black, ConsoleColor.Gray);
            Display.SetLevelText(57, 2, levelCounter, ConsoleColor.Black, ConsoleColor.Gray);
            Keys keys = new Keys();
            mainGridBackgroundClr = ConsoleColor.Black;
            mainGridWallClr = ConsoleColor.Gray;
            Console.SetCursorPosition(6, 6);
            character = new Attributes(name, hp, weapon, Console.CursorLeft, Console.CursorTop, color, symbol);
            monTimer = new System.Timers.Timer(monTimerInterval);
            monTimer.Start();
            monTimer.Elapsed += MonTimer_Elapsed;

            if (character.Color.Equals(ConsoleColor.Blue))
                createSword();
            if (character.Color.Equals(ConsoleColor.Green))
                createMagic();
            createWalls(wallArraySize);
            createMonsters(5);
            createCoins(10);
            createDoor();
            display.DrawRectangle(gridXLength, gridYHeight, leftMargin, topMargin, mainGridWallClr, mainGridBackgroundClr);
            display.drawWalls(allWalls, mainGridBackgroundClr);
            Display.DrawItemLists(coins, ConsoleColor.Black);
            display.SetStatScreen((gridXLength + 3), topMargin, 25, 20, color, ConsoleColor.Black, character);
            Display.DrawItemLists(door, ConsoleColor.White);
            PlaySounds("Monster.wav", 100);            

            while (gameOver == false)
            {
                
                Console.CursorVisible = false;
                hero.DrawHero(character, mainGridBackgroundClr);
                monsters.DrawMonster(listOfMonsters, mainGridBackgroundClr);
                Display.DrawItemLists(coins, ConsoleColor.Black);

                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        hero.Action(keyInfo.Key, character, listOfMonsters);
                    }
                    else
                    {
                        hero.move(keyInfo.Key, allWalls, gridXLength, gridYHeight, listOfMonsters, coins, character, mainGridBackgroundClr, sword, magicList);
                    }
                }

                if (monMoveYet == true)
                {                    
                    monsters.move(listOfMonsters, character, allWalls, gridXLength, gridYHeight, mainGridBackgroundClr, coins);
                    if (Player.CheckHeroDead())
                    {
                        gameOver = true;
                        character.X = 6;
                        character.Y = 6;
                        if (character.Color.Equals(ConsoleColor.Blue))
                        {
                            sword.Clear();
                            swingDown.Clear();
                            swingLeft.Clear();
                            swingUp.Clear();
                            swingRight.Clear();
                        }
                        if (character.Color.Equals(ConsoleColor.Green))
                        {
                            reCenterMagicList();
                        }
                        ResetAllLists();
                        HeroDeath();
                    }
                        
                    monMoveYet = false;
                }
                if (collisions.checkHeroDoor(door, character))
                {                    
                    nextLevel();
                }
            }
        }

        /// <summary>
        /// Creates a tutorial for the user to understand how to play the game.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hp"></param>
        /// <param name="weapon"></param>
        /// <param name="color"></param>
        /// <param name="symbol"></param>
        public void Tutorial(String name, int hp, string weapon, ConsoleColor color, String symbol)
        {
            mainGridBackgroundClr = ConsoleColor.Black;
            mainGridWallClr = ConsoleColor.Gray;
            Console.SetCursorPosition(6, 6);
            character = new Attributes(name, hp, weapon, Console.CursorLeft, Console.CursorTop, color, symbol);
            createWalls(wallArraySize);
            createMonsters(5);
            createCoins(10);
            createDoor();
            display.DrawRectangle(gridXLength, gridYHeight, leftMargin, topMargin, mainGridWallClr, mainGridBackgroundClr);
            display.drawWalls(allWalls, mainGridBackgroundClr);
            Display.DrawItemLists(coins, ConsoleColor.Black);
            Display.DrawItemLists(door, ConsoleColor.White);
            Display.LoadTextFile("Tutorial.txt", 68, 7, ConsoleColor.Black, ConsoleColor.White);            
            Console.CursorVisible = false;
            hero.DrawHero(character, mainGridBackgroundClr);
            monsters.DrawMonster(listOfMonsters, mainGridBackgroundClr);
            Display.DrawItemLists(coins, ConsoleColor.Black);
            while (true)
            {
                display.TutorialReadKey();
            }
        }

        /// <summary>
        /// The timer event for moving the monsters. This turns the bool
        /// "monMoveYet" to true so that the monsters can move. I was having 
        /// an issue with placing the actual method call of monster move.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            monMoveYet = true;
        }

        /// <summary>
        /// Creates 4 arrays of obstacles: Top, Bottom, Left, and Right.
        /// Each Wall has an array of Obstacles that have an X,Y position and size.
        /// After each wall is checked for collision then it fills the wall into
        /// the allWalls list to print.
        /// </summary>
        public void createWalls(int size)
        {
            int topWallSize = rand.Next((int)(Math.Round(gridYHeight * .20)), (int)(Math.Round(gridYHeight * .70)));
            int bottomWallSize = rand.Next((int)(Math.Round(gridYHeight * .20)), (int)(Math.Round(gridYHeight * .70)));
            int leftWallSize = rand.Next((int)(Math.Round(gridXLength * .20)), (int)Math.Round(gridXLength * .70));
            int rightWallSize = rand.Next((int)(Math.Round(gridXLength * .20)), (int)Math.Round(gridXLength * .70));
            int randWallX = rand.Next((leftMargin + 5), ((leftMargin + gridXLength) - 5));
            int randWallY = rand.Next((topMargin + 5), ((topMargin + gridYHeight) - 5));

            for (int i = 0; i < topWallSize; i++)
            {
                topWall.Add(new Obstacles(randWallX, randWallX + 1, (topMargin + 1) + i, ConsoleColor.Gray));
            }
            allWalls.Add(topWall);

            randWallX = rand.Next((leftMargin + 5), ((leftMargin + gridXLength) - 5));
            for (int i = 0; i < bottomWallSize; i++)
            {
                if (collisions.checkWall(allWalls, randWallX, ((topMargin - 2) + gridYHeight) - i))
                {
                    break;
                }
                bottomWall.Add(new Obstacles(randWallX, randWallX + 1, ((topMargin - 2) + gridYHeight) - i, ConsoleColor.Gray));
            }
            allWalls.Add(bottomWall);

            for (int i = 0; i < rightWallSize; i++)
            {
                if (collisions.checkWall(allWalls, (leftMargin + gridXLength) - i, randWallY))
                {
                    break;
                }
                rightWall.Add(new Obstacles((leftMargin + gridXLength) - i, ((leftMargin + gridXLength) - i), randWallY, ConsoleColor.Gray));
            }
            allWalls.Add(rightWall);

            randWallY = rand.Next((topMargin + 5), ((topMargin + gridYHeight) - 5));
            for (int i = 0; i < leftWallSize; i++)
            {
                if (collisions.checkWall(allWalls, (leftMargin + 1) + i, randWallY))
                {
                    break;
                }
                leftWall.Add(new Obstacles((leftMargin + 1) + i, (leftMargin + 1) + i, randWallY, ConsoleColor.Gray));
            }
            allWalls.Add(leftWall);
        }

        /// <summary>
        /// Creates a list of Attributes which is filled by monsters. 
        /// May need to change this to work move() and Draw()
        /// </summary>
        /// <param name="size"></param>
        public void createMonsters(int size)
        {
            for (int i = 0; i < size; i++)
            {
                monX = rand.Next((leftMargin + 1), (gridXLength - 1));
                monY = rand.Next((topMargin + 1), (gridYHeight - 1));

                // Checks to make sure the random X and Y location is not the same as a wall.
                for (int j = 0; j < allWalls.Count; j++)
                {
                    for (int k = 0; k < allWalls[j].Count; k++)
                    {

                        if (monX.Equals(allWalls[j][k].X) && monY.Equals(allWalls[j][k].Y) || monX.Equals(allWalls[j][k].X2) && monY.Equals(allWalls[j][k].Y))
                        {
                            while (monX.Equals(allWalls[j][k].X) && monY.Equals(allWalls[j][k].Y) || monX.Equals(allWalls[j][k].X2) && monY.Equals(allWalls[j][k].Y))
                            {
                                monX = rand.Next((leftMargin + 1), (gridXLength - 1));
                                monY = rand.Next((topMargin + 1), (gridYHeight - 1));
                            }
                        }
                    }
                }                
                listOfMonsters.Add(new Attributes("Kill me", 20, "claw", monX, monY, ConsoleColor.Red, "M", 10));
            }
        }

        /// <summary>
        /// Creates a list of coins to scatter around the level
        /// </summary>
        /// <param name="num"></param>
        public void createCoins(int num)
        {
            for (int i = 0; i < num; i++)
            {
                coinX = rand.Next((leftMargin + 1), (gridXLength - 1));
                coinY = rand.Next((topMargin + 1), (gridYHeight - 1));

                // Checks to make sure the random X and Y location is not the same as a wall.
                for (int j = 0; j < allWalls.Count; j++)
                {
                    for (int k = 0; k < allWalls[j].Count; k++)
                    {

                        if (coinX.Equals(allWalls[j][k].X) && coinY.Equals(allWalls[j][k].Y) || coinX.Equals(allWalls[j][k].X2) && coinY.Equals(allWalls[j][k].Y))
                        {
                            while (coinX.Equals(allWalls[j][k].X) && coinY.Equals(allWalls[j][k].Y) || coinX.Equals(allWalls[j][k].X2) && coinY.Equals(allWalls[j][k].Y))
                            {
                                coinX = rand.Next((leftMargin + 1), (gridXLength - 1));
                                coinY = rand.Next((topMargin + 1), (gridYHeight - 1));
                            }
                        }
                    }
                }
                coins.Add(new Item(coinX, coinY, "o", ConsoleColor.Yellow));
            }
        }

        /// <summary>
        /// Creates a list of item called door with a random location either on the far right, right-bottom,
        /// or bottom-left corner
        /// </summary>
        public void createDoor()
        {

            int location = rand.Next(1, 4);
            if (location == 1)
            {
                door.Add(new Item((gridXLength + leftMargin) - 3, (gridXLength + leftMargin) - 2, topMargin + 3, "  ", ConsoleColor.White));
                door.Add(new Item((gridXLength + leftMargin) - 2, topMargin + 2, " ", ConsoleColor.White));
            }
            if (location == 2)
            {
                door.Add(new Item((gridXLength + leftMargin) - 3, (gridXLength + leftMargin) - 2, (topMargin + gridYHeight) - 2, "  ", ConsoleColor.White));
                door.Add(new Item((gridXLength + leftMargin) - 2, (topMargin + gridYHeight) - 3, " ", ConsoleColor.White));
            }
            if (location == 3)
            {
                door.Add(new Item(leftMargin + 3, leftMargin + 4, (topMargin + gridYHeight) - 2, "  ", ConsoleColor.White));
                door.Add(new Item(leftMargin + 4, (topMargin + gridYHeight) - 3, " ", ConsoleColor.White));
            }
        }

        /// <summary>
        /// When user enters a door, it causes all the lists to be cleared, the player moved 
        /// to the top left, and then increase the level by 1.
        /// </summary>
        public void nextLevel()
        {   
            character.X = 6;
            character.Y = 6;
            ResetAllLists();
            if (character.Color.Equals(ConsoleColor.Blue))
            {
                sword.Clear();
                swingDown.Clear();
                swingLeft.Clear();
                swingUp.Clear();
                swingRight.Clear();
            }
            if (character.Color.Equals(ConsoleColor.Green))
            {
                reCenterMagicList();
            }
            level += 1;
            createLevel();
        }
        /// <summary>
        /// Resets the tutorial level to avoid bugs when game starts
        /// </summary>
        public void TutorialReset()
        {
            character.X = 6;
            character.Y = 6;
            ResetAllLists();
            level = 0;
        }

        /// <summary>
        /// Creates the level by creating the walls, monsters, coins,
        /// and door. Then it calls display to draw them.
        /// </summary>
        public void createLevel()
        {
            //Thread Sleep to hear the stepping on stairs audio :)
            Thread.Sleep(1000);            
            levelCounter++;
            PlaySounds("Monster.wav", 75);
            Display.LoadTextFile("Level.txt", 38, 1, ConsoleColor.Black, ConsoleColor.Gray);
            Display.SetLevelText(57, 2, levelCounter, ConsoleColor.Black, ConsoleColor.Gray);

            createWalls(wallArraySize);
            createMonsters(5 + (level + 1));
            createCoins(10);
            createDoor();
            if (character.Color.Equals(ConsoleColor.Blue))
                createSword();
            display.DrawRectangle(gridXLength, gridYHeight, leftMargin, topMargin, mainGridWallClr, mainGridBackgroundClr);
            display.drawWalls(allWalls, mainGridBackgroundClr);
            Display.DrawItemLists(coins, ConsoleColor.Black);
            display.SetStatScreen((gridXLength + 3), topMargin, 25, 20, character.Color, ConsoleColor.Black, character);
            Display.DrawItemLists(door, ConsoleColor.White);
        }

        /// <summary>
        /// Creates the sword to use in any direction. It places the string into an Ilist
        /// that is then called to see if the X,Y positions are te same as any monster or 
        /// wall.
        /// </summary>
        public void createSword()
        {
            Item swingUp1 = new Item(character.X - 1, character.Y - 1, "\\", ConsoleColor.White);
            Item swingUp2 = new Item(character.X, character.Y - 1, "|", ConsoleColor.White);
            Item swingUp3 = new Item(character.X + 1, character.Y - 1, "/", ConsoleColor.White);

            Item swingLeft1 = new Item(character.X - 1, character.Y - 1, "\\", ConsoleColor.White);
            Item swingLeft2 = new Item(character.X - 2, character.X - 3, character.Y, "--", ConsoleColor.White);
            Item swingLeft3 = new Item(character.X - 1, character.Y + 1, "/", ConsoleColor.White);

            Item swingRight1 = new Item(character.X + 1, character.Y - 1, "/", ConsoleColor.White);
            Item swingRight2 = new Item(character.X + 1, character.X + 2, character.Y, "--", ConsoleColor.White);
            Item swingRight3 = new Item(character.X + 1, character.Y + 1, "\\", ConsoleColor.White);

            Item swingDown1 = new Item(character.X + 1, character.Y + 1, "\\", ConsoleColor.White);
            Item swingDown2 = new Item(character.X, character.Y + 1, "|", ConsoleColor.White);
            Item swingDown3 = new Item(character.X - 1, character.Y + 1, "/", ConsoleColor.White);

            swingUp.Add(swingUp1);
            swingUp.Add(swingUp2);
            swingUp.Add(swingUp3);

            swingLeft.Add(swingLeft1);
            swingLeft.Add(swingLeft2);
            swingLeft.Add(swingLeft3);

            swingRight.Add(swingRight1);
            swingRight.Add(swingRight2);
            swingRight.Add(swingRight3);

            swingDown.Add(swingDown1);
            swingDown.Add(swingDown2);
            swingDown.Add(swingDown3);

            sword.Add(swingUp);
            sword.Add(swingLeft);
            sword.Add(swingRight);
            sword.Add(swingDown);
        }

        /// <summary>
        /// Creates a list of Items. Each one has an initial direction and will 
        /// be updated in the Player Class as the player moves.
        /// </summary>
        public void createMagic()
        {
            magicIceUp = new Item(Game.character.X, Game.character.Y - 1, "+", ConsoleColor.Cyan);
            magicIceRight = new Item(Game.character.X + 1, Game.character.Y, "+", ConsoleColor.Cyan);
            magicIceLeft = new Item(Game.character.X - 1, Game.character.Y, "+", ConsoleColor.Cyan);
            magicIceDown = new Item(Game.character.X, Game.character.Y + 1, "+", ConsoleColor.Cyan);

            magicList.Add(magicIceUp);
            magicList.Add(magicIceRight);
            magicList.Add(magicIceLeft);
            magicList.Add(magicIceDown);
        }

        /// <summary>
        /// Trying to get this method to work with a thread to play 
        /// music, while allowing the game to have sound effects.
        /// </summary>
        public void PlayBGMusic()
        {
            backgroundMusic = new System.Media.SoundPlayer(@"Possible music.wav");
            backgroundMusic.PlayLooping();
        }

        private void PlayMenuMusic()
        {
            menuMusic = new System.Media.SoundPlayer(@"CalmMusic.wav");
            menuMusic.PlayLooping();
        }

        /// <summary>
        /// recenters all the magic X,Y when going to the next level
        /// </summary>
        public void reCenterMagicList()
        {
            magicList.Clear();
            magicIceDown.X = character.X;
            magicIceDown.Y = character.Y + 1;
            magicIceUp.X = character.X;
            magicIceUp.Y = character.Y - 1;
            magicIceLeft.X = character.X - 1;
            magicIceLeft.Y = character.Y;
            magicIceRight.X = character.X + 1;
            magicIceRight.Y = character.Y;
            magicList.Add(magicIceUp);
            magicList.Add(magicIceRight);
            magicList.Add(magicIceLeft);
            magicList.Add(magicIceDown);
        }

        /// <summary>
        /// Occurs When the hero dies
        /// </summary>
        public void HeroDeath()
        {
            backgroundMusic.Stop();
            menuMusic.PlayLooping();
            using (StreamWriter sw = new StreamWriter("HighScores.txt", true))
            {
                sw.WriteLine(character.Name + "\t" + level + "\t\t\t" + Game.points);                
            }
            //gameOver = true;            
            PlaySounds("GameOver.wav", 75);
            display.HighScoreScreen();
            level = 0;
        }

        /// <summary>
        /// Alternative method to playing game sounds, might not work on all computers
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="volume"></param>
        public void PlaySounds(string fileName, int volume)
        {
            audio.URL = @fileName;            
            audio.settings.volume = volume;
            controls.pause();            
        }

        /// <summary>
        /// Resets all the lists and recenters the magic so that they can be reused.
        /// </summary>
        public void ResetAllLists()
        {
            Console.Clear();
            listOfMonsters.Clear();
            coins.Clear();
            allWalls.Clear();
            topWall.Clear();
            bottomWall.Clear();
            leftWall.Clear();
            rightWall.Clear();
            door.Clear();
        }
    }
}
