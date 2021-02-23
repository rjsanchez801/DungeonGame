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
    /// Obstacles represents a class that sets up wall objects
    /// </summary>
    class Obstacles
    {
        public int X { get; set; }
        public int X2 { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public string Size{get; set;}
        
        /// <summary>       
        /// Parameters for Obstacle object
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="x2">X2 Coordinate (To make thickness equal)</param>
        /// <param name="y">Y Coordiante</param>
        /// <param name="color">Obstacle Color </param>
        public Obstacles(int x,int x2, int y, ConsoleColor color)
        {
            X = x;
            X2 = x2;
            Y = y;
            Color = color;
        }
    }
}
