using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading;
namespace RougelikeGame
{
   public class Player
   { 
        public static string currentpossymbol;
        public static char character = '@';
        public int x;
        public int y;
        public static int prevX;
        public static int prevY;
        public static int prevArrayValue;
        public int killedEnemies;
        char arrayCharacter;
        public MapGenerator3 mapGen;
        public Capybara capy;
        public Enemy fightingEnemy;
        bool canMove;
        ConsoleColor colour ;
        public bool fighting;
        public bool engaged;
        public bool playerTurn;
        Random rnd = new Random();


        //Inventory and stats
        public int maxHealth;
        public int health;
        public string weapon = "Fists";
        public int weaponDamage;
        public int coins;
        public int coinAmount;
        public int healthItems;
        public int capyCoins;
        public bool hasHealthUpgrade;
        public bool hasSword;
        public bool hasAxe;
        public void InitialiseObjects()//Sets up default values
        {
            capy = mapGen.capy;
            colour = ConsoleColor.White;
            fighting = false;
            playerTurn = true;
            health = maxHealth;
            capyCoins = 0;
            healthItems = 0;
            coins = 0;
        }

        public void Movement()//Controls player movement along with other things such as changing weapon and pausing/saving
        {

            WriteCharacter(character, x, y, colour);
            DisplayStats();
            if (fighting == false && engaged == false)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (mapGen.map[x, y - 1] == 0 || mapGen.map[x, y - 1] == 3)
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
                            break;


                        case ConsoleKey.DownArrow:
                            
                                if (mapGen.map[x, y + 1] == 0 || mapGen.map[x, y + 1] == 3)
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
                            break;
                        case ConsoleKey.LeftArrow:
                            
                                if (mapGen.map[x - 1, y] == 0 || mapGen.map[x - 1, y] == 3)
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
                            break;
                        case ConsoleKey.RightArrow:
                            
                                if (mapGen.map[x + 1, y] == 0 || mapGen.map[x + 1, y] == 3)
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
                            break;
                        case ConsoleKey.Escape:
                            Quit();
                            break;
                        case ConsoleKey.D1:
                            if(weapon != "Fists")
                            {
                                if(weapon == "Sword")
                                {
                                    Console.SetCursorPosition(101, 9);
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write("Sword");
                                }
                                if(weapon == "Battle Axe")
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.SetCursorPosition(101, 10);
                                    Console.Write("Battle Axe");
                                }
                                weapon = "Fists";
                                Console.SetCursorPosition(101, 2);
                                Console.Write("                                     ");
                                Console.SetCursorPosition(101, 2);
                                Console.Write("Weapon: " + weapon);
                            }
                            break;
                        case ConsoleKey.D2:
                            if(weapon != "Sword" && hasSword == true )
                            {
                                if(weapon == "Battle Axe")
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.SetCursorPosition(101, 10);
                                    Console.Write("Battle Axe");
                                }
                                weapon = "Sword";
                                Console.SetCursorPosition(101, 2);
                                Console.Write("                                     ");
                                Console.SetCursorPosition(101, 2);
                                Console.Write("Weapon: " + weapon);
                                Console.SetCursorPosition(101, 9);
                                Console.Write("                                     ");
                            }
                            break;
                        case ConsoleKey.D3:
                            if(weapon != "Battle Axe" && hasAxe == true)
                            {
                                if(weapon == "Sword")
                                {
                                    Console.SetCursorPosition(101, 9);
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write("Sword");
                                }
                                weapon = "Battle Axe";
                                Console.SetCursorPosition(101, 2);
                                Console.Write("                                     ");
                                Console.SetCursorPosition(101, 2);
                                Console.Write("Weapon: " + weapon);
                                Console.SetCursorPosition(101, 10);
                                Console.Write("                                     ");
                            }
                            break;
                        default:

                            break;
                    
                    }
                }
                if (mapGen.map[prevX, prevY] == 1)
                {
                    colour = ConsoleColor.White;
                    arrayCharacter = '▓';
                }
                if (mapGen.map[prevX, prevY] == 0)
                {
                    colour = ConsoleColor.White;
                    arrayCharacter = ' ';
                }
                if (mapGen.map[prevX, prevY] == 5)
                {
                    colour = ConsoleColor.Green;
                    arrayCharacter = '█';
                }
                if (mapGen.map[prevX, prevY] == 3)
                {
                    colour = ConsoleColor.Green;
                    arrayCharacter = '#';
                }
                if (canMove == true)
                {
                    WriteCharacter(arrayCharacter, prevX, prevY, colour);//Puts back what was visible before the player was in that position
                    colour = ConsoleColor.White;
                    WriteCharacter(character, x, y, colour);
                }

            }
            else
            {
                if (fighting == true)
                {
                    Fight();
                }
            }
        }
        public void DisplayStats()//Prints the information the player needs on the screeen (most likely overengineered)
        {
            Console.SetCursorPosition(101, 0);
            Console.Write("Stats: ");
            Console.SetCursorPosition(101, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Health: " + health);
            Console.SetCursorPosition(101, 2);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Weapon: " + weapon);
            Console.SetCursorPosition(101, 3);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Health Potions: " + healthItems);
            Console.SetCursorPosition(101, 4);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Coins: " + coins);
            Console.SetCursorPosition(101, 6);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Inventory: ");
            Console.SetCursorPosition(101, 21);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Level: " + (mapGen.difficulty + 1));
            if (hasHealthUpgrade == true)
            {
                Console.SetCursorPosition(101, 8);
                Console.ForegroundColor = ConsoleColor.DarkMagenta; ;
                Console.WriteLine("Health Trinket");
            }
            if(hasAxe == true && weapon != "Battle Axe")
            {
                Console.SetCursorPosition(101, 10);
                Console.ForegroundColor = ConsoleColor.Magenta; ;
                Console.WriteLine("Battle Axe");
            }
            if(hasSword == true && weapon != "Sword")
            {
                Console.SetCursorPosition(101, 9);
                Console.ForegroundColor = ConsoleColor.Magenta; ;
                Console.WriteLine("Sword");
            }
            if(mapGen.level == 1)
            {
                Console.SetCursorPosition(101, 22);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (capy.requiredEnemies == 0)
                {
                    Console.Write("Killed enemies: " + killedEnemies + "/Unkown");
                }
                else
                {
                    Console.SetCursorPosition(101, 22);
                    Console.Write("Killed enemies: " + killedEnemies + "/" + capy.requiredEnemies);
                }
            }
            else
            {
                Console.SetCursorPosition(101, 22);
                Console.Write("                                          ");
            }
            if(capyCoins > 0 && mapGen.level == 2)
            {
                Console.SetCursorPosition(101, 7);
                Console.ForegroundColor = ConsoleColor.Yellow;
                if(capy.requiredCoins == 0)
                {
                    Console.WriteLine("Capy Coins: " + capyCoins + "/Unkown");
                }
                else
                {
                    Console.WriteLine("Capy Coins: " + capyCoins + "/" + capy.requiredCoins);
                }
                
            }
            else
            {
                Console.SetCursorPosition(101, 7);
                Console.Write("                                          ");
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
        public void Fight()//Controls fighting between the player and the enemy
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey().Key;
                Console.SetCursorPosition(110,0);
                Console.ForegroundColor = ConsoleColor.Black;
                if (fighting == true && playerTurn == true)
                {
                    if (key == ConsoleKey.E)
                    {
                        switch (weapon)
                        {
                            case "Fists":
                                weaponDamage = 1;
                                break;
                            case "Sword":
                                weaponDamage = 2;
                                break;
                            case "Battle Axe":
                                weaponDamage = 3;
                                break;
                        }
                        fightingEnemy.health -= weaponDamage;
                        Console.SetCursorPosition(0, 25);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You hit the enemy and did " + weaponDamage + " damage!");
                        Thread.Sleep(1000);
                        if (fightingEnemy.health <= 0)
                        {
                            killedEnemies++;
                            fighting = false;
                            fightingEnemy.inbattle = false;
                            fightingEnemy.enemyTurn = false;
                            playerTurn = true;
                            fightingEnemy.character = 'X';
                            coinAmount = rnd.Next(5, 20);
                            coinAmount += (mapGen.difficulty * 2);
                            Console.SetCursorPosition(0, 25);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("You killed the enemy and gained " + coinAmount + " coins!");
                            coins += coinAmount;
                            coinAmount = 0;
                            Console.SetCursorPosition(101, 23);
                            Console.Write("                                  ");
                            Console.SetCursorPosition(101, 24);
                            Console.Write("                                  ");
                        }
                        else
                        {
                            playerTurn = false;
                            fightingEnemy.enemyTurn = true;
                            fightingEnemy.Damage();
                        }
                    }
                    if(key == ConsoleKey.R)
                    {
                        if(healthItems > 0)
                        {
                            if (health < maxHealth)
                            {
                                health += 5;
                                if(health > maxHealth)
                                {
                                    health = maxHealth;
                                }
                                healthItems -= 1;
                                playerTurn = false;
                                fightingEnemy.enemyTurn = true;
                                Console.SetCursorPosition(0, 25);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 25);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("You used a health potion!");

                            }
                            else
                            {
                                Console.SetCursorPosition(0, 25);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Health is max");
                            }
                        }
                        else
                        {
                            playerTurn = false;
                            fightingEnemy.enemyTurn = true;
                            Console.SetCursorPosition(0, 25);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, 25);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("You dont have any health potions!");
                            
                        }
                        Thread.Sleep(1000);
                        fightingEnemy.Damage();
                    }
                    if(key == ConsoleKey.Q)
                    {
                        int quitDamage = 0;
                        switch (weapon)
                        {
                            case "Fists":
                                quitDamage = rnd.Next(1, 2 + mapGen.difficulty);
                                break;
                            case "Sword":
                                quitDamage = rnd.Next(1, 3 + mapGen.difficulty);
                                break;
                            case "Battle Axe":
                                quitDamage = rnd.Next(1, 4 + mapGen.difficulty);
                                break;
                        }
                        if(quitDamage > 8)
                        {
                            quitDamage = 8;
                        }
                        fighting = false;
                        fightingEnemy.quitFight = true;
                        fightingEnemy.inbattle = false;
                        Console.SetCursorPosition(0, 25);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("You left the battle and took " + quitDamage + " damage in your escape");
                        health -= quitDamage;
                        
                        if (health <= 0)
                        {
                            Console.Clear();
                            Program.DeathScreen();
                        }
                        Console.SetCursorPosition(101, 23);
                        Console.Write("                                   ");
                        Console.SetCursorPosition(101, 24);
                        Console.Write("                                  ");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(101, 1);
                        Console.WriteLine("                        ");
                    }
                    
                }
            
         
            }
        }

        public void Interaction()//Controls what items the player should recieve when walking through the bushes
        {

            bool itemfound = false; ;
            string item = " ";
            ConsoleColor colour = ConsoleColor.White;
            if (mapGen.map[x + 1, y] == 10 || mapGen.map[x - 1, y] == 10 || mapGen.map[x, y + 1] == 10 || mapGen.map[x, y - 1] == 10)
            {
                Console.SetCursorPosition(0, 25);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("The Capybara says: ");
                Console.SetCursorPosition(0, 26);
                Console.WriteLine(capy.levelText);
                Console.SetCursorPosition(101, 22);
                Console.Write("                                     ");
                Console.SetCursorPosition(101, 7);
                Console.Write("                             ");
                capy.Mission();
            }
            else if (mapGen.map[x, y] == 3)
            {
                int itemChance = rnd.Next(1, 100);
                if (itemChance >= 1 && itemChance < 20 && mapGen.level == 2)
                {
                    capyCoins++;
                    itemfound = true;
                    item = "Capy Coin";
                    colour = ConsoleColor.Yellow;
                }
                if (itemChance >= 20 && itemChance < 30)
                {
                    healthItems++;
                    itemfound = true;
                    colour = ConsoleColor.Blue;
                    item = "health potion";
                }
                if(itemChance >= 90 && itemChance < 99)
                {
                    int itemtype = rnd.Next(1, 3);
                    if(itemtype == 1 && hasSword == false)
                    {
                        item = "Sword";
                        itemfound = true;
                        colour = ConsoleColor.Magenta;
                        hasSword = true;
                    }
                    if(itemtype == 2 && hasAxe == false)
                    {
                        item = "Battle Axe";
                        itemfound = true;
                        colour = ConsoleColor.Magenta;
                        hasAxe = true;
                    }
                    
                }
               if(itemChance >=80 && itemChance < 85)
                {
                    if(hasHealthUpgrade == false)
                    {
                        hasHealthUpgrade = true;
                        maxHealth = 20;
                        health = maxHealth;
                        itemfound = true;
                        colour = ConsoleColor.DarkMagenta;
                        item = "health trinket";
                    }
                }
                

            }
            else
            {
                for (int i = 25; i < 30; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }
            if (itemfound == true)
            {
                if (item == "Sword" || item == "Battle Axe")
                {
                    engaged = true;
                    Engage(item, colour);
                }
                else
                {
                    Console.SetCursorPosition(0, 25);
                    Console.ForegroundColor = colour;
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 25);
                    if (item == "health trinket")
                    {
                        Console.WriteLine("You found a " + item + " in the bush! Your max health has doubled");
                    }
                    else
                    {
                        Console.WriteLine("You found a " + item + " in the bush!");
                    }
                }
            }
        }
        public void Engage(string item, ConsoleColor colour)//If the player finds a weapon they do not have this function will pause the game to allow the player to decide if they want to equip it
        {
            bool choiceChosen = false;
            Console.SetCursorPosition(0, 25);
            Console.ForegroundColor = colour;
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("You found a " + item + " in the bush!");
            Console.WriteLine("Do you want to swap your " + weapon + " with the " + item + "? Press Y/N");
            var key = Console.ReadKey().Key;
            while (choiceChosen == false)
            {
                if (key == ConsoleKey.Y)
                {
                    if (weapon == "Sword")
                    {
                        Console.SetCursorPosition(101, 9);
                        Console.Write("Sword");
                    }
                    if(weapon == "Battle Axe")
                    {
                        Console.SetCursorPosition(101, 10);
                        Console.Write("Battle Axe");
                    }
                    weapon = item;
                    engaged = false;
                    Console.SetCursorPosition(101, 2);
                    Console.Write("                                  ");
                    Console.SetCursorPosition(101, 2);
                    Console.Write("Weapon: " + weapon);
                    Console.SetCursorPosition(101, 10);
                    Console.Write("                                  ");
                    
                    Console.SetCursorPosition(0, 25);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 26);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 27);
                    Console.Write(new string(' ', Console.WindowWidth));
                    choiceChosen = true;
                }
                if (key == ConsoleKey.N)
                {
                    engaged = false;
                    if (item == "Sword")
                    {
                        Console.SetCursorPosition(101, 9);
                        Console.Write("Sword");
                    }
                    if (item == "Battle Axe")
                    {
                        Console.SetCursorPosition(101, 10);
                        Console.Write("Battle Axe");
                    }
                    Console.SetCursorPosition(0, 25);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 26);
                    Console.Write(new string(' ', Console.WindowWidth));
                    choiceChosen = true;
                }
                else
                {
                    key = Console.ReadKey().Key;
                }
            }
        }
        public void Quit()//Returns the player to the main menu but saves the game first
        {
            bool choiceChosen = false;
            Console.SetCursorPosition(0, 25);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Do you want to quit? Y/N");
            var key = Console.ReadKey().Key;
            while (choiceChosen == false)
            {
                if (key == ConsoleKey.Y)
                {
                    choiceChosen = true;
                    Program.SaveGame();
                    Console.Clear();
                    mapGen.ClearRoom();
                    Program.gameRunning = false;
                    Program.MainMenu();
                }
                if(key == ConsoleKey.N)
                {
                    choiceChosen = true;
                    Console.SetCursorPosition(0, 25);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                else
                {
                    key = Console.ReadKey().Key;
                }
            }
        }
        
   }
}
