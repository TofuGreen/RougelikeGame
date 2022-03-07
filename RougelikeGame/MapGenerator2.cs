using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RougelikeGame
{
    class MapGenerator2
    {
        public static int width = 100;
        public static int height = 25;
        public int[,] map = new int[width + 1, height + 1];
        public static char character;


        



        public void CreateMap()
        {
            bool clearRoom = false;
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            for (y = 0; y <= height; y++)
            {
                for (x = 0; x <= width; x++)
                {
                    map[x, y] = 1;
                }
            }
            CreateRoom();
            CreatePath();
            DisplayMap();
        }
        public void CreateRoom()
        {
            Random rnd = new Random();
            int currentRoomCount = 0;
            int roomCount = 4;
            int rngX = rnd.Next(0, 99);
            int rngY = rnd.Next(0, 24);
                for (int y = 0; y <= height; y++)
                {
                    for (int x = 0; x <= width; x++)
                    {
                        int randomnumber = rnd.Next(1, 250);
                        if (randomnumber == 1 && x > 1 && y > 1 && x < width - 15 && y < height - 5 && map[x, y] == 1 && currentRoomCount < roomCount)
                        {
                            currentRoomCount++;
                            int roomSizeY = rnd.Next(2, 5);
                            int roomSizeX = rnd.Next(2, 15);
                            for (int i = 0; i <= roomSizeX; i++)
                            {
                                for (int j = 0; j <= roomSizeY; j++)
                                {
                                    if ((x + i < width) && (y + j < height))
                                    {
                                        map[x + i, y + j] = 0;

                                    }
                                }

                            }

                        }
                    }

                }



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
                        character = '▓';
                    }
                    if (map[x, y] == 0)
                    {
                        character = ' ';
                    }
                    WriteCharacter(character, posX, posY);
                }
            }
        }

        public void CreatePath()
        {
            Random rnd = new Random();
            int startX = rnd.Next(1, width);
            int startY = rnd.Next(1, height);
            int x = startX;
            int y = startY;
            int pathLengthMax = 50;
            int currentPathLength = 0;
            int pathLength = 0;
            map[startX, startY] = 0;
            while(currentPathLength < pathLengthMax)
            {
                int direction = rnd.Next(1, 2);
                if(direction > 2)
                {
                    if(x > 0 && x < width)
                    {
                        int leftOrRight = rnd.Next(1, 2);
                        if (leftOrRight == 1)
                        {
                            x++;
                            map[x, y] = 0;
                            currentPathLength++;
                        }
                        else
                        {
                            x--;
                            map[x, y] = 0;
                            currentPathLength++;
                        }
                    }
                }
                else
                {
                    if (y > 0 && y < height)
                    {
                        int upOrDown = rnd.Next(1, 2);
                        if (upOrDown == 1)
                        {
                            y++;
                            map[x, y] = 0;
                            currentPathLength++;
                        }
                        else
                        {
                            y--;
                            map[x, y] = 0;
                            currentPathLength++;
                        }
                    }
                }
            }
            

        }
        public static void WriteCharacter(char character, int x, int y)
        {
            try
            {
                if (x >= 0 || y >= 0)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(character);


                }
            }
            catch (Exception)
            {

            }
        }
    }
    
}
