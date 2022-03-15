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
        public static int x;
        public static int y;
        public static int prevX;
        public static int prevY;
        public static int prevArrayValue;
        char arrayCharacter;
        public MapGenerator2 mapGen;
        bool canMove;
        ConsoleColor colour ;

        //Inventory and stats
        public int health = 10;
        public string weapon = "Fists";
        public int coins = 0;
        public int healthItems = 0;
        public void Movement()
        {
            
            x = mapGen.startX;
            y = mapGen.startY;
            colour = ConsoleColor.White;
            WriteCharacter(character, x, y, colour);
            while (gameRunning == true)
                {
                DisplayStats();
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow) {
                    if (mapGen.map[x, y - 1] == 0)
                        {
                        canMove = true;
                        prevY = y;
                        prevX = x;
                        y--;
                        Interaction();
                        }
                    else
                        {
                        canMove = false;
                        }
                    }

                if (key == ConsoleKey.DownArrow) {
                    if (mapGen.map[x, y + 1] == 0)
                        {
                        canMove = true;
                        prevY = y;
                        prevX = x;
                        y++;
                        Interaction();
                    }
                    else
                        {
                        canMove = false;
                        }
                    }
                if (key == ConsoleKey.LeftArrow) {
                    if (mapGen.map[x - 1, y] == 0)
                        {
                        canMove = true;
                        prevX = x;
                        prevY = y;
                        x--;
                        Interaction();
                    }
                    else
                        {
                        canMove = false;
                        }
                }
                if(key == ConsoleKey.RightArrow){
                    if (mapGen.map[x + 1, y] == 0)
                        {
                        canMove = true;
                        prevX = x;
                        prevY = y;
                        x++;
                        Interaction();
                    }
                    else
                        {
                        canMove = false;
                        }
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
        public void DisplayStats()
        {
            Console.SetCursorPosition(0, 25);
            Console.Write("Health:" + health + " weapon:" + weapon + " health potions:" + healthItems + " coins:" + coins);
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
        public void Interaction()
        {
            if (mapGen.map[x+1, y] == 2 || mapGen.map[x-1,y] == 2 || mapGen.map[x,y+1] == 2|| mapGen.map[x,y-1] == 2)
            {
                Console.SetCursorPosition(0,26);
                Console.WriteLine("Snake");

            }
            else
            {
                Console.SetCursorPosition(0, 26);
                Console.Write("                           ");
            }
        }

   }
}
