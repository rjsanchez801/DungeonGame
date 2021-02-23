using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @Author Trevor Davis, Lucas Montoya, Ramon Sanchez
/// </summary>
namespace DungeonGame
{
    /// <summary>
    /// Collision represents a class that checks whether objects in the game are colliding with each other    
    /// </summary>
    class Collision
    {
        /// <summary>
        /// Checks whether or not the hero is touching a border or wall.
        /// </summary>
        /// <param name="heroX"></param>
        /// <param name="heroY"></param>
        /// <param name="walls"></param>
        /// <returns></returns>
        public bool checkHeroWall(Attributes hero, IList<IList<Obstacles>> walls, int borderX, int borderY)
        {
            // Checks if Hero is touching a wall (not border)
            for (int i = 0; i < walls.Count; i++)
            {
                for (int j = 0; j < walls[i].Count; j++)
                    if (hero.X.Equals(walls[i][j].X) && hero.Y.Equals(walls[i][j].Y) || hero.X.Equals(walls[i][j].X2) && hero.Y.Equals(walls[i][j].Y))
                        return true;
            }
            // Checks Top border
            for (int i = 0; i < (borderX + 5); i++)
            {
                if (hero.X.Equals(i) && hero.Y.Equals(5))
                    return true;
            }
            // Checks Bottom border
            for (int i = 0; i < (borderX + 5); i++)
            {
                if (hero.X.Equals(i) && hero.Y.Equals(borderY + 4))
                    return true;
            }

            // Checks Left border
            for (int i = 0; i < (borderY + 5); i++)
            {
                if (hero.X.Equals(5) && hero.Y.Equals(i))
                    return true;
            }

            // Checks Right border
            for (int i = 0; i < (borderY + 5); i++)
            {
                if (hero.X.Equals(borderX + 5) && hero.Y.Equals(i))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a monster and hero are on the same X Y position. 
        /// If so it returns true. This will start a battle.
        /// At the moment it just returns true which takes 1 life away from the hero.
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="heroX"></param>
        /// <param name="heroY"></param>
        /// <returns></returns>
        public bool CheckHeroMon(IList<Attributes> monster, Attributes hero)
        {
            for (int i = 0; i < monster.Count; i++)
            {
                if (hero.X.Equals(monster[i].X) && hero.Y.Equals(monster[i].Y))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the monster is at the same position a the wall.
        /// If the monster is at the same position as a wall, the monster can't pass.
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="heroX"></param>
        /// <param name="heroY"></param>
        /// <param name="walls"></param>
        /// <param name="borderX"></param>
        /// <param name="borderY"></param>
        /// <returns></returns>
        public bool checkMonsterWall(IList<Attributes> monster, IList<IList<Obstacles>> walls, int borderX, int borderY)
        {
            // Checks Top Border
            for (int i = 0; i < (borderX + 5); i++)
            {
                for (int j = 0; j < monster.Count; j++)
                    if (monster[j].X.Equals(i) && monster[j].Y.Equals(5))
                        return true;
            }

            // Checks Bottom Border
            for (int i = 0; i < (borderX + 5); i++)
            {
                for (int j = 0; j < monster.Count; j++)
                    if (monster[j].X.Equals(i) && monster[j].Y.Equals(borderY + 4))
                        return true;
            }

            // Checks Left border
            for (int i = 0; i < (borderY + 5); i++)
            {
                for (int j = 0; j < monster.Count; j++)
                    if (monster[j].X.Equals(5) && monster[j].Y.Equals(i))
                        return true;
            }

            // Checks Right border
            for (int i = 0; i < (borderY + 5); i++)
            {
                for (int j = 0; j < monster.Count; j++)
                    if (monster[j].X.Equals(borderX + 5) && monster[j].Y.Equals(i))
                        return true;
            }

            // Checks if a monster is touching another monster
            for (int i = 0; i < monster.Count; i++)
            {
                for (int j = 0; j < monster.Count - 1; j++)
                {
                    if (i != j)
                    {
                        if (monster[i].X.Equals(monster[j].X) && monster[i].Y.Equals(monster[j].Y))
                            return true;
                    }
                }
            }

            // Checks if monster is touching a wall (not border)
            for (int i = 0; i < walls.Count; i++)
            {
                for (int j = 0; j < walls[i].Count; j++)
                {
                    for (int k = 0; k < monster.Count; k++)
                    {
                        // Changed this TODO
                        if (monster[k].X.Equals(walls[i][j].X) && monster[k].Y.Equals(walls[i][j].Y) 
                            || monster[k].X.Equals(walls[i][j].X2) && monster[k].Y.Equals(walls[i][j].Y))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the walls are touching other walls before it actually 
        /// saves or prints the wall. This also checks to make sure that each 
        /// wall has an opening to allow monsters or players to get through.
        /// </summary>
        /// <param name="walls"></param>
        /// <param name="wallX"></param>
        /// <param name="wallY"></param>
        /// <returns></returns>
        public bool checkWall(IList<IList<Obstacles>> walls, int wallX, int wallY)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                for (int j = 0; j < walls[i].Count; j++)
                {
                    if (walls[i][j].X.Equals(wallX) && walls[i][j].Y.Equals(wallY))
                        return true;
                    if (walls[i][j].X2.Equals(wallX) && walls[i][j].Y.Equals(wallY))
                        return true;
                    if ((walls[i][j].X.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY + 1)) || (walls[i][j].X.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY - 1)))
                        return true;
                    if ((walls[i][j].X.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY + 1)) || (walls[i][j].X.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY - 1)))
                        return true;
                    if ((walls[i][j].X.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY + 1)) || (walls[i][j].X.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY + 1)))
                        return true;
                    if ((walls[i][j].X.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY - 1)) || (walls[i][j].X.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY - 1)))
                        return true;
                    if ((walls[i][j].X2.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY - 1)) || (walls[i][j].X2.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY + 1)))
                        return true;
                    if ((walls[i][j].X2.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY + 1)) || (walls[i][j].X2.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY - 1)))
                        return true;
                    if ((walls[i][j].X2.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY + 1)) || (walls[i][j].X2.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY + 1)))
                        return true;
                    if ((walls[i][j].X2.Equals(wallX - 1) && walls[i][j].Y.Equals(wallY - 1)) || (walls[i][j].X2.Equals(wallX + 1) && walls[i][j].Y.Equals(wallY - 1)))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks wether or not the monster is within the range of the sword as it swings.
        /// If so, it damages the monster and shoves it back. Though this might change to a 
        /// boolean soon.
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="hero"></param>
        public void CheckSwordHit(IList<Attributes> monster, Attributes hero)
        {
            if (hero.Symbol.Equals(">"))
            {
                for (int i = 0; i < monster.Count; i++)
                {
                    if (monster[i].X.Equals(hero.X + 1) && monster[i].Y.Equals(hero.Y - 1) 
                        || monster[i].X.Equals(hero.X + 1) && monster[i].Y.Equals(hero.Y)
                        || monster[i].X.Equals(hero.X + 2) && monster[i].Y.Equals(hero.Y)
                        || monster[i].X.Equals(hero.X + 1) && monster[i].Y.Equals(hero.Y + 1))
                    {
                        monster[i].HP -= 5;
                        for (int j = 0; j < 5; j++)
                        {
                            monster[i].X += 1;
                            if(checkMonsterWall(monster, Game.allWalls, Game.gridXLength, Game.gridYHeight))
                            {
                                monster[i].X -= 1;
                                j = 4;
                            }
                        }
                        Monster.checkMonDead(i);
                    }
                }
            }
            if (hero.Symbol.Equals("^"))
            {
                for (int i = 0; i < monster.Count; i++)
                {
                    if (monster[i].X.Equals(hero.X + 1) && monster[i].Y.Equals(hero.Y - 1) 
                        || monster[i].X.Equals(hero.X) && monster[i].Y.Equals(hero.Y - 1) 
                        || monster[i].X.Equals(hero.X - 1) && monster[i].Y.Equals(hero.Y - 1))
                    {
                        monster[i].HP -= 5;
                        for (int j = 0; j < 5; j++)
                        {
                            monster[i].Y -= 1;
                            if (checkMonsterWall(monster, Game.allWalls, Game.gridXLength, Game.gridYHeight))
                            {
                                monster[i].Y += 1;
                                j = 4;
                            }
                        }

                        Monster.checkMonDead(i);
                    }
                }
            }
            if (hero.Symbol.Equals("<"))
            {
                for (int i = 0; i < monster.Count; i++)
                {
                    if (monster[i].X.Equals(hero.X - 1) && monster[i].Y.Equals(hero.Y - 1) 
                        || monster[i].X.Equals(hero.X - 1) && monster[i].Y.Equals(hero.Y)
                        || monster[i].X.Equals(hero.X - 2) && monster[i].Y.Equals(hero.Y)
                        || monster[i].X.Equals(hero.X - 1) && monster[i].Y.Equals(hero.Y + 1))
                    {
                        monster[i].HP -= 5;
                        for (int j = 0; j < 5; j++)
                        {
                            monster[i].X -= 1;
                            if (checkMonsterWall(monster, Game.allWalls, Game.gridXLength, Game.gridYHeight))
                            {
                                monster[i].X += 1;
                                j = 4;
                            }
                        }
                        Monster.checkMonDead(i);
                    }
                }
            }
            if (hero.Symbol.Equals("v"))
            {
                for (int i = 0; i < monster.Count; i++)
                {
                    if (monster[i].X.Equals(hero.X + 1) && monster[i].Y.Equals(hero.Y + 1) 
                        || monster[i].X.Equals(hero.X) && monster[i].Y.Equals(hero.Y + 1) 
                        || monster[i].X.Equals(hero.X - 1) && monster[i].Y.Equals(hero.Y + 1))
                    {
                        monster[i].HP -= 5;
                        for (int j = 0; j < 5; j++)
                        {
                            monster[i].Y += 1;
                            if (checkMonsterWall(monster, Game.allWalls, Game.gridXLength, Game.gridYHeight))
                            {
                                monster[i].Y -= 1;
                                j = 4;
                            }
                        }
                        Monster.checkMonDead(i);
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether or not the hero is touching a coin
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool checkHeroItem(Attributes hero, IList<Item> item)
        {
            for (int i = 0; i < item.Count; i++)
            {
                if (hero.X.Equals(item[i].X) && hero.Y.Equals(item[i].Y))
                {
                    item.Remove(item[i]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether or not a monster is touching a coin
        /// </summary>
        /// <param name="item"></param>
        /// <param name="mon"></param>
        /// <returns></returns>
        public bool checkMonItem(IList<Item> item, IList<Attributes> mon)
        {
            for (int i = 0; i < item.Count; i++)
            {
                for (int j = 0; j < mon.Count; j++)
                {
                    if (mon[j].X.Equals(item[i].X) && mon[j].Y.Equals(item[i].Y))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the hero touches a door
        /// </summary>
        /// <param name="door"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public bool checkHeroDoor(IList<Item> door, Attributes hero)
        {
            for (int i = 0; i < door.Count; i++)
            {
                if (door[i].X.Equals(hero.X) && door[i].Y.Equals(hero.Y) || door[i].X2.Equals(hero.X) && door[i].Y.Equals(hero.Y))
                {
                    Game game = new Game();
                    game.PlaySounds("Stairs.wav", 75);
                    return true;
                }
            } 
            return false;
        }

        /// <summary>
        /// Checks if the sword hits a wall or border
        /// </summary>
        /// <param name="sword"></param>
        /// <returns></returns>
        public static bool checkWeaponWall(Item sword)
        {
            for (int i = 0; i < Game.allWalls.Count; i++)
            {
                for (int j = 0; j < Game.allWalls[i].Count; j++)
                {
                    if (sword.X.Equals(Game.allWalls[i][j].X) && sword.Y.Equals(Game.allWalls[i][j].Y) 
                        || sword.X.Equals(Game.allWalls[i][j].X2) && sword.Y.Equals(Game.allWalls[i][j].Y)
                        || sword.X2.Equals(Game.allWalls[i][j].X) && sword.Y.Equals(Game.allWalls[i][j].Y)
                        || sword.X2.Equals(Game.allWalls[i][j].X2) && sword.Y.Equals(Game.allWalls[i][j].Y))
                        return true;
                }
            }

            for(int i = 0; i < (Game.gridXLength + 5); i++)
            {
                if (sword.X.Equals(i) && sword.Y.Equals(5)) // I don't like hardcoding 5
                    return true;
            }

            // Checks Bottom Border
            for (int i = 0; i < (Game.gridXLength + 5); i++)
            {
                if (sword.X.Equals(i) && sword.Y.Equals(Game.gridYHeight + 4))
                    return true;
            }

            // Checks Left border
            for (int i = 0; i < (Game.gridYHeight + 5); i++)
            {
                if (sword.X.Equals(5) && sword.Y.Equals(i))
                    return true;
            }

            // Checks Right border
            for (int i = 0; i < (Game.gridYHeight + 5); i++)
            {
                if (sword.X.Equals(Game.gridXLength + 5) && sword.Y.Equals(i))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether or not the monster is hit by the magic.
        /// If so it decreases Monster HP by 1. However, I may want to change this
        /// so the monsters never get hurt, just frozen.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool checkMagicHit(int index)
        {
            for(int i = 0; i < Game.listOfMonsters.Count; i++)
            {
                if(Game.magicList[index].X.Equals(Game.listOfMonsters[i].X) && Game.magicList[index].Y.Equals(Game.listOfMonsters[i].Y))
                {
                    Game.listOfMonsters[i].Color = ConsoleColor.Cyan;
                    Game.listOfMonsters[i].HP -= 1;
                    Monster.checkMonDead(i);
                    return true;
                }
            }
            return false;
        }

    }

}
