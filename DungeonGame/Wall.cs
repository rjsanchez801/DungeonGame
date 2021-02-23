using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    class Wall
    {
        Obstacles[] wall = new Obstacles[4];

        public Wall(int x, int y, ConsoleColor color)
        {
            for(int i = 0; i < wall.Length; i++)
            {
                wall[i] = new Obstacles(x, y + i, color);
            }
        }

        public void drawWalls()
        {
            for (int i = 0; i < wall.Length; i++)
            {
                Console.SetCursorPosition(wall[i].X, wall[i].Y);
                Console.BackgroundColor = wall[i].Color;
                Console.Write(" ");

            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public int getWallX(int index)
        {
            return wall[index].X;
        }

        public int getWallY(int index)
        {
            return wall[index].Y;
        }

    }
}
