using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.OpenSpaceHandler
{
    /// <summary>
    /// Finds and appropriately labels open spaces in the mapGrid
    /// </summary>
    public class OpenSpaceHandler
    {
        /// <summary>
        /// Find open spaces and mark accordingly.
        /// </summary>
        public static TileType[,] OpenSpaceFinder(TileType[,] mapGrid, int threshold)
        {
            for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    if (mapGrid[i, j] != TileType.none)
                    {
                        continue;
                    }
                    else
                    {
                        //mark open space accordingly
                        if(RecursiveOpenFinder(mapGrid, i, j) > threshold)
                        {
                            RecursiveOpenMarker(mapGrid, i, j);
                        }
                    }
                }
            }
            return mapGrid;
        }

        /// <summary>
        /// Gets the area of a continuous null space on the grid.
        /// Marks all touched tiles as "base".
        /// </summary>
        static int RecursiveOpenFinder(TileType[,] mapGrid, int i, int j)
        {
            if (i>=mapGrid.GetLength(0) || j>=mapGrid.GetLength(1)||i<0||j<0|| mapGrid[i, j] != TileType.none)
            {
                return 0;
            } else
            {
                mapGrid[i, j] = TileType.baseTile;
                return 1 + RecursiveOpenFinder(mapGrid, i, j + 1) +
                           RecursiveOpenFinder(mapGrid, i, j - 1) +
                           RecursiveOpenFinder(mapGrid, i + 1, j) +
                           RecursiveOpenFinder(mapGrid, i - 1, j);
            }
        }

        /// <summary>
        /// Gets marks all base tiles in continuous space with "largeOpen"
        /// </summary>
        static void RecursiveOpenMarker(TileType[,] mapGrid, int i, int j)
        {
            if (i >= mapGrid.GetLength(0) || j >= mapGrid.GetLength(1) || i < 0 || j < 0|| mapGrid[i, j] != TileType.baseTile)
            {
                return;
            }
            else
            {
                mapGrid[i, j] = TileType.openTile;
                RecursiveOpenMarker(mapGrid, i, j + 1);
                RecursiveOpenMarker(mapGrid, i, j - 1);
                RecursiveOpenMarker(mapGrid, i + 1, j);
                RecursiveOpenMarker(mapGrid, i - 1, j);
            }
            return;
        }
    }
}

