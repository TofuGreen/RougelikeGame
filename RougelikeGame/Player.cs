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
        public static int prevX;
        public static int prevY;
        public void Movement()
        {
            int x = 0;
            int y = 0;
            Write(character, x, y);
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
                Write(character, x, y);
            }
        }

        public static void Write(char character, int x, int y)
        {
            try
            {
                if(x >= 0 && y >= 0)
                {
                    
                    Console.SetCursorPosition(x, y);
                    Console.Write(character);
                    Console.Write(" ",prevX,prevY);
                }
            }
            catch(Exception) { 
            }
        }

    }
}
