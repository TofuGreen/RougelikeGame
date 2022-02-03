using System;
using System.Collections.Generic;
using System.Text;

namespace RougelikeGame
{
    class MapGenerator
    {
        public Room startingRoom;
        public Room endRoom;

        public List<Room> myRooms;
        public int roomCount = 10;

        public MapGenerator()
        {
            //startingRoom = new Room();
            //startingRoom.RandomiseRoom("startRoom" ,5, 11, 5, 11);
            //endRoom = new Room();
            //endRoom.RandomiseRoom("endRoom", 5, 11, 5, 11);

            myRooms = new List<Room>();

            RoomInit();
        }

        public void RoomInit()
        {
            for (int i = 0; i < roomCount; i++)
            {
                //Create a temp room
                Room newRoom = new Room();
                newRoom.RandomiseRoom("R" + i, 5, 11, 5, 11); //customise it

                //add it to my collection
                myRooms.Add(newRoom);
            }
        }

    }
}
