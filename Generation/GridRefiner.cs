using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.GridRefiner
{
    public class GridRefiner
    {
        ///The grid refiner takes in a mapGrid and returns a fineMapGrid with FINENESSxFINENESS tiles per mapGrid tile
        public static TileType[,] RefineGrid(TileType[,] mapGrid, int fineness)
        {
            //initialize the fineMapGrid
            TileType[,] fineMapGrid = new TileType[mapGrid.GetLength(0) * fineness, mapGrid.GetLength(1) * fineness];

            for (int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    RefineSingleTile(mapGrid, fineMapGrid, i, j, fineness);
                }
            }

            return fineMapGrid;
        }

        static void RefineSingleTile(TileType[,] mapGrid, TileType[,] fineMapGrid, int x, int y, int fineness)
        {
            for (int i = 0; i < fineness; i++)
            {
                for (int j = 0; j < fineness; j++)
                {
                    fineMapGrid[x*fineness + i, y*fineness + j] = mapGrid[x, y];
                }
            }
        }
    }
}

