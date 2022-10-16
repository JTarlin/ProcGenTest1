using System.Collections.Generic;
using UnityEngine;
using Generation.RoadBuilder;
using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.RiftPlacer
{
    public class RiftPlacer
    {
        //RIFT PLACEMENT
        //Places a 3x3 Rift structure on the grid
        public static TileType[,] PlaceRift(TileType[,] mapGrid)
        {
            int centrePointX = Random.Range(3, mapGrid.GetLength(0) - 2);
            int centrePointY = Random.Range(3, mapGrid.GetLength(1) - 2);

            //sets the 9 tiles of the rift
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    mapGrid[centrePointX + i, centrePointY + j] = TileType.riftTile;
                }
            }

            mapGrid = ConnectRift(mapGrid, centrePointX, centrePointY);

            return mapGrid;
        }

        ////RIFT INTERCONNECTOR
        ///When none of the adjacent tiles to the Rift are roads, it builds a new road starting from the Rift towards existing
        static TileType[,] ConnectRift(TileType[,] mapGrid, int i0, int j0)
        {
            //checks for adjacent roads to placed rift
            for (int i = -2; i < 3; i++)
            {
                for (int j = -2; j < 3; j++)
                {
                    if (mapGrid[i0+i, j0+j] == TileType.roadTile)
                    {
                        //if there's already a road, we chillin, return mapGrid as-is
                        return mapGrid;
                    }
                }
            }

            //build rift road here
            mapGrid = MakeRiftRoad(mapGrid, i0, j0);

            return mapGrid;
        }

        ////GET dist along straight lines from point to road
        private static int DistToRoad(TileType[,] mapGrid, int x0, int y0, int dX, int dY)
        {
            int x = 0;
            int y = 0;
            while (true)
            {
                //check for out of bounds
                if (x0+x >= mapGrid.GetLength(0)|| y0 + y >= mapGrid.GetLength(1) || x0+x < 0 || y0+y<0)
                {
                    //road is not reachable, arbitrary large int
                    return 1000000;
                } else if(mapGrid[x0 + x, y0 + y] == TileType.roadTile)
                {
                    //we've found our road, return the appropriate distance
                    break;
                }
                
                y += dY;
                x += dX;
            }
            //one of these should be zero, since its delta was 0
            return Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
        }

        ////MAKE A WALKER TO BUILD A ROAD TOWARDS THE NEAREST ROAD
        static TileType[,] MakeRiftRoad(TileType[,] mapGrid, int i0, int j0)
        {
            //unit vectors
            int[] xPos = { 1, 0 }, xNeg = { -1, 0 }, yPos= {0,1 },yNeg= { 0,-1};


            int minDist = DistToRoad(mapGrid, i0, j0, 1, 0);
            //Debug.Log($"Distance right to road from rift: {minDist}");
            Walker walker = new(i0 + 2, j0, xPos);
            int distNegX = DistToRoad(mapGrid, i0, j0, -1, 0);
            //Debug.Log($"Distance left to road from rift: {distNegX}");
            if (distNegX < minDist)
            {
                walker = new(i0 - 2, j0, xNeg);
                minDist = distNegX;
            }
            int distPosY = DistToRoad(mapGrid, i0, j0, 0, 1);
            //Debug.Log($"Distance up to road from rift: {distPosY}");
            if (distPosY < minDist)
            {
                walker = new(i0, j0+2, yPos);
                minDist = distPosY;
            }
            int distNegY = DistToRoad(mapGrid, i0, j0, 0, -1);
            //Debug.Log($"Distance down to road from rift: {distNegY}");
            if (distNegY < minDist)
            {
                walker = new(i0, j0 -2, yNeg);
            }

            List<Walker> walkers = new() { walker };

            mapGrid = RoadBuilder.RoadBuilder.RoadWalk(mapGrid, walkers, 0, 0, 0);
            return mapGrid;
        }
    }
}
