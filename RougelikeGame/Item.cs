using System;
using System.Collections.Generic;
using System.Text;

namespace RougelikeGame
{
    class Item
    {
        public string name;
        public int weight;
        public float value;
        static Random rnd = new Random();
        public static MapGenerator3 mapDifficulty;
    }
}
