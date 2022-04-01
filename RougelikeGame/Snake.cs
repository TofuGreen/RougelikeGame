using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RougelikeGame
{
    class Snake
    {

        public int x;
        public int y;
        public int spawnX;
        public int spawnY;
        int prevX;
        int prevY;
        bool canMove;
        public bool gameRunning;
        public char arrayCharacter;
        public char character = 'S';
        ConsoleColor colour = ConsoleColor.Red;
        public void Movement(int direction, int[,] mapGen)
        {
            x = spawnX;
            y = spawnY;
            WriteCharacter(character, x, y, colour);
            while (gameRunning == true)
            {
                if (direction == 1)
                {
                    if (mapGen[x, y - 1] != 1)
                    {
                        while (mapGen[x, y - 1] != 1)
                        {
                            canMove = true;
                            prevY = y;
                            prevX = x;
                            y--;
                            WriteCharacter(arrayCharacter, prevX, prevY, colour);
                            WriteCharacter(character, x, y, colour);
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, 26);
                            Console.Write("Moving up");
                        }
                    }
                    else if (mapGen[x, y + 1] != 1)
                    {
                        while (mapGen[x, y + 1] != 1)
                        {
                            canMove = true;
                            prevY = y;
                            prevX = x;
                            y++;
                            WriteCharacter(arrayCharacter, prevX, prevY, colour);
                            WriteCharacter(character, x, y, colour);
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, 26);
                            Console.Write("Moving down");
                        }
                    }
                    else
                    {
                        direction = 2;
                    }
                    
                }
                else
                {
                    if (mapGen[x - 1, y] != 1)
                    {
                        while (mapGen[x - 1, y] != 1)
                        {
                            canMove = true;
                            prevY = y;
                            prevX = x;
                            
                            x--;
                            WriteCharacter(arrayCharacter, prevX, prevY, colour);
                            WriteCharacter(character, x, y, colour);
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, 26);
                            Console.Write("Moving left");
                        }
                    }
                    else if (mapGen[x + 1, y] != 1)
                    {
                        while (mapGen[x + 1, y] != 1)
                        {
                            canMove = true;
                            prevY = y;
                            prevX = x;
                            if(mapGen[x,y] == 0)
                            {
                                arrayCharacter = ' ';
                                colour = ConsoleColor.White;
                            }
                            if(mapGen[x,y] == 3)
                            {
                                arrayCharacter = '#';
                                colour = ConsoleColor.Green;
                            }
                            x++;
                            WriteCharacter(arrayCharacter, prevX, prevY, colour);
                            WriteCharacter(character, x, y, colour);
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, 26);
                            Console.Write("Moving right");
                        }
                    }
                    else
                    {
                        direction = 1;
                    }
                }
                if (mapGen[prevX, prevY] != 1)
                {
                    colour = ConsoleColor.Red;
                    arrayCharacter = ' ';
                }
                if (canMove == true)
                {
                    WriteCharacter(arrayCharacter, prevX, prevY, colour);
                    WriteCharacter(character, x, y, colour);
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
