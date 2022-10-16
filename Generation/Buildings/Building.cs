using TileType = Generation.TileUtilities.TileUtilities.TileType;

namespace Generation.Building
{
    /// <summary>
    /// Represents a building (2d array) 
    /// Provides methods for getting the three rotations of the building
    /// Also the mirror version
    /// </summary>
    public class BuildingClass
    {
        public TileType[,] grid;
        public BuildingClass(TileType[,] tileTypes)
        {
            this.grid = tileTypes;
        }
    }
}
