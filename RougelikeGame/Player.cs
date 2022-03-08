using System;
using System.Collections.Generic;
using System.Text;

namespace RougelikeGame
{
   class Player
   {
        public bool gameRunning;
        public static string currentpossymbol;
        public static char character = '@';
        static char blank = ' ';
        public static int prevX;
        public static int prevY;
        public static int prevArrayValue;
        char arrayCharacter;
        public MapGenerator2 mapGen;
        bool canMove;
        ConsoleColor colour ;
        public void Movement()
        {
            int x = mapGen.startX;
            int y = mapGen.startY;
            colour = ConsoleColor.White;
            WriteCharacter(character, x, y, colour);
            while(gameRunning == true)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (mapGen.map[x, y-1] != 1)
                        {
                            canMove = true;
                            prevY = y;
                            prevX = x;
                            y--;
                        }
                        else
                        {
                            canMove = false;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (mapGen.map[x, y+1] != 1)
                        {
                            canMove = true;
                            prevY = y;
                            prevX = x;
                            y++;
                        }
                        else
                        {
                            canMove = false;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (mapGen.map[x-1, y] != 1)
                        {
                            canMove = true;
                            prevX = x;
                            prevY = y;
                            x--;
                        }
                        else
                        {
                            canMove = false;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (mapGen.map[x+1, y] == 0)
                        {
                            canMove = true;
                            prevX = x;
                            prevY = y;
                            x++;
                        }
                        else
                        {
                            canMove = false;
                        }
                        break;
                }
                if(mapGen.map[prevX,prevY] == 1)
                {
                    colour = ConsoleColor.White;
                    arrayCharacter = '▓';
                }
                if(mapGen.map[prevX,prevY] == 0)
                {
                    colour = ConsoleColor.White;
                    arrayCharacter = ' ';
                }
                if (mapGen.map[prevX, prevY] == 5)
                {
                    colour = ConsoleColor.Green;
                    arrayCharacter = '█';
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
                if(x >= 0 || y >= 0)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = colour;
                    Console.Write(character);


                }
            }
            catch(Exception) { 
            }
        }

    }
}
