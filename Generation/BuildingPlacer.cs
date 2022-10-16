using System.Collections.Generic;
using Generation.Building;
using UnityEngine;
using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.BuildingPlacer
{
    public class BuildingPlacer
    {
        //BUILDING PLACEMENT

        //checks through the various building types and attempts to place each one
        //different map themes get different building types
        public static TileType[,] MapBuildingMaster(TileType[,] mapGrid, List<List<BuildingClass>> buildingsBySize)
        {
            //goes through each set of buildings (large to small) in the buildingsBySize and attempts to place one at random. 
            //once it fails to place, it starts trying to place smaller buildings
            //once every size has failed to be placed, we are doneski
            foreach (List<BuildingClass> buildingSizeSet in buildingsBySize)
            {
                bool canPlaceMore = true;
                int count = buildingSizeSet.Count;
                while (canPlaceMore)
                {
                    canPlaceMore = BuildingsSurveyor(buildingSizeSet[Random.Range(0,count)], ref mapGrid);
                }
                
            }
            return mapGrid;
        }

        //the placer for a specific building type gets that building's grid shape and the mapgrid
        //it scans the whole mapgrid tile by tile, jumping over road/rift/building tiles
        //on valid tile it tries to place build
        static bool BuildingsSurveyor(BuildingClass building, ref TileType[,] mapGrid)
        {
            //loop over mapGrid positions that could possibly hold building
            for (int i = 0; i < mapGrid.GetLength(0) - building.grid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1) - building.grid.GetLength(1); j++)
                {
                    if (mapGrid[i, j] != TileType.baseTile)
                    {
                        continue;
                    }
                    if(PlaceOneBuilding(building, ref mapGrid, i, j))
                    {
                        //we successfully placed the building, so return TRUE to continue placing buildings of this size
                        return true;
                    }
                }
            }
            //we failed! time to start placing smaller buildings!
            return false;
        }

        //makes sure the area is clear for the given building and places it if so
        static bool PlaceOneBuilding(BuildingClass building, ref TileType[,] mapGrid, int xOffset, int yOffset)
        {
            TileType[,] temp = (TileType[,])mapGrid.Clone();
            //loop over mapGrid positions that could possibly hold building
            for (int i = 0; i < building.grid.GetLength(0); i++)
            {
                for (int j = 0; j < building.grid.GetLength(1); j++)
                {
                    //if the building we're trying to place overlaps key map objects
                    if (mapGrid[i + xOffset, j + yOffset] != TileType.baseTile && building.grid[i, j] != TileType.none)
                    {
                        return false;
                    }
                    //dont overwrite tempGrid with building nulls
                    if(building.grid[i, j] == TileType.none)
                    {
                        continue;
                    }
                    temp[i + xOffset, j + yOffset] = building.grid[i, j];
                }
            }
            mapGrid = (TileType[,])temp.Clone();
            return true;
        }
    }
}
