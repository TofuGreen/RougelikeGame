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
        static Random rnd = new Random();
        public const string pathWithEnv = @"%USERPROFILE%\AppData\Local\TextGameAgain\";
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
            MainMenu();

            while (gameRunning == true)
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
        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            choiceChosen = false;
            string title = "▄▄▄█████▓▓█████ ▒██   ██▒▄▄▄█████▓     ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▄▄▄        ▄████  ▄▄▄       ██▓ ███▄    █ \n▓  ██▒ ▓▒▓█   ▀ ▒▒ █ █ ▒░▓  ██▒ ▓▒    ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒████▄     ██▒ ▀█▒▒████▄    ▓██▒ ██ ▀█   █ \n▒ ▓██░ ▒░▒███   ░░  █   ░▒ ▓██░ ▒░   ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██  ▀█▄  ▒██░▄▄▄░▒██  ▀█▄  ▒██▒▓██  ▀█ ██▒\n░ ▓██▓ ░ ▒▓█  ▄  ░ █ █ ▒ ░ ▓██▓ ░    ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ░██▄▄▄▄██ ░▓█  ██▓░██▄▄▄▄██ ░██░▓██▒  ▐▌██▒\n  ▒██▒ ░ ░▒████▒▒██▒ ▒██▒  ▒██▒ ░    ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒    ▓█   ▓██▒░▒▓███▀▒ ▓█   ▓██▒░██░▒██░   ▓██░\n  ▒ ░░   ░░ ▒░ ░▒▒ ░ ░▓ ░  ▒ ░░       ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░    ▒▒   ▓▒█░ ░▒   ▒  ▒▒   ▓▒█░░▓  ░ ▒░   ▒ ▒ \n    ░     ░ ░  ░░░   ░▒ ░    ░         ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ▒   ▒▒ ░  ░   ░   ▒   ▒▒ ░ ▒ ░░ ░░   ░ ▒░\n  ░         ░    ░    ░    ░         ░ ░   ░   ░   ▒   ░      ░      ░        ░   ▒   ░ ░   ░   ░   ▒    ▒ ░   ░   ░ ░ \n            ░  ░ ░    ░                    ░       ░  ░       ░      ░  ░         ░  ░      ░       ░  ░ ░           ░ \n";
            Console.WriteLine(title);
            Console.WriteLine("\n1.Start new game\n2. Load Game\n3.How to play\n4.Exit");
            Console.WriteLine("What do you want to do?");
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
                    player.dead = false;
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
                else if (key == ConsoleKey.D2)
                {
                    if (!Directory.Exists(filePath))
                    {
                        Console.WriteLine("No Save Found");
                        key = Console.ReadKey().Key;
                    }
                    else
                    {
                        choiceChosen = true;
                        Console.Clear();
                        mapMaker.loading = true;
                        changingLevel = false;
                        choiceChosen = true;
                        player.maxHealth = 10;
                        player.InitialiseObjects();
                        player.dead = false;
                        mapMaker.SpawnEnemys();
                        LoadGame();
                        mapMaker.SpawnCapybara();
                        mapMaker.DisplayMap();
                        gameRunning = true;
                        Console.CursorVisible = false;
                        mapMaker.loading = false;
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
        static void HelpMenu()
        {
            bool choiceChosen = false;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string title = " ██░ ██  ▒█████   █     █░   ▄▄▄█████▓ ▒█████      ██▓███   ██▓    ▄▄▄     ▓██   ██▓\n▓██░ ██▒▒██▒  ██▒▓█░ █ ░█░   ▓  ██▒ ▓▒▒██▒  ██▒   ▓██░  ██▒▓██▒   ▒████▄    ▒██  ██▒\n▒██▀▀██░▒██░  ██▒▒█░ █ ░█    ▒ ▓██░ ▒░▒██░  ██▒   ▓██░ ██▓▒▒██░   ▒██  ▀█▄   ▒██ ██░\n░▓█ ░██ ▒██   ██░░█░ █ ░█    ░ ▓██▓ ░ ▒██   ██░   ▒██▄█▓▒ ▒▒██░   ░██▄▄▄▄██  ░ ▐██▓░\n░▓█▒░██▓░ ████▓▒░░░██▒██▓      ▒██▒ ░ ░ ████▓▒░   ▒██▒ ░  ░░██████▒▓█   ▓██▒ ░ ██▒▓░\n ▒ ░░▒░▒░ ▒░▒░▒░ ░ ▓░▒ ▒       ▒ ░░   ░ ▒░▒░▒░    ▒▓▒░ ░  ░░ ▒░▓  ░▒▒   ▓▒█░  ██▒▒▒ \n ▒ ░▒░ ░  ░ ▒ ▒░   ▒ ░ ░         ░      ░ ▒ ▒░    ░▒ ░     ░ ░ ▒  ░ ▒   ▒▒ ░▓██ ░▒░ \n ░  ░░ ░░ ░ ░ ▒    ░   ░       ░      ░ ░ ░ ▒     ░░         ░ ░    ░   ▒   ▒ ▒ ░░\n ░  ░  ░    ░ ░      ░                    ░ ░                  ░  ░     ░  ░░ ░     \n                                                                            ░ ░     ";
            Console.WriteLine(title);
            Console.WriteLine("Use the arrow keys to move around and 1-3 to change weapon (if you have them)");
            Console.WriteLine("Visit the Capybara (" + (char)2 + ") to find your quest for the level.\nThen return to the Capybara to move to the next level");
            Console.WriteLine("When fighting an enemy press E to attack, press R to heal(healing will use up your turn) and Q to quit the fight");
            Console.WriteLine("If you quit from a fight enemies will heal health over time");
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
            else
            {
                Console.WriteLine("You collected " + player.coins + " what a failure!");
            }
            Console.WriteLine("\n1.Retrun to menu\n2.Exit");
            Console.WriteLine("What do you want to do?");
            changingLevel = true;
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                choiceChosen = true;
                Console.Clear();
                mapMaker.ClearRoom();
                MainMenu();
                //changingLevel = true;

            }
            else if (choice == "2")
            {
                choiceChosen = true;
                System.Environment.Exit(1);
            }
            else
            {
                Console.WriteLine("That is an invalid answer");
                Console.WriteLine("What do you want to do?");
                choice = Console.ReadLine();
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
            openFileStream.Close();
            for (int i = 0; i < mapMaker.enemyArray.Length; i++)
            {
                mapMaker.enemyArray[i].LoadEnemy();
            }
            
        }
    }
}
