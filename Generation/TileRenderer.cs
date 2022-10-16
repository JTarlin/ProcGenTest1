using System.Collections.Generic;
using UnityEngine;
using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.TileRenderer
{
    public class TileRenderer : MonoBehaviour
    {
        //TILE RENDERING

        //rasters across the mapGrid and places tileObjects where they need to be placed
        public static void TilePlacer(TileType[,] mapGrid, Dictionary<TileType, GameObject> tileDict)
        {
            for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    if (mapGrid[i, j] != TileType.none)
                    {
                        Instantiate(tileDict[mapGrid[i, j]], new Vector3(i, j, 0), Quaternion.identity);
                    }
                    else
                    {
                        //place baseTile on nulls
                        //should NOT occur, just for safety.
                        Instantiate(tileDict[TileType.baseTile], new Vector3(i, j, 0), Quaternion.identity);
                    }
                }
            }
        }
    }
}
