using System.Collections.Generic;
using UnityEngine;
using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.RoadBuilder
{
    public class RoadBuilder
    {
        ////ROAD PLACEMENT
        public static TileType[,] BuildRoads(TileType[,] mapGrid, int initialWalkerCount, float passThroughChance, float roadTurnChance, float walkerSpawnChance)
        {
            //List "walkers" holds the Walkers
            List<Walker> walkers = GetWalkers(mapGrid, initialWalkerCount);

            return RoadWalk(mapGrid, walkers, passThroughChance, roadTurnChance, walkerSpawnChance);
        }

        //method TURN is used to rotate a dir vector by 90deg
        static int[] Turn(int[] dir)
        {
            for (int i = 0; i < 2; i++)
            {
                if (dir[i] != 0)
                {
                    dir[i] = 0;
                }
                else
                {
                    dir[i] = new int[] { -1, 1 }[Random.Range(0, 2)];
                }
            }
            return dir;
        }
        //Funtion to get an initial dir vector based on spawn location of walker and map size
        //used to point roads that start near the edge towards the mapGrid centre
        static int[] StartingDir(int xPos, int yPos, int mapGridW, int mapGridH)
        {
            int horizontalLean = xPos - mapGridW / 2;
            int verticalLean = yPos - mapGridH / 2;
            if (Mathf.Abs(horizontalLean) >= Mathf.Abs(verticalLean))
            {
                if (horizontalLean == 0) { return new int[] { 0, 0 }; } //handle the 0 case
                return new int[] { -horizontalLean / Mathf.Abs(horizontalLean), 0 }; //return normalized
            }
            if (verticalLean == 0) { return new int[] { 0, 0 }; } //handle the 0 case
            return new int[] { 0, -verticalLean / Mathf.Abs(verticalLean) }; //return normalized
        }

        ////GET WALKERS
        static List<Walker> GetWalkers(TileType[,] mapGrid, int initialWalkerCount)
        {
            int mapX = mapGrid.GetLength(0);
            int mapY = mapGrid.GetLength(1);
            //List "walkers" holds the Walkers
            List<Walker> walkers = new();

            //place initial walkers
            for (int i = 0; i < initialWalkerCount; i++)
            {
                bool placed = false;
                while (!placed)
                {
                    //try random positions until we get one that isn't the rift or another walker
                    int walkerX = Random.Range(2, mapX - 1);
                    int walkerY = Random.Range(2, mapY - 1);
                    if (mapGrid[walkerX, walkerY] == TileType.none)
                    { //unoccupied space
                        mapGrid[walkerX, walkerY] = TileType.roadTile;
                        var newWalker = new Walker(walkerX, walkerY, StartingDir(walkerX, walkerY, mapX, mapY));
                        walkers.Add(newWalker);
                        placed = true;
                    }
                }
            }

            return walkers;
        }

        ////THE ROAD WALK
        ///uses a drunken walk with parameters specific to the map
        public static TileType[,] RoadWalk(TileType[,] mapGrid, List<Walker> walkers, float passThroughChance, float roadTurnChance, float walkerSpawnChance)
        {
            int mapX = mapGrid.GetLength(0);
            int mapY = mapGrid.GetLength(1);

            int numberWalkers = walkers.Count;
            //in this loop, the walk begins
            while (numberWalkers > 0)
            {
                //they step until there are none
                for (int i = 0; i < walkers.Count; i++)
                {
                    //place road
                    mapGrid[walkers[i].Xpos, walkers[i].Ypos] = TileType.roadTile;
                    //get next position x and y
                    int nextX = walkers[i].Xpos + walkers[i].Dir[0];
                    int nextY = walkers[i].Ypos + walkers[i].Dir[1];
                    //check to delete (out of range, running into something)
                    //ignore passthrough chance for now
                    if (nextX >= mapX || nextX < 0 || nextY >= mapY || nextY < 0)
                    {
                        walkers.RemoveAt(i);
                        //edit iterator appropriately
                        i -= 1;
                        numberWalkers = walkers.Count;
                        break;
                    }
                    else if (mapGrid[nextX, nextY] != TileType.none)
                    {
                        //if it's another road, the walker has a chance to pass through to the other side
                        if (Random.Range(0, 1f) > passThroughChance)
                        {
                            walkers.RemoveAt(i);
                            //edit iterator appropriately
                            i -= 1;
                            numberWalkers = walkers.Count;
                            break;
                        }
                    }
                    //move
                    walkers[i].Xpos = nextX;
                    walkers[i].Ypos = nextY;
                    //turn
                    if (Random.Range(0, 1f) < roadTurnChance)
                    {
                        walkers[i].Dir = Turn(walkers[i].Dir);
                    }
                    //spawn new walker
                    if (Random.Range(0, 1f) < walkerSpawnChance)
                    {
                        int newX = walkers[i].Xpos, newY = walkers[i].Ypos;
                        int[] newDir = new int[] { walkers[i].Dir[0], walkers[i].Dir[1] };
                        var newWalker = new Walker(newX, newY, newDir);
                        newWalker.Dir = Turn(newWalker.Dir);
                        walkers.Add(newWalker);
                    }
                }
            }
            return mapGrid;
        }
    }

    ///Create a WALKER class to handle the walker properties
    public class Walker
    {
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public int[] Dir { get; set; }

        //constructor
        public Walker(int xpos, int ypos, int[] dir)
        {
            this.Xpos = xpos;
            this.Ypos = ypos;
            this.Dir = dir;
        }
    }
}

