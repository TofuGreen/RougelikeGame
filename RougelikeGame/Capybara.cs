using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace RougelikeGame
{
    public class Capybara
    {
        public int level;
        public int difficulty;
        public string levelText;
        public int requiredEnemies;
        public int requiredCoins;
        public Player player;
        public MapGenerator3 map;
        public bool changingLevel;
        Random rnd = new Random();
        public void MissionText()//Displays the text for the current mission (Wanted to add more but I couldnt think of any)
        {
            switch (level) 
            {
                case 1:
                    levelText = "Kill " + (5 + ((1 * difficulty))) + "  enemies please";
                 break;
                case 2:
                    levelText = "Collect " + (3 + ((1 * difficulty))) + " capyCoins please";
                    break;
                case 3:
                    levelText = "Kill the boss!";
                    break;
            }
        }
        public void Mission()//Checks if the player has completed the quest and if so starts the next level
        {
            switch (level)
            {
                case 1:
                    requiredEnemies = 5 + (1 * difficulty);
                    if (player.killedEnemies >= requiredEnemies)
                    {
                        ChangeLevel();
                    }
                break;
                case 2:
                    requiredCoins = 3 + (1 * difficulty);
                    if (player.capyCoins >= requiredCoins)
                    {
                        ChangeLevel();
                    }
                    break;
                case 3:
                    if(player.killedEnemies >= 1)
                    {
                        ChangeLevel();
                    }
                    break;
            }
            
        }
        public void ChangeLevel()//Removes the current level and starts the next one
        {
            Console.SetCursorPosition(0, 26);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("                                           ");
            Console.SetCursorPosition(0, 26);
            Console.WriteLine("Thank you!");
            Console.SetCursorPosition(0, 28);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Loading next level...");
            Program.changingLevel = true;
            map.level = rnd.Next(1, 3);
            player.killedEnemies = 0;
            player.capyCoins = 0;
            map.difficulty++;
            requiredEnemies = 0;
            Thread.Sleep(2000);
            map.ClearRoom();
            map.CreateMap();
            Program.changingLevel = false;
            map.SpawnEnemys();
        }
    }
}
