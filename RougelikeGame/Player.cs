﻿using System;
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
        public int[,] currentRoomArray;
        public void Movement()
        {
            int x = 0;
            int y = 0;
            WriteCharacter(character, x, y);
            while(gameRunning == true)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        prevY = y;
                        y--;
                        break;
                    case ConsoleKey.DownArrow:
                        prevY = y;
                        y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        prevX = x;
                        x--;
                        break;
                    case ConsoleKey.RightArrow:
                        prevX = x;
                        x++;
                        break;
                }
                WriteCharacter(character, x, y);
            }
        }

        public static void WriteCharacter(char character, int x, int y)
        {
            try
            {
                if(x >= 0 || y >= 0)
                {
                    
                    Console.SetCursorPosition(x, y);
                    Console.Write(character);


                }
            }
            catch(Exception) { 
            }
        }

    }
}
