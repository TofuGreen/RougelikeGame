using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace RougelikeGame
{
    public class MapGenerator3
    {
        public static int width = 100;
        public static int height = 25;
        public int[,] map = new int[width + 1, height + 1];
        public int level;
        public int difficulty = 0;
        public static char character;
        public int startX = 5;
        public int startY = 5;
        public int enemyNumber = 0;
        public Enemy[] enemyArray;
        public Player capyPlayer;
        public Capybara capy;
        public static bool spawnPointSet = false;
        public bool loading;
        ConsoleColor Lettercolour;

        
        public void CreateMap()//Builds a new map and sets what level type it is
        {
            Random rnd = new Random();
            if(difficulty % 5 == 0 && difficulty != 0)
            {
                    level = 3;
            }
            else
            {
                int levelChance = rnd.Next(1, 100);
                if(levelChance >= 1 && levelChance <= 60)
                {
                    level = 1;
                }
                else
                {
                    level = 2;
                }
            }
            int x;
            int y;
            int currentCapy = 0;
            int bushChance;
            int capyChance;
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
                        bushChance = rnd.Next(1, 40+(difficulty*2));
                        if (bushChance == 1)
                        {
                            map[x, y] = 3;
                        }
                        else
                        {
                            map[x, y] = 0;
                        }
                        if (map[x - 1, y] == 3 || map[x, y - 1] == 3)
                        {
                            bushChance = rnd.Next(1, 10);
                            if (bushChance > 6)
                            {
                                map[x, y] = 3;
                            }
                        }
                    }
                    
                }

            }
            while (currentCapy < 1)
            {
                for (y = 0; y <= height; y++)
                {
                    for (x = 0; x <= width; x++)
                    {
                        if (currentCapy < 1)
                        {
                            if (map[x, y] == 0)
                            {
                                capyChance = rnd.Next(1, 50);
                                if (capyChance == 1)
                                {
                                    map[x, y] = 10;
                                    SpawnCapybara();
                                    currentCapy++;
                                }
                            }
                        }
                    }
                }
            }
            DisplayMap();
        }
        
        
        public void ClearRoom()//Removes the current map
        {

            for (int y = 0; y <= height; y++)
            {
                for (int x = 0; x <= width; x++)
                {
                    map[x, y] = 0;
                }
            }
        }
        
        
        public void SpawnEnemys()//Spawns in enemies amount varies depending on difficulty. There values are also initialised in this method
        {
            Random rnd = new Random();
            if (level != 3)
            {
                enemyArray = new Enemy[5 + (difficulty * 2)];
                for (int i = 0; i < 5 + (difficulty * 2); i++)
                {
                    if (enemyArray[i] != null)
                    {
                        Array.Clear(enemyArray, i, i);
                    }
                    enemyArray[i] = new Enemy();
                    enemyArray[i].maxHealth = 3 + (difficulty * 2);
                    if (loading == false)
                    {

                        enemyArray[i].x = rnd.Next(5, 85);
                        enemyArray[i].y = rnd.Next(5, 23);
                    }          
                    enemyArray[i].mapGen = this;
                    enemyArray[i].direction = rnd.Next(1, 5);
                    enemyArray[i].pathLength = rnd.Next(3, 15);
                    enemyArray[i].ID = i;
                    enemyArray[i].player = capyPlayer;
                    enemyArray[i].Initialise();
                }
            }
            else
            {
                enemyArray = new Enemy[1];
                for (int i = 0; i < 1; i++)
                {
                    if (enemyArray[i] != null)
                    {
                        Array.Clear(enemyArray, i, i);
                    }
                    enemyArray[i] = new Enemy();
                    enemyArray[i].maxHealth = 10 + (difficulty * 2);
                    if (loading == false)
                    {

                        enemyArray[i].x = rnd.Next(5, 85);
                        enemyArray[i].y = rnd.Next(5, 23);
                    }
                    enemyArray[i].mapGen = this;
                    enemyArray[i].direction = rnd.Next(1, 5);
                    enemyArray[i].pathLength = rnd.Next(3, 15);
                    enemyArray[i].ID = i;
                    enemyArray[i].player = capyPlayer;
                    enemyArray[i].Initialise();

                }
            }
        }
        
        
        public void SpawnCapybara()//Spawns in the capybara quest giver and provides its variables with data
        {
            capy = new Capybara();
            Program.capy = capy;
            capy.level = level;
            capy.difficulty = difficulty;
            capyPlayer.capy = capy;
            capy.player = capyPlayer;
            capy.map = this;
            capy.changingLevel = false;
            capy.MissionText();
        }

        
        public void DisplayMap()//Displays the map array on the screen
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
                    if(map[x,y] == 10)
                    {
                        character = (char)2;
                        Lettercolour = ConsoleColor.DarkYellow;
                    }
                    Console.SetCursorPosition(posX, posY);
                    Console.ForegroundColor = Lettercolour;
                    Console.Write(character);
                }
            }
        }
    }
}
