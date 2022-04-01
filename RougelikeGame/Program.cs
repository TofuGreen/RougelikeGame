using System;

namespace RougelikeGame
{
    class Program
    {
        static MapGenerator roomManager;
        static MapGenerator3 mapMaker;
        static Player player;
        static bool choiceChosen = false;
        static void Main(string[] args)
        {
            
            player = new Player();
            player.gameRunning = true;
            mapMaker = new MapGenerator3();
            player.mapGen = mapMaker;
            MainMenu();
            //Console.WriteLine("Hello World!");
            //master test 2 now doing the stream to show this off branching test
            //roomManager = new MapGenerator();
            
            //roomManager.RoomInit();
        }
        public static void MainMenu()
        {
            string title = "▄▄▄█████▓▓█████ ▒██   ██▒▄▄▄█████▓     ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▄▄▄        ▄████  ▄▄▄       ██▓ ███▄    █ \n▓  ██▒ ▓▒▓█   ▀ ▒▒ █ █ ▒░▓  ██▒ ▓▒    ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒████▄     ██▒ ▀█▒▒████▄    ▓██▒ ██ ▀█   █ \n▒ ▓██░ ▒░▒███   ░░  █   ░▒ ▓██░ ▒░   ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██  ▀█▄  ▒██░▄▄▄░▒██  ▀█▄  ▒██▒▓██  ▀█ ██▒\n░ ▓██▓ ░ ▒▓█  ▄  ░ █ █ ▒ ░ ▓██▓ ░    ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ░██▄▄▄▄██ ░▓█  ██▓░██▄▄▄▄██ ░██░▓██▒  ▐▌██▒\n  ▒██▒ ░ ░▒████▒▒██▒ ▒██▒  ▒██▒ ░    ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒    ▓█   ▓██▒░▒▓███▀▒ ▓█   ▓██▒░██░▒██░   ▓██░\n  ▒ ░░   ░░ ▒░ ░▒▒ ░ ░▓ ░  ▒ ░░       ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░    ▒▒   ▓▒█░ ░▒   ▒  ▒▒   ▓▒█░░▓  ░ ▒░   ▒ ▒ \n    ░     ░ ░  ░░░   ░▒ ░    ░         ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ▒   ▒▒ ░  ░   ░   ▒   ▒▒ ░ ▒ ░░ ░░   ░ ▒░\n  ░         ░    ░    ░    ░         ░ ░   ░   ░   ▒   ░      ░      ░        ░   ▒   ░ ░   ░   ░   ▒    ▒ ░   ░   ░ ░ \n            ░  ░ ░    ░                    ░       ░  ░       ░      ░  ░         ░  ░      ░       ░  ░ ░           ░ \n";
            Console.WriteLine(title);
            Console.WriteLine("\n1.Start new game\n2.Exit");
            Console.WriteLine("What do you want to do?");
            string choice = Console.ReadLine();
            while (choiceChosen == false)
            {
                if (choice == "1")
                {
                    choiceChosen = true;
                    Console.Clear();
                    Console.CursorVisible = false;
                    mapMaker.CreateMap();
                    player.InitialiseObjects();
                    player.Movement();
                    //mapMaker.SpawnSnakes();
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
        }
        public void SpecialFunctionality()
        {
            //very awesome code
        }
    }
}
