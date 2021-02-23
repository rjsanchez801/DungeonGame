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
    /// Keys represents a enum of user directions to control character movement
    /// </summary>
    class Keys
    {        
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            F
        }

        //Returns key that is read from user input
        public Direction ProcessKey()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.DownArrow:
                    return Direction.Down;
                case ConsoleKey.F:
                    return Direction.F;
                default:
                    return 0;
            }
        }
    }
}
