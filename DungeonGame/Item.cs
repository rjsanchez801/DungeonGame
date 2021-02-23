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
    /// Item represents a class to create coin items and character attack moves
    /// </summary>
    class Item
    {
        public int X { get; set; }
        public int X2 { get; set; }
        public int Y { get; set; }
        public string Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        public string Direction { get; set; }
        /// <summary>
        /// Parameters for Item
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y Value</param>
        /// <param name="symbol">Symbol for item shown during game</param>
        /// <param name="color">Color for item</param>
        public Item(int x, int y, string symbol, ConsoleColor color)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
        }

        /// <summary>
        /// Override Constructor to add an extra X value for items needing double values
        /// such as swords and doors.
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="x2">X2 value (to match thickness)</param>
        /// <param name="y">Y value</param>
        /// <param name="symbol">Symbol for item shown during game</param>
        /// <param name="color">Color for item</param>
        public Item(int x, int x2, int y, string symbol, ConsoleColor color)
        {
            X = x;
            X2 = x2;
            Y = y;
            Symbol = symbol;
            Color = color;
        }

    }
}
