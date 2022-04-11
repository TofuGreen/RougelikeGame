using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
namespace RougelikeGame
{
    public class Program
    {
        public static MapGenerator3 mapMaker;
        public static Player player;
        public static Capybara capy;
        static bool choiceChosen = false;
        public static bool gameRunning;
        public static int enemyCount;
        public static bool changingLevel;
        public const string pathWithEnv = @"%USERPROFILE%\AppData\Local\TextGameAgain\SaveFile\";
        public static string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
        static void Main(string[] args)
        {

            mapMaker = new MapGenerator3();
            player = new Player();
            capy = new Capybara();
            mapMaker.capyPlayer = player;
            player.mapGen = mapMaker;
            capy.map = mapMaker;
            gameRunning = false;
            mapMaker.loading = false;
            Console.WriteLine("Please set Console font size to 28, enable bold fonts and set to fullscreen for best viewing experience visual glitches may occur otherwise\n Press any enter to continue");
            Console.ReadLine();
            Console.Clear();
            MainMenu();

            while (gameRunning == true)//This loop allows the game to run continuosly until the player quits. Probably could be done more efficiently
            {
                if (changingLevel == false)
                {
                    player.Movement();
                    for (int i = 0; i < mapMaker.enemyArray.Length; i++)
                    {

                        mapMaker.enemyArray[i].Movement(mapMaker.map);

                    }
                    
                }
            }
        }
        public static void MainMenu()//Allows the user to start a new game or load a previous one
        {
            Console.ForegroundColor = ConsoleColor.White;
            choiceChosen = false;
            string title = "▄▄▄█████▓▓█████ ▒██   ██▒▄▄▄█████▓     ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▄▄▄        ▄████  ▄▄▄       ██▓ ███▄    █ \n▓  ██▒ ▓▒▓█   ▀ ▒▒ █ █ ▒░▓  ██▒ ▓▒    ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒████▄     ██▒ ▀█▒▒████▄    ▓██▒ ██ ▀█   █ \n▒ ▓██░ ▒░▒███   ░░  █   ░▒ ▓██░ ▒░   ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██  ▀█▄  ▒██░▄▄▄░▒██  ▀█▄  ▒██▒▓██  ▀█ ██▒\n░ ▓██▓ ░ ▒▓█  ▄  ░ █ █ ▒ ░ ▓██▓ ░    ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ░██▄▄▄▄██ ░▓█  ██▓░██▄▄▄▄██ ░██░▓██▒  ▐▌██▒\n  ▒██▒ ░ ░▒████▒▒██▒ ▒██▒  ▒██▒ ░    ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒    ▓█   ▓██▒░▒▓███▀▒ ▓█   ▓██▒░██░▒██░   ▓██░\n  ▒ ░░   ░░ ▒░ ░▒▒ ░ ░▓ ░  ▒ ░░       ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░    ▒▒   ▓▒█░ ░▒   ▒  ▒▒   ▓▒█░░▓  ░ ▒░   ▒ ▒ \n    ░     ░ ░  ░░░   ░▒ ░    ░         ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ▒   ▒▒ ░  ░   ░   ▒   ▒▒ ░ ▒ ░░ ░░   ░ ▒░\n  ░         ░    ░    ░    ░         ░ ░   ░   ░   ▒   ░      ░      ░        ░   ▒   ░ ░   ░   ░   ▒    ▒ ░   ░   ░ ░ \n            ░  ░ ░    ░                    ░       ░  ░       ░      ░  ░         ░  ░      ░       ░  ░ ░           ░ \n";
            Console.WriteLine(title);
            Console.WriteLine("1.Start new game\n2.Load Game");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("3.How to play");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("4.Exit");
            Console.WriteLine("\nPress 1,2,3 or 4");
            var key = Console.ReadKey().Key;
            while (choiceChosen == false)
            {
                if (key == ConsoleKey.D1)
                {
                    Console.Clear();
                    changingLevel = false;
                    choiceChosen = true;
                    player.maxHealth = 10;
                    player.killedEnemies = 0;
                    player.x = 5;
                    player.y = 5;
                    mapMaker.difficulty = 0;
                    player.InitialiseObjects();
                    mapMaker.CreateMap();
                    mapMaker.SpawnEnemys();
                    gameRunning = true;
                    if (Directory.Exists(filePath))
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(filePath);
                        foreach(FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                    }
                    Console.CursorVisible = false;
                }
                else if (key == ConsoleKey.D2)//Loading a game requires the user to press a key for the player and enemies to appear on screen not sure why
                {
                    if (File.Exists(filePath + "save1.txt"))
                    {
                        gameRunning = true;
                        choiceChosen = true;
                        Console.Clear();
                        mapMaker.loading = true;
                        changingLevel = false;
                        choiceChosen = true;
                        player.maxHealth = 10;
                        player.InitialiseObjects();
                        mapMaker.SpawnEnemys();
                        LoadGame();
                        mapMaker.SpawnCapybara();
                        mapMaker.DisplayMap();
                        capy.Mission();
                        Console.CursorVisible = false;
                        mapMaker.loading = false;
                        
                    }
                    else
                    {
                        Console.WriteLine("No Save Found");
                        key = Console.ReadKey().Key;

                    }
                }
                else if (key == ConsoleKey.D3)
                {
                    choiceChosen = true;
                    Console.Clear();
                    HelpMenu();
                }
                else if (key == ConsoleKey.D4)
                {
                    choiceChosen = true;
                    System.Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("That is an invalid answer");
                    Console.WriteLine("What do you want to do?");
                    key = Console.ReadKey().Key;
                }
            }

        }
        static void HelpMenu()//Displays helpful information for new players
        {
            bool choiceChosen = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            string title = " ██░ ██  ▒█████   █     █░   ▄▄▄█████▓ ▒█████      ██▓███   ██▓    ▄▄▄     ▓██   ██▓\n▓██░ ██▒▒██▒  ██▒▓█░ █ ░█░   ▓  ██▒ ▓▒▒██▒  ██▒   ▓██░  ██▒▓██▒   ▒████▄    ▒██  ██▒\n▒██▀▀██░▒██░  ██▒▒█░ █ ░█    ▒ ▓██░ ▒░▒██░  ██▒   ▓██░ ██▓▒▒██░   ▒██  ▀█▄   ▒██ ██░\n░▓█ ░██ ▒██   ██░░█░ █ ░█    ░ ▓██▓ ░ ▒██   ██░   ▒██▄█▓▒ ▒▒██░   ░██▄▄▄▄██  ░ ▐██▓░\n░▓█▒░██▓░ ████▓▒░░░██▒██▓      ▒██▒ ░ ░ ████▓▒░   ▒██▒ ░  ░░██████▒▓█   ▓██▒ ░ ██▒▓░\n ▒ ░░▒░▒░ ▒░▒░▒░ ░ ▓░▒ ▒       ▒ ░░   ░ ▒░▒░▒░    ▒▓▒░ ░  ░░ ▒░▓  ░▒▒   ▓▒█░  ██▒▒▒ \n ▒ ░▒░ ░  ░ ▒ ▒░   ▒ ░ ░         ░      ░ ▒ ▒░    ░▒ ░     ░ ░ ▒  ░ ▒   ▒▒ ░▓██ ░▒░ \n ░  ░░ ░░ ░ ░ ▒    ░   ░       ░      ░ ░ ░ ▒     ░░         ░ ░    ░   ▒   ▒ ▒ ░░\n ░  ░  ░    ░ ░      ░                    ░ ░                  ░  ░     ░  ░░ ░     \n                                                                            ░ ░     ";
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Use the arrow keys to move around and 1-3 to change weapon (if you have them)");
            Console.WriteLine("Visit the Capybara (" + (char)2 + ") to find your quest for the level.\nThen return to the Capybara once the quest is complete to move to the next level.");
            Console.WriteLine("Find items in the bushes that will assist you on your mission.");
            Console.WriteLine("When fighting an enemy you will be unable to move till you kill, are killed or run away.\nPress E to attack, press R to heal(healing will use up your turn) and Q to quit the fight");
            Console.WriteLine("If you quit from a fight enemies will heal health over time");
            Console.WriteLine("If you die the game will restart");
            Console.WriteLine("Press Escape to pause the game");
            Console.WriteLine("Press Enter to return to menu");
            var key = Console.ReadKey().Key;
            while (choiceChosen == false)
            {
                if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    choiceChosen = true;
                    MainMenu();
                }
                else
                {
                    key = Console.ReadKey().Key;
                }
            }
        }
        public static void DeathScreen()
        {
            if (Directory.Exists(filePath))
            {
                File.Delete(filePath + "save1.txt");
                for (int i = 0; i < mapMaker.enemyArray.Length; i++)
                {
                    File.Delete(filePath + "save1Enemy" + mapMaker.enemyArray[i].ID + ".txt");
                }
            }
            choiceChosen = false;
            string deathTitle = "▓██   ██▓ ▒█████   █    ██    ▓█████▄  ██▓▓█████ ▓█████▄    \n ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▒██▀ ██▌▓██▒▓█   ▀ ▒██▀ ██▌   \n  ▒██ ██░▒██░  ██▒▓██  ▒██░   ░██   █▌▒██▒▒███   ░██   █▌   \n  ░ ▐██▓░▒██   ██░▓▓█  ░██░   ░▓█▄   ▌░██░▒▓█  ▄ ░▓█▄   ▌   \n  ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░▒████▓ ░██░░▒████▒░▒████▓    \n   ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒     ▒▒▓  ▒ ░▓  ░░ ▒░ ░ ▒▒▓  ▒    \n ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░     ░ ▒  ▒  ▒ ░ ░ ░  ░ ░ ▒  ▒    \n ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░     ░ ░  ░  ▒ ░   ░    ░ ░  ░    \n ░ ░         ░ ░     ░           ░     ░     ░  ░   ░       \n ░ ░                           ░                  ░         ";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(deathTitle);
            if(player.coins >= 100 && player.coins < 200)
            {
                Console.WriteLine("You collected " + player.coins + " good job!");
            }
            if(player.coins >= 200 && player.coins < 500)
            {
                Console.WriteLine("You collected " + player.coins + " very well done!");
            }
            if(player.coins >= 500)
            {
                Console.WriteLine("You collected " + player.coins + " you are a legend!");
            }
            if(player.coins < 100)
            {
                Console.WriteLine("You collected " + player.coins + " what a failure!");
            }
            Console.WriteLine("\n1.Retrun to menu\n2.Exit");
            Console.WriteLine("Press 1 or 2");
            changingLevel = true;
            var choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.D1)
            {
                choiceChosen = true;
                Console.Clear();
                mapMaker.ClearRoom();
                MainMenu();
            }
            else if (choice == ConsoleKey.D2)
            {
                choiceChosen = true;
                System.Environment.Exit(1);
            }
            else
            {
                Console.WriteLine("That is an invalid answer");
                Console.WriteLine("What do you want to do?");
                choice = Console.ReadKey().Key;
            }

        }
        public static void SaveGame()//Saves all data to a serialized file .txt is used but it could really be anything
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            Stream saveFileStream = File.Create(filePath + "save1.txt");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, player.x);
            serializer.Serialize(saveFileStream, player.y);
            serializer.Serialize(saveFileStream, player.maxHealth);
            serializer.Serialize(saveFileStream, player.health);
            serializer.Serialize(saveFileStream, player.healthItems);
            serializer.Serialize(saveFileStream, player.hasHealthUpgrade);
            serializer.Serialize(saveFileStream, player.killedEnemies);
            serializer.Serialize(saveFileStream, player.weapon);
            serializer.Serialize(saveFileStream, player.coins);
            serializer.Serialize(saveFileStream, player.capyCoins);
            serializer.Serialize(saveFileStream, player.hasAxe);
            serializer.Serialize(saveFileStream, player.hasSword);
            serializer.Serialize(saveFileStream, mapMaker.map);
            serializer.Serialize(saveFileStream, mapMaker.level);
            serializer.Serialize(saveFileStream, mapMaker.difficulty);
            serializer.Serialize(saveFileStream, capy.requiredEnemies);
            serializer.Serialize(saveFileStream, capy.requiredCoins);
            saveFileStream.Close();
            for(int i = 0; i < mapMaker.enemyArray.Length; i++)
            {
                mapMaker.enemyArray[i].SaveEnemy();
            }
        }
        

        static void LoadGame()//Reads all the data from the serialized file back into the correct varialbes
        {
            Stream openFileStream = File.OpenRead(filePath + "save1.txt");
            BinaryFormatter deserializer = new BinaryFormatter();
            player.x = (int)deserializer.Deserialize(openFileStream);
            player.y = (int)deserializer.Deserialize(openFileStream);
            player.maxHealth = (int)deserializer.Deserialize(openFileStream);
            player.health = (int)deserializer.Deserialize(openFileStream);
            player.healthItems = (int)deserializer.Deserialize(openFileStream);
            player.hasHealthUpgrade = (bool)deserializer.Deserialize(openFileStream);
            player.killedEnemies = (int)deserializer.Deserialize(openFileStream);
            player.weapon = (string)deserializer.Deserialize(openFileStream);
            player.coins = (int)deserializer.Deserialize(openFileStream);
            player.capyCoins = (int)deserializer.Deserialize(openFileStream);
            player.hasAxe = (bool)deserializer.Deserialize(openFileStream);
            player.hasSword = (bool)deserializer.Deserialize(openFileStream);
            mapMaker.map = (int[,])deserializer.Deserialize(openFileStream);
            mapMaker.level = (int)deserializer.Deserialize(openFileStream);
            mapMaker.difficulty = (int)deserializer.Deserialize(openFileStream);
            capy.requiredEnemies = (int)deserializer.Deserialize(openFileStream);
            capy.requiredCoins = (int)deserializer.Deserialize(openFileStream);
            openFileStream.Close();
            for (int i = 0; i < mapMaker.enemyArray.Length; i++)
            {
                mapMaker.enemyArray[i].LoadEnemy();
            }
            
        }
    }
}
