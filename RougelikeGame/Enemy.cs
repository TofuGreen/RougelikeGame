using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace RougelikeGame
{
   public class Enemy
   {
        public int x;
        public int y;
        public int spawnX;
        public int spawnY;
        int prevX;
        int prevY;
        bool canMove;
        public Player player;
        public MapGenerator3 mapGen;
        public char arrayCharacter;
        public char character;
        public int pathLength;
        int currentPathLength;
        public int direction;
        public int ID;
        public ConsoleColor colour = ConsoleColor.Red;
        Random rnd = new Random();
        public bool inbattle;
        long DelayBetweenEnemyMoves = 200;
        long lastTime;
        public int maxHealth;
        public int health;
        public bool enemyTurn;
        public int maxDamage = 8;
        public bool quitFight;
        public int quitCount = 5;
        public void Initialise()
        {
            health = maxHealth;
            canMove = false;
            inbattle = false;
            quitFight = false;
            character = 'S';
            lastTime = 0;
            WriteCharacter(character, x, y, colour);
        }
        public void Movement(int[,] map)
        {
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            WriteCharacter(character, x, y, colour);
            if (inbattle == false && health > 0)
            {
                if (currentTime > (lastTime + DelayBetweenEnemyMoves))
                {
                    if (health < maxHealth)
                    {
                        health++;
                    }
                    if (currentPathLength < pathLength)
                    {
                        switch (direction)
                        {
                            case 1:

                                if (map[x, y - 1] == 0 || map[x, y - 1] == 3)
                                {
                                    canMove = true;
                                    prevY = y;
                                    prevX = x;
                                    y--;
                                }
                                else
                                {
                                    canMove = false;
                                    direction = rnd.Next(1, 5);
                                }
                                break;

                            case 2:
                                if (map[x, y + 1] == 0 || map[x, y + 1] == 3)
                                {
                                    canMove = true;
                                    prevY = y;
                                    prevX = x;
                                    y++;
                                }
                                else
                                {
                                    canMove = false;
                                    direction = rnd.Next(1, 5);
                                }
                                break;

                            case 3:

                                if (map[x - 1, y] == 0 || map[x - 1, y] == 3)
                                {
                                    canMove = true;
                                    prevY = y;
                                    prevX = x;
                                    x--;
                                }
                                else
                                {
                                    canMove = false;
                                    direction = rnd.Next(1, 5);
                                }
                                break;

                            case 4:

                                if (map[x + 1, y] == 0 || map[x + 1, y] == 3)
                                {
                                    canMove = true;
                                    prevY = y;
                                    prevX = x;
                                    x++;
                                }
                                else
                                {
                                    canMove = false;
                                    direction = rnd.Next(1, 5);
                                }
                                break;
                        }
                        currentPathLength++;
                        if(quitFight == true)
                        {
                            if (quitCount > 0)
                            {
                                quitCount--;
                            }
                            else
                            {
                                quitCount = 5;
                                quitFight = false;
                            }
                        }

                        if (map[prevX, prevY] == 1)
                        {
                            colour = ConsoleColor.White;
                            arrayCharacter = '▓';
                        }
                        if (map[prevX, prevY] == 0)
                        {
                            colour = ConsoleColor.White;
                            arrayCharacter = ' ';
                        }
                        if (map[prevX, prevY] == 5)
                        {
                            colour = ConsoleColor.Green;
                            arrayCharacter = '█';
                        }
                        if (map[prevX, prevY] == 3)
                        {
                            colour = ConsoleColor.Green;
                            arrayCharacter = '#';
                        }
                        if(map[prevX,prevY] == 10)
                        {
                            colour = ConsoleColor.DarkYellow;
                            arrayCharacter = (char)3;
                        }
                        if (canMove == true)
                        {
                            WriteCharacter(arrayCharacter, prevX, prevY, colour);
                            if (mapGen.level == 3)
                            {
                                colour = ConsoleColor.DarkRed;
                            }
                            else
                            {
                                colour = ConsoleColor.Red;
                            }
                            WriteCharacter(character, x, y, colour);
                        }
                        
                    }
                    else
                    {
                        pathLength = rnd.Next(3, 15);
                        direction = rnd.Next(1, 5);
                        currentPathLength = 0;
                    }
                    lastTime = currentTime;
                }
                Damage();
            }
        }
        public void DisplayEnemyStats()
        {
            Console.SetCursorPosition(101, 23);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Enemy Stats:");
            Console.SetCursorPosition(101, 24);
            Console.Write("                                ");
            Console.SetCursorPosition(101, 24);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Health: " + health);
        }
        public void Damage()
        {
            
            if (player.fighting == false && quitFight == false)
            {
                if ((x - 1 == player.x || x+1 == player.x || x== player.x) && (y - 1 == player.y || y + 1 == player.y || y == player.y))
                {
                    player.fighting = true;
                    player.fightingEnemy = this;
                    inbattle = true;
                    Console.SetCursorPosition(0, 25);
                    Console.WriteLine("You are engaging an enemy!");
                    DisplayEnemyStats();
                }
            }
            if(inbattle == true && enemyTurn == true)
            {
                DisplayEnemyStats();
                int damage = rnd.Next(1, (1 + (mapGen.difficulty)));
                if(damage > 8)
                {
                    damage = 8;
                }
                player.health -= damage;
                if(player.health <= 0)
                {
                    Console.Clear();
                    Program.DeathScreen();
                }
                else
                {
                    Console.SetCursorPosition(0, 25);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 25);
                    Console.WriteLine("The enemy hit you for " + damage + " damage!");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(0, 25);
                    Console.WriteLine("                                              ");
                    Console.SetCursorPosition(101, 1);
                    Console.WriteLine("                        ");
                    Console.SetCursorPosition(player.x, player.y);
                    enemyTurn = false;
                    player.playerTurn = true;
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
        public void SaveEnemy()
        {
            Stream saveFileStream = File.Create(Program.filePath + "save1Enemy"+ ID +".txt");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, x);
            serializer.Serialize(saveFileStream, y);
            serializer.Serialize(saveFileStream, health);
            serializer.Serialize(saveFileStream, currentPathLength);
            serializer.Serialize(saveFileStream, colour);
            serializer.Serialize(saveFileStream, character);
            saveFileStream.Close();
        }
        public void LoadEnemy()
        {
            Stream openFileStream = File.OpenRead(Program.filePath + "save1Enemy" + ID + ".txt");
            BinaryFormatter deserializer = new BinaryFormatter();
            x = (int)deserializer.Deserialize(openFileStream);
            y = (int)deserializer.Deserialize(openFileStream);
            health = (int)deserializer.Deserialize(openFileStream);
            currentPathLength = (int)deserializer.Deserialize(openFileStream);
            colour = (ConsoleColor)deserializer.Deserialize(openFileStream);
            character = (char)deserializer.Deserialize(openFileStream);
            openFileStream.Close();
        }
    }
}
