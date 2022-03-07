using System;

namespace RougelikeGame
{
    class Program
    {
        static MapGenerator roomManager;
        static MapGenerator2 mapMaker;
        static Player player;
        static void Main(string[] args)
        {
            player = new Player();
            player.gameRunning = true;

            //Console.WriteLine("Hello World!");
            //master test 2 now doing the stream to show this off branching test
            //roomManager = new MapGenerator();
            mapMaker = new MapGenerator2();
            mapMaker.CreateMap();
            player.Movement();
            //roomManager.RoomInit();
        }

        public void SpecialFunctionality()
        {
            //very awesome code
        }
    }
}
