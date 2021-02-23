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
    /// Attributes represents a class that sets the parameters for both character and monster
    /// </summary>
    class Attributes
    {
        public string Name { get; }
        public int HP { get; set; }
        public string Weapon { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        public int Counter { get; set; }
        
        /// <summary>
        /// Parameters for character creation
        /// </summary>
        /// <param name="name">Name of character</param>
        /// <param name="hp">Health Points of character</param>
        /// <param name="weapon">Weapon of character</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="color">Color of character</param>
        /// <param name="symbol">Symbol of character in game</param>
        public Attributes (String name, int hp, string weapon, int x, int y, ConsoleColor color, String symbol)
        {
            Name = name;
            HP = hp;
            Weapon = weapon;
            X = x;
            Y = y;
            Color = color;
            Symbol = symbol;
        }

        /// <summary>
        /// Parameters for character creation
        /// </summary>
        /// <param name="name">Name of character</param>
        /// <param name="hp">Health Points of character</param>
        /// <param name="weapon">Weapon of character</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="color">Color of character</param>
        /// <param name="symbol">Symbol of character in game</param>
        /// <param name="counter">Character counter</param>
        public Attributes(String name, int hp, string weapon, int x, int y, ConsoleColor color, String symbol, int counter)
        {
            Name = name;
            HP = hp;
            Weapon = weapon;
            X = x;
            Y = y;
            Color = color;
            Symbol = symbol;
            Counter = counter;
        }
    }
}
