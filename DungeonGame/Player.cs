using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// @Author Trevor Davis, Lucas Montoya, Ramon Sanchez
/// </summary>
namespace DungeonGame
{
    /// <summary>
    /// Player represents a class the movement of the player, its attack moves
    /// and when the player dies
    /// </summary>
    class Player
    {
        Collision collision = new Collision();

        /// <summary>
        /// Check to see if the new coodrdinates the player is moving in
        /// collide with a wall. If so, the player doesn't move beyond the wall.
        /// If not, the player moves 1 space in that direction.
        /// Right now the monster is not used, but I will keep it there for now 
        /// in case in the future we need it.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="walls"></param>
        public void move(ConsoleKey d, IList<IList<Obstacles>> walls, int x, int y, IList<Attributes> monster, IList<Item> item,
            Attributes Hero, ConsoleColor BGColor, IList<IList<Item>> sword, IList<Item> singleShots)
        {

            Console.SetCursorPosition(Hero.X, Hero.Y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" ");

           // if (d == Keys.Direction.Down)
           if(d == ConsoleKey.DownArrow)
            {
                Hero.Y += 1;
                Hero.Symbol = "v";
                //If hero is warrior
                if(Game.character.Color.Equals(ConsoleColor.Blue))
                {
                    foreach (IList<Item> el in sword)
                    {
                        foreach (Item sw in el)
                        {
                            sw.Y += 1;
                        }
                    }
                }
                //If hero is mage or hunter
                if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                {
                    foreach (Item el in singleShots)
                    {
                        el.Y += 1;
                    }
                }
                    // Checks collision for down
                    if (collision.checkHeroWall(Hero, walls, x, y) || collision.CheckHeroMon(monster, Hero))
                    {
                        Hero.Y -= 1;
                        //If hero is warrior
                        if (Game.character.Color.Equals(ConsoleColor.Blue))
                        {
                            foreach (IList<Item> el in sword)
                            {
                                foreach (Item sw in el)
                                {
                                    sw.Y -= 1;
                                }
                            }
                        }
                        //If hero is mage or hunter
                        if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                        {
                            foreach (Item el in singleShots)
                            {
                                el.Y -= 1;
                            }
                        }
                    }   
            }
            // if (d == Keys.Direction.Up)
            if (d == ConsoleKey.UpArrow)
            {
                Hero.Y -= 1;
                Hero.Symbol = "^";
                // If hero is warrior
                if (Game.character.Color.Equals(ConsoleColor.Blue))
                {
                    foreach (IList<Item> el in sword)
                    {
                        foreach (Item sw in el)
                        {
                            sw.Y -= 1;
                        }
                    }
                }
                // If hero is mage or hunter
                if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                {
                    foreach (Item el in singleShots)
                    {
                        el.Y -= 1;
                    }
                }
                // Checks Collision
                if (collision.checkHeroWall(Hero, walls, x, y) || collision.CheckHeroMon(monster, Hero))
                {
                    Hero.Y += 1;
                    // if hero is warrior
                    if (Game.character.Color.Equals(ConsoleColor.Blue))
                    {
                        foreach (IList<Item> el in sword)
                        {
                            foreach (Item sw in el)
                            {
                                sw.Y += 1;
                            }
                        }
                    }
                    //if hero is mage or hunter
                    if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                    {
                        foreach (Item el in singleShots)
                        {
                            el.Y += 1;
                        }
                    }
                }
            }

            //if (d == Keys.Direction.Left)
            if (d == ConsoleKey.LeftArrow)
            {
                Hero.X -= 1;
                Hero.Symbol = "<";
                // if hero is warrior
                if (Game.character.Color.Equals(ConsoleColor.Blue))
                {
                    foreach (IList<Item> el in sword)
                    {
                        foreach (Item sw in el)
                        {
                            sw.X -= 1;
                        }
                    }
                }
                // if hero is mage or hunter
                if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                {
                    foreach (Item el in singleShots)
                    {
                        el.X -= 1;
                    }
                }
                if (collision.checkHeroWall(Hero, walls, x, y) || collision.CheckHeroMon(monster, Hero))
                {
                    Hero.X += 1;
                    // if hero is warrior
                    if (Game.character.Color.Equals(ConsoleColor.Blue))
                    {
                        foreach (IList<Item> el in sword)
                        {
                            foreach (Item sw in el)
                            {
                                sw.X += 1;
                            }
                        }
                    }
                    // if hero is mage or hunter
                    if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                    {
                        foreach (Item el in singleShots)
                        {
                            el.X += 1;
                        }
                    }
                }
            }
            //if (d == Keys.Direction.Right)
            if (d == ConsoleKey.RightArrow)
            {
                Hero.X += 1;
                Hero.Symbol = ">";
                // if hero is warrior
                if (Game.character.Color.Equals(ConsoleColor.Blue))
                {
                    foreach (IList<Item> el in sword)
                    {
                        foreach (Item sw in el)
                        {
                            sw.X += 1;
                        }
                    }
                }
                // if hero is mage or hunter
                if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                {
                    foreach (Item el in singleShots)
                    {
                        el.X += 1;
                    }
                }
                if (collision.checkHeroWall(Hero, walls, x, y) || collision.CheckHeroMon(monster, Hero))
                {
                    Hero.X -= 1;
                    //if hero is warrior
                    if (Game.character.Color.Equals(ConsoleColor.Blue))
                    {
                        foreach (IList<Item> el in sword)
                        {
                            foreach (Item sw in el)
                            {
                                sw.X -= 1;
                            }
                        }
                    }
                    // if hero is mage or hunter
                    if (Game.character.Color.Equals(ConsoleColor.Green) || Game.character.Color.Equals(ConsoleColor.Red))
                    {
                        foreach (Item el in singleShots)
                        {
                            el.X -= 1;
                        }
                    }
                }

            }
            if (collision.checkHeroItem(Hero, item))
            {
                Game game = new Game();
                game.PlaySounds("Coin.wav", 75);
                Game.points += 2;
                Display.UpdatePoints();
            }

            DrawHero(Hero, BGColor);
        }

        /// <summary>
        /// Draws the hero in the new location after the User chooses a direction
        /// and passes the collision check.
        /// </summary>
        public void DrawHero(Attributes Hero, ConsoleColor BGColor)
        {
            Console.SetCursorPosition(Hero.X, Hero.Y);
            Console.BackgroundColor = BGColor;
            Console.ForegroundColor = Hero.Color;
            Console.Write(Hero.Symbol);
        }

        /// <summary>
        /// Sword swing action. When the user presses spacebar it loops through the animation
        /// depending on which direction the hero is pointing.
        /// ***May be able to change this to an array to make it smoother
        /// </summary>
        /// <param name="action"></param>
        /// <param name="hero"></param>
        /// <param name="monster"></param>
        public void Action(ConsoleKey action, Attributes hero, IList<Attributes>monster)
        {
            Game game = new Game();
            // Sword user
            if (hero.Symbol.Equals(">") && hero.Color.Equals(ConsoleColor.Blue))
            {
                swingSword(2);
                collision.CheckSwordHit(monster, hero);
                game.PlaySounds("Sword.wav", 90);                
            }
            if (hero.Symbol.Equals("<") && hero.Color.Equals(ConsoleColor.Blue))
            {
                swingSword(1);
                collision.CheckSwordHit(monster, hero);
                game.PlaySounds("Sword.wav", 90);
            }
            if (hero.Symbol.Equals("v") && hero.Color.Equals(ConsoleColor.Blue))
            {
                swingSword(3);
                collision.CheckSwordHit(monster, hero);
                game.PlaySounds("Sword.wav", 90);
            }
            if (hero.Symbol.Equals("^") && hero.Color.Equals(ConsoleColor.Blue))
            {
                swingSword(0);
                collision.CheckSwordHit(monster, hero);
                game.PlaySounds("Sword.wav", 90);
            }

            // Magic user
            if (hero.Symbol.Equals(">") && hero.Color.Equals(ConsoleColor.Green))
            {
                CastMagic(1, hero.Symbol);
                collision.checkMagicHit(1);                
                game.PlaySounds("Wizard.wav", 75);
            }
            if (hero.Symbol.Equals("<") && hero.Color.Equals(ConsoleColor.Green))
            {
                CastMagic(2, hero.Symbol);
                collision.checkMagicHit(2);
                game.PlaySounds("Wizard.wav", 75);
            }
            if (hero.Symbol.Equals("v") && hero.Color.Equals(ConsoleColor.Green))
            {
                CastMagic(3, hero.Symbol);
                collision.checkMagicHit(3);
                game.PlaySounds("Wizard.wav", 75);
            }
            if (hero.Symbol.Equals("^") && hero.Color.Equals(ConsoleColor.Green))
            {
                CastMagic(0, hero.Symbol);
                collision.checkMagicHit(0);
                game.PlaySounds("Wizard.wav", 75);
            }
        }

        public static void swingSword(int index)
        {
            for (int i = 0; i < 3; i++)
            {
                Display.SetCharText(Game.sword[index][i].X, Game.sword[index][i].Y, Game.sword[index][i].Symbol, ConsoleColor.Black, ConsoleColor.White);
                if(Collision.checkWeaponWall(Game.sword[index][i]))
                {
                    Thread.Sleep(50);
                    Display.SetCharText(Game.sword[index][i].X, Game.sword[index][i].Y, Game.sword[index][i].Symbol, ConsoleColor.Gray, ConsoleColor.Gray);
                    break;
                }
                else
                {
                    Thread.Sleep(50);
                    Display.SetCharText(Game.sword[index][i].X, Game.sword[index][i].Y, Game.sword[index][i].Symbol, ConsoleColor.Black, ConsoleColor.Black);
                }
            }
        }

        /// <summary>
        /// Casts a magic thingy of ice in a constant direction
        /// </summary>
        /// <param name="index"></param>
        /// <param name="direction"></param>
        public void CastMagic(int index, string direction)
        {
            
            if(direction.Equals("^"))
            {
                for(int i = 0; i < 5; i++)
                {
                    Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Black, Game.magicList[index].Color);
                    collision.checkMagicHit(index);
                    if (Collision.checkWeaponWall(Game.magicList[index]))
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Gray, ConsoleColor.Gray);
                        Game.magicList[index].X = Game.character.X;
                        Game.magicList[index].Y = Game.character.Y - 1;
                        break;
                    }
                    
                    else
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, " ", ConsoleColor.Black, ConsoleColor.Black);
                        Game.magicList[index].Y -= 1;
                    }
                    
                }
                Game.magicList[index].X = Game.character.X;
                Game.magicList[index].Y = Game.character.Y - 1;
            }

            if (direction.Equals("v"))
            {
                for (int i = 0; i < 5; i++)
                {
                    Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Black, Game.magicList[index].Color);
                    collision.checkMagicHit(index);
                    if (Collision.checkWeaponWall(Game.magicList[index]))
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Gray, ConsoleColor.Gray);
                        Game.magicList[index].X = Game.character.X;
                        Game.magicList[index].Y = Game.character.Y + 1;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, " ", ConsoleColor.Black, ConsoleColor.Black);
                        Game.magicList[index].Y += 1;
                    }
                    
                }
                Game.magicList[index].X = Game.character.X;
                Game.magicList[index].Y = Game.character.Y + 1;
            }

            if (direction.Equals("<"))
            {
                for (int i = 0; i < 8; i++)
                {
                    Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Black, Game.magicList[index].Color);
                    collision.checkMagicHit(index);
                    if (Collision.checkWeaponWall(Game.magicList[index]))
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Gray, ConsoleColor.Gray);
                        Game.magicList[index].X = Game.character.X - 1;
                        Game.magicList[index].Y = Game.character.Y;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, " ", ConsoleColor.Black, ConsoleColor.Black);
                        Game.magicList[index].X -= 1;
                    }
                    
                }
                Game.magicList[index].X = Game.character.X - 1;
                Game.magicList[index].Y = Game.character.Y;
            }

            if (direction.Equals(">"))
            {
                for (int i = 0; i < 8; i++)
                {
                    Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Black, Game.magicList[index].Color);
                    collision.checkMagicHit(index);
                    if (Collision.checkWeaponWall(Game.magicList[index]))
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, Game.magicList[index].Symbol, ConsoleColor.Gray, ConsoleColor.Gray);
                        Game.magicList[index].X = Game.character.X + 1;
                        Game.magicList[index].Y = Game.character.Y;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(50);
                        Display.SetCharText(Game.magicList[index].X, Game.magicList[index].Y, " ", ConsoleColor.Black, ConsoleColor.Black);
                        Game.magicList[index].X += 1;
                    }
                    
                }
                Game.magicList[index].X = Game.character.X + 1;
                Game.magicList[index].Y = Game.character.Y;
            }
        }
        /// <summary>
        /// Checks if player is dead
        /// </summary>
        /// <returns></returns>
        public static bool CheckHeroDead()
        {            
            if (Game.character.HP <= 0)
                return true;
            return false;
        }
        
    }
}
