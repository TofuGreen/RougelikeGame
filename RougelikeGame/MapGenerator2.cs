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
        public static int roomMaxCount;
        public int startX;
        public int startY;
        public static bool spawnPointSet = false;
        bool endPointSet = false;
        ConsoleColor Lettercolour;


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
            //CreatePath();
            CreateRoom();
            DisplayMap();
        }
        public void CreateRoom()
        {
            Random rnd = new Random();
            int currentRoomCount = 0;
            int roomCount = 5;
            int rngX = rnd.Next(0, 99);
            int rngY = rnd.Next(0, 24);
            while (currentRoomCount < roomCount)
            {
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
                            int roomArea = roomSizeX * roomSizeY;
                            int currentRoomArea = 0;
                            for (int i = 0; i <= roomSizeX; i++)
                            {
                                for (int j = 0; j <= roomSizeY; j++)
                                {
                                    if ((x + i < width) && (y + j < height))
                                    {
                                        map[x + i, y + j] = 0;
                                        if (currentRoomCount == roomCount && endPointSet == false)
                                        {
                                            map[x + i, y + j] = 5;
                                            endPointSet = true;
                                        }
                                            
                                            if (spawnPointSet == false)
                                            {
                                                startX = x;
                                                startY = y;
                                                spawnPointSet = true;
                                            }

                                            if (currentRoomArea < roomArea)
                                            {
                                                currentRoomArea++;
                                            }
                                            else
                                            {
                                                currentRoomArea = 0;

                                            }
                                        
                                    }
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
                        Lettercolour = ConsoleColor.White;
                        character = '▓';
                    }
                    if (map[x, y] == 0)
                    {
                        character = ' ';
                    }
                    if(map[x,y] == 5)
                    {
                        Lettercolour = ConsoleColor.Green;
                        character = '█';
                    }
                    WriteCharacter(character, posX, posY, Lettercolour);
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
            int direction = rnd.Next(1, 10);
            while (currentPathLength < pathLengthMax)
            {
                if (startX < width && startY < height)
                {
                    Console.WriteLine(direction);
                    if (direction >= 5)
                    {
                        Console.WriteLine("this way");
                        if (x > 1 && x < width)
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
                        if (y > 1 && y < height)
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
                    Console.WriteLine(startX + " " + startY);
                    direction = rnd.Next(1, 10);

                }
                else
                {
                    map[startX, startY] = 1;
                    startX = rnd.Next(1, width);
                    startY = rnd.Next(1, height);
                    map[startX, startY] = 0;
                    x = startX;
                    y = startY;
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
