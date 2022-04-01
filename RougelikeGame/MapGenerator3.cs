using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace RougelikeGame
{
    class MapGenerator3
    {
        public static int width = 100;
        public static int height = 25;
        public int[,] map = new int[width + 1, height + 1];
        public static char character;
        public static int roomMaxCount;
        public int startX = 5;
        public int startY = 5;
        public int enemyNumber = 0;
        public Snake[] enemyArray = new Snake[50];
        public static bool spawnPointSet = false;
        public static int currentEnemyCount;
        public static int enemyCount;
        bool endPointSet = false;

        ConsoleColor Lettercolour;

        public void CreateMap()
        {
            bool clearRoom = false;
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            int number = 0;
            bool noBush = false;
            int bushChance;
            for (y = 0; y <= height; y++)
            {
                for  (x = 0; x <= width; x++)
                {
                    if (x == 0 || x == width-1 || y == 0 || y == height-1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        bushChance = rnd.Next(1, 40);
                        if (bushChance == 1)
                        {
                            map[x, y] = 3;
                        }
                        else
                        {
                            map[x, y] = 0;
                        }
                        if (map[x - 1, y] == 3 || map[x,y-1] == 3)
                        {
                            bushChance = rnd.Next(1, 10);
                            if (bushChance > 6)
                            {
                                map[x, y] = 3;
                            }
                            else
                            {
                                
                            }
                        }
                        
                    }
                    
                }

            }
            DisplayMap();
            SpawnSnakes();
        }


        public void SpawnSnakes()
        {
            Snake enemy = new Snake();
            enemy.spawnX = 10;
            enemy.spawnY = 10;
            enemy.gameRunning = true;
            enemy.Movement(1, map);
        }


        public void DisplayMap()
        {
            int posX;
            int posY;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    posX = x;
                    posY = y;
                    if (map[x, y] == 1)
                    {
                        Lettercolour = ConsoleColor.White;
                        character = '▓';
                    }
                    if (map[x, y] == 0)
                    {
                        character = ' ';
                    }
                    if (map[x, y] == 5)
                    {
                        Lettercolour = ConsoleColor.DarkGreen;
                        character = '█';
                    }
                    if (map[x, y] == 2)
                    {
                        Lettercolour = ConsoleColor.Red;
                        character = 'S';
                    }
                    if(map[x,y] == 3)
                    {
                        character = '#';
                        Lettercolour = ConsoleColor.Green;
                    }
                    Console.SetCursorPosition(posX, posY);
                    Console.ForegroundColor = Lettercolour;
                    Console.Write(character);
                }
            }
        }
        public static void WriteCharacter(char character, int x, int y, ConsoleColor colour)
        {
            try
            {
                if (x >= 0 || y >= 0)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = colour;
                    Console.Write(character);


                }
            }
            catch (Exception)
            {

            }
        }
    }
}
