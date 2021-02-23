using System;
using System.Collections.Generic;
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
    /// Monster represents a class that checks monster collisions with walls, whether it is being attacked
    /// and when the monster dies.
    /// </summary>
    class Monster
    {
        Collision collision = new Collision();        

        /// <summary>
        /// Moves the monster after checking collisions for walls, borders, and the hero
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="hero"></param>
        /// <param name="walls"></param>
        /// <param name="borderX"></param>
        /// <param name="borderY"></param>
        /// <param name="BGColor"></param>
        /// <param name="item"></param>
        public void move(IList<Attributes> monster, Attributes hero, IList<IList<Obstacles>> walls, int borderX, int borderY, ConsoleColor BGColor, IList<Item> item)
        {
            for (int i = 0; i < monster.Count; i++)
            {
                if(monster[i].Color.Equals(ConsoleColor.Red))
                {
                    Console.SetCursorPosition(monster[i].X, monster[i].Y);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = BGColor;
                    Console.Write(" ");

                    if (monster[i].X > hero.X)
                    {
                        monster[i].X -= 1;
                        if (collision.checkMonsterWall(monster, walls, borderX, borderY) || collision.CheckHeroMon(monster, hero))
                        {
                            if (collision.CheckHeroMon(monster, hero))
                                monsterAttack(hero);
                            monster[i].X += 1;

                        }
                    }

                    if (monster[i].X < hero.X)
                    {
                        monster[i].X += 1;
                        if (collision.checkMonsterWall(monster, walls, borderX, borderY) || collision.CheckHeroMon(monster, hero))
                        {
                            if (collision.CheckHeroMon(monster, hero))
                                monsterAttack(hero);
                            monster[i].X -= 1;

                        }
                    }

                    if (monster[i].Y < hero.Y)
                    {
                        monster[i].Y += 1;
                        if (collision.checkMonsterWall(monster, walls, borderX, borderY) || collision.CheckHeroMon(monster, hero))
                        {
                            if (collision.CheckHeroMon(monster, hero))
                                monsterAttack(hero);
                            monster[i].Y -= 1;

                        }
                    }

                    if (monster[i].Y > hero.Y)
                    {
                        monster[i].Y -= 1;
                        if (collision.checkMonsterWall(monster, walls, borderX, borderY) || collision.CheckHeroMon(monster, hero))
                        {
                            if (collision.CheckHeroMon(monster, hero))
                                monsterAttack(hero);
                            monster[i].Y += 1;

                        }
                    }
                }
                else
                {
                    monster[i].Counter -= 1;
                    checkMonsterFreeze(i);
                }
            }
        }

        /// <summary>
        /// Draws the monsters on their X-Y coordinatesby using LINQ
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="BGColor"></param>
        public void DrawMonster(IList<Attributes> monster, ConsoleColor BGColor)
        {
            IEnumerable<Attributes> monsterList =
                from m in Game.listOfMonsters
                select m;

            foreach(Attributes m in monsterList)
            {
                Console.SetCursorPosition(m.X, m.Y);
                Console.BackgroundColor = BGColor;
                Console.ForegroundColor = m.Color;
                Console.Write(m.Symbol);
            }
        }

        /// <summary>
        /// When the monster is next to the hero it attacks and causes the 
        /// hero to lose one health.
        /// </summary>
        /// <param name="hero"></param>
        public void monsterAttack(Attributes hero)
        {
            Game game = new Game();
            Console.SetCursorPosition(hero.X, hero.Y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("X");
            hero.HP -= 1;
            game.PlaySounds("PlayerHit.wav", 75);
            Thread.Sleep(50);
            Display.UpdateHP();
        }

        /// <summary>
        /// When a monster dies the user gets 1 point and the monster is
        /// removed from the list of monsters. Points are updated.
        /// For kicks and giggles I also added blood, which can be 
        /// cleaned up if a monster or the hero walks over it.
        /// </summary>
        /// <param name="index"></param>
        public static void monsterDeath(int index)
        {            
            Game.points += 1;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Game.listOfMonsters[index].X, Game.listOfMonsters[index].Y);
            Console.Write(" ");
            Game.listOfMonsters.Remove(Game.listOfMonsters[index]);
            Console.BackgroundColor = ConsoleColor.Black;
            Display.UpdatePoints();
        }

        /// <summary>
        /// Checks if the monster is still frozen by checking the monster counter 
        /// in each monster. If it's zero, monster is unfrozen and starts moving again.
        /// </summary>
        /// <param name="index"></param>
        public static void checkMonsterFreeze(int index)
        {            
            if (Game.listOfMonsters[index].Counter <= 0)
            {
                Game game = new Game();
                game.PlaySounds("MonsterDeath.wav", 75);
                Game.listOfMonsters[index].Color = ConsoleColor.Red;
                Game.listOfMonsters[index].Counter = 10;
            }            
        }
        /// <summary>
        /// Checks if the monster is dead
        /// </summary>
        /// <param name="index"></param>
        public static void checkMonDead(int index)
        {
            if (Game.listOfMonsters[index].HP <= 0)
            {
                monsterDeath(index);
            }            
        }
    }
}
