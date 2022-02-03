using System;

namespace RougelikeGame
{
    class Program
    {
        static MapGenerator roomManager;


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //master test 2 now doing the stream to show this off branching test
            roomManager = new MapGenerator();
            //roomManager.RoomInit();
        }

        public void SpecialFunctionality()
        {
            //very awesome code
        }
    }
}
