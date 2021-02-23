using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// @Author Trevor Davis, Lucas Montoya, Ramon Sanchez
/// </summary>
namespace DungeonGame
{
    /// <summary>
    /// Display represents a class for the user interface, its menu options
    /// and processes user keys for menu selection
    /// </summary>
    class Display
    {        
        int counter;
        string name;
        List<string> Warrior = new List<string>() {"HP: 50", "AP: 5", "------", "Weapon: Sword", "Item: Buff Potion"};
        List<string> Hunter = new List<string>() { "(⌐■_■)==>", "The Hunter!", "", "HP: 50", "AP: 30", "Weapon: Dagger", "Item: Cloak Potion" };
        List<string> Wizard = new List<string>() { "HP: 30", "AP: 1", "------", "Weapon: Ice", "Item: Fire Scroll" };
        /// <summary>
        /// Starts the music of the game and draws the start menu out.
        /// </summary>
        public void Setup()
        {
            Console.Clear();
            Console.SetWindowSize(100, 30);
            DrawRectangle(90, 25, 5, 2, ConsoleColor.Red, ConsoleColor.Black);            
            LoadTextFile("Title.txt", 10, 4, ConsoleColor.Black, ConsoleColor.Red);
            LoadTextFile("Menu.txt", 65, 18, ConsoleColor.Black, ConsoleColor.Gray);            
            while (true)
            {
                MenuKey();                
            }
        }
                
        /// <summary>
        /// Character Info Stats When Game is Started
        /// </summary>
        private void CharacterInfo()
        {   
            // Three rectangles for character screens
            DrawRectangle(85, 9, 5, 2, ConsoleColor.Blue, ConsoleColor.Black);            
            DrawRectangle(85, 9, 5, 12, ConsoleColor.Green, ConsoleColor.Black);

            // Warrior
            LoadTextFile("Warrior.txt", 7, 4, ConsoleColor.Black, ConsoleColor.Blue);
            int counter = 4;
            foreach (String w in Warrior)
            {
                SetCharText(50, counter, w, ConsoleColor.Black, ConsoleColor.White);
                counter++;
            }
            // Wizard
            LoadTextFile("Wizard.txt", 7, 14, ConsoleColor.Black, ConsoleColor.Green);
            counter = 14;
            foreach (String w in Wizard)
            {
                SetCharText(50, counter, w, ConsoleColor.Black, ConsoleColor.White);
                counter++;
            }
            // Choices
            LoadTextFile("CharChoice.txt", 35, 22, ConsoleColor.Black, ConsoleColor.White);

            // Check user input
            while (true)
            {
                CharacterKey();
            }
        }

        /// <summary>
        /// How to Play Screen When Clicked on From Start Menu
        /// </summary>
        private void HowToPlay()
        {
            Game game = new Game();
            game.Tutorial("Bob", 10, "Stick", ConsoleColor.White, ">");

        }

        /// <summary>
        /// Draw rectangles with custom length, height, leftMargin, topMargin, and color parameters.
        /// The color parameters are for the background of the walls and the background of the floor.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="color"></param>
        public void DrawRectangle(int length, int height, int left, int top, ConsoleColor BGColorWall, ConsoleColor BGColorFloor)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < height; i++)
            {
                Console.CursorLeft = left;
                Console.BackgroundColor = BGColorWall;
                Console.Write(" ");
                for (int j = 0; j < length; j++)
                {

                    if (i == 0 || i == height - 1 || j == length - 1)
                    {
                        Console.BackgroundColor = BGColorWall;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = BGColorFloor;
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        /// <summary>
        /// Draws walls for map from the IList allWalls.
        /// </summary>
        /// <param name="walls"></param>
        public void drawWalls(IList<IList<Obstacles>> walls, ConsoleColor BGColor)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                for (int j = 0; j < walls[i].Count; j++)
                {
                    Console.SetCursorPosition(walls[i][j].X, walls[i][j].Y);
                    Console.BackgroundColor = walls[i][j].Color;
                    Console.Write(" ");
                    Console.SetCursorPosition(walls[i][j].X2, walls[i][j].Y);
                    Console.BackgroundColor = walls[i][j].Color;
                    Console.Write(" ");
                }

            }
        }
        /// <summary>
        /// Main Menu Screen Key Check
        /// </summary>
        private void MenuKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    Console.Clear();
                    CharacterInfo();
                }
                if (key == ConsoleKey.D2)
                {
                    Console.Clear();
                    HowToPlay();
                }
                if (key == ConsoleKey.D3)
                {
                    HighScoreScreen();
                }
                if (key == ConsoleKey.D4)
                {
                    Environment.Exit(0);
                }
            }            
        }
        /// <summary>
        /// Character Selection Screen Key Check
        /// </summary>
        private void CharacterKey()
        {
            Game game = new Game();
            counter = 0;            
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    game.PlaySounds("Select.mp3", 40);
                    while (counter < 5)
                    {
                        DrawRectangle(85, 9, 5, 2, ConsoleColor.White, ConsoleColor.Black);
                        Thread.Sleep(50);
                        DrawRectangle(85, 9, 5, 2, ConsoleColor.Blue, ConsoleColor.Black);
                        Thread.Sleep(50);
                        counter++;
                    }                    
                    Console.Clear();
                    EnterPlayerName(name, 50, "Sword", ConsoleColor.Blue, ">");                    

                }
                if (key == ConsoleKey.D2)
                {
                    game.PlaySounds("Select.mp3", 40);
                    while (counter < 5)
                    {
                        DrawRectangle(85, 9, 5, 12, ConsoleColor.White, ConsoleColor.Black);
                        Thread.Sleep(50);
                        DrawRectangle(85, 9, 5, 12, ConsoleColor.Green, ConsoleColor.Black);
                        Thread.Sleep(50);
                        counter++;
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    EnterPlayerName(name, 30, "Staff", ConsoleColor.Green, ">");

                }
            }
        }
        /// <summary>
        /// Method that sets the left and top position of Console Write, which elminates the use
        /// of Set Cursor Position
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="text"></param>
        /// <param name="BGColor"></param>
        /// <param name="FGColor"></param>
        public static void SetCharText(int left, int top, string text,ConsoleColor BGColor, ConsoleColor FGColor)
        {
            Console.BackgroundColor = BGColor;
            Console.ForegroundColor = FGColor;
            Console.SetCursorPosition(left, top);
            Console.Write(text);            
        }
        /// <summary>
        /// Changes the number on the top of the map according to which level you are on
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="level"></param>
        /// <param name="BGColor"></param>
        /// <param name="FGColor"></param>
        public static void SetLevelText(int left, int top, int level, ConsoleColor BGColor, ConsoleColor FGColor)
        {
            Console.BackgroundColor = BGColor;
            Console.ForegroundColor = FGColor;
            Console.SetCursorPosition(left, top);
            Console.Write(level);
        }

        /// <summary>
        /// Creates the stats screen on the right hand side with the characters current stats
        /// There's probably a much better way to do this. 
        /// </summary>
        /// <param name="rightBorderX"></param>
        /// <param name="topMargin"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="borderClr"></param>
        /// <param name="BGColor"></param>
        /// <param name="hero"></param>
        public void SetStatScreen(int rightBorderX, int topMargin, int length, int height, ConsoleColor borderClr, ConsoleColor BGColor, Attributes hero)
        {
            DrawRectangle(length, height, (rightBorderX + 5), (topMargin), borderClr, BGColor);
            Console.BackgroundColor = BGColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((rightBorderX + 8), (topMargin + 4));
            Console.Write("Hero Name: " + hero.Name);
            Console.WriteLine();
            UpdateHP();
            Console.WriteLine();
            Console.CursorLeft = (rightBorderX + 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Weapon: " + hero.Weapon);
            Console.WriteLine();
            Console.CursorLeft = (rightBorderX + 8);
            Console.WriteLine();
            Console.CursorLeft = (rightBorderX + 8);
            UpdatePoints();
        }

        /// <summary>
        /// Updates Heroes HP on Stat Screen
        /// </summary>
        public static void UpdateHP()
        {
            Console.SetCursorPosition((Game.gridXLength + Game.leftMargin) + 6, Game.topMargin + 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"HP: {Game.character.HP} ");            
        }

        /// <summary>
        /// Only Updates the Points instead of redrawing the grid.
        /// </summary>
        public static void UpdatePoints()
        {
            Game game = new Game();
            Monster monster = new Monster();
            Console.SetCursorPosition((Game.gridXLength + Game.leftMargin) + 6, Game.topMargin + 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Coins collected: " + Game.points);                        
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Loads a text file from Dungeon Game bin folder
        /// </summary>
        /// <param name="textFile"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="BGColor"></param>
        /// <param name="FGColor"></param>
        public static void LoadTextFile(string textFile, int left, int top, ConsoleColor BGColor, ConsoleColor FGColor)
        {
            int counter = 0;
            Console.BackgroundColor = BGColor;
            Console.ForegroundColor = FGColor;
            string ma;
            using (StreamReader reader = new StreamReader(textFile))
            {   
                while (true)
                {                    
                    ma = reader.ReadLine();
                    if(ma == null)
                    {
                        break;
                    }
                    Console.SetCursorPosition(left, (top + counter));
                    Console.Write(ma);                    
                    counter++;
                }   
            }
            Console.CursorVisible = false;
        }
        /// <summary>
        /// Changes Level Number Art
        /// </summary>
        public static void LoadLevelText()
        {              
            LoadTextFile("Level.txt", 40, 0, ConsoleColor.Black, ConsoleColor.Red);
        }

        /// <summary>
        /// Draws Coins on game map
        /// </summary>
        /// <param name="item"></param>
        /// <param name="BGColor"></param>
        public static void DrawItemLists(IList<Item> item, ConsoleColor BGColor)
        {
            for (int i = 0; i < item.Count; i++)
            {
                Console.SetCursorPosition(item[i].X, item[i].Y);
                Console.BackgroundColor = BGColor;
                Console.ForegroundColor = item[i].Color;
                Console.Write(item[i].Symbol);
            }
        }
        /// <summary>
        /// High Score screen with title and menu options
        /// </summary>
        public void HighScoreScreen()
        {            
            Console.Clear();            
            DrawRectangle(90, 25, 5, 2, ConsoleColor.Red, ConsoleColor.Black);
            LoadTextFile("HighScoresTitle.txt", 9, 5, ConsoleColor.Black, ConsoleColor.Red);
            DrawRectangle(45, 10, 10, 15, ConsoleColor.DarkGray, ConsoleColor.Black);
            SetCharText(12, 16, "Name	Levels Cleared		Score", ConsoleColor.Black, ConsoleColor.Gray);
            SetCharText(12, 17, "-----------------------------------------", ConsoleColor.Black, ConsoleColor.Gray);
            LoadTextFile("HighScores.txt", 12, 18, ConsoleColor.Black, ConsoleColor.Gray);
            LoadTextFile("HighScoresMenu.txt", 60, 18, ConsoleColor.Black, ConsoleColor.Gray);
            while (true)
            {
                HighScoresReadKey();
            }
            
        }
        /// <summary>
        /// Resets the high score if the user desires
        /// </summary>
        public void ResetHighScores()
        {
            using (StreamWriter sw = File.CreateText(@"HighScores.txt"))
            {
                sw.Flush();
                sw.Close();
            }
        }
        /// <summary>
        /// Asks for user input before game starts, so you can enter your name for
        /// the high score screen
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hp"></param>
        /// <param name="weapon"></param>
        /// <param name="color"></param>
        /// <param name="symbol"></param>
        public void EnterPlayerName(String name, int hp, string weapon, ConsoleColor color, String symbol)
        {            
            Game game = new Game();
            LoadTextFile("Skull.txt", 11, 1, ConsoleColor.Black, ConsoleColor.Gray);
            SetCharText(20, 23, "Enter Your First Name then Press 'Enter' to Proceed...\n", ConsoleColor.Black, ConsoleColor.Gray);
            Console.SetCursorPosition(20, 24);
            Console.CursorVisible = true;
            while (true)
            {
                string playerName = Console.ReadLine();                
                if (playerName != "")
                {
                    Console.Clear();                    
                    game.startGame(playerName, hp, weapon, color, symbol);
                }
                else
                {
                    Console.Clear();
                    EnterPlayerName(playerName, hp, weapon, color, symbol);
                }
            }

        }
        /// <summary>
        /// Reads the menu options in the tutorial menu
        /// </summary>
        public void TutorialReadKey()
        {
            if (Console.KeyAvailable)
            {
                Game game = new Game();
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    game.TutorialReset();
                    CharacterInfo();
                }
                if (key == ConsoleKey.D2)
                {
                    game.TutorialReset();
                    Setup();
                }
            }
        }
        /// <summary>
        /// Reads the menu options in the high score menu
        /// </summary>
        public void HighScoresReadKey()
        {
            if (Console.KeyAvailable)
            {
                Game game = new Game();
                ConsoleKey key = Console.ReadKey(true).Key;                
                if (key == ConsoleKey.D1)
                {   
                    game.TutorialReset();
                    Setup();                          
                }
                if (key == ConsoleKey.D2)
                {
                    Console.Clear();
                    ResetHighScores();
                    HighScoreScreen();
                }
                if (key == ConsoleKey.D3)
                {
                    Environment.Exit(0);
                }
            }
            
        }
    }
}
