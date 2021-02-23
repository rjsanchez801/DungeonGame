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
    /// Program represents a class that intializes Program setup from Game class.
    /// </summary>
    class Program
    {
       
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Setup();
        }

        
    }
}
