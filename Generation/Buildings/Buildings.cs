using tile = Generation.TileUtilities.TileUtilities.TileType;
using Generation.Building;
using System.Collections.Generic;

namespace Generation.Buildings
{
    public class Buildings
    {
        public static List<List<BuildingClass>> GetBuildings()
        {
            List<BuildingClass> listBig = new() { houseFourRooms0, houseFourRooms1, houseFourRooms2, houseFourRooms3 };
            List<BuildingClass> listMed = new() { house2, house3, house4, house5 };
            List<BuildingClass> listSmall = new() { house0, house1, threeShops0, threeShops1 };

            List<List<BuildingClass>> list = new() { listBig, listMed,listSmall };
            return list;
        }
        //sets the grid to a new grid, rotated 90deg
        static tile[,] Rotate90(tile[,] grid)
        {
            //gridlength variables
            int lenX = grid.GetLength(0);
            int lenY = grid.GetLength(1);
            //new temp grid (avoid changing grid reference during loop)
            tile[,] temp = new tile[lenY, lenX];
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    //90deg rotation achieved by transposing and flipping columns
                    temp[j, i] = grid[i, lenY - 1 - j];
                }
            }
            return temp;
        }

        //sets the grid to a new grid, rotated 180deg
        static tile[,] Rotate180(tile[,] grid)
        {
            //gridlength variables
            int lenX = grid.GetLength(0);
            int lenY = grid.GetLength(1);
            //new temp grid (avoid changing grid reference during loop)
            tile[,] temp = new tile[lenX, lenY];
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    //90deg rotation achieved by flipping rows and flipping columns
                    temp[i, j] = grid[lenX - 1 - i, lenY - 1 - j];
                }
            }
            return temp;
        }

        //sets the grid to a new grid, rotated 180deg
        static tile[,] Rotate270(tile[,] grid)
        {
            //gridlength variables
            int lenX = grid.GetLength(0);
            int lenY = grid.GetLength(1);
            //new temp grid (avoid changing grid reference during loop)
            tile[,] temp = new tile[lenY, lenX];
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    //90deg rotation achieved by transposing and flipping rows
                    temp[j, i] = grid[lenX - 1 - i, j];
                }
            }
            return temp;
        }

        //sets the grid to a new grid, mirrored
        static tile[,] Mirror(tile[,] grid)
        {
            //gridlength variables
            int lenX = grid.GetLength(0);
            int lenY = grid.GetLength(1);
            //new temp grid (avoid changing grid reference during loop)
            tile[,] temp = new tile[lenX, lenY];
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    //90deg rotation achieved by flipping rows
                    temp[i, j] = grid[lenX - 1 - i, j];
                }
            }
            return temp;
        }


        ////ALL THE BUILDINGS
        //quick definitions for tileTypes
        static tile wall = tile.wallTile;
        static tile floor = tile.floorTile;
        static tile door = tile.doorTile;
        static tile none = tile.none;
        //10x9 box house
        static readonly tile[,] houseBox = new tile[,]{
            { wall, wall, wall, wall,wall,wall, wall, wall, wall,wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall },
            { wall, wall, wall, wall,wall,wall, wall, wall, wall,wall },
        };
        readonly static BuildingClass house0 = new(houseBox);
        readonly static BuildingClass house1 = new(Rotate90(houseBox));

        static readonly tile[,] houseEllGrid = new tile[,]{
            { wall, wall, wall, wall,wall,wall, wall, wall, wall,wall ,none,none,none,none,none},
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,none,none,none,none,none },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,none,none,none,none,none },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,none,none,none,none,none },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,wall,wall,wall,wall,wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,wall,floor,floor,floor,wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,floor,floor,floor,floor,wall },
            { wall, floor, floor,floor, floor, floor, floor, floor, floor, wall,wall,floor,floor,floor,wall },
            { wall, wall, wall, wall,wall,wall, wall, wall, wall,wall,wall,wall,wall,wall,wall },
        };
        readonly static BuildingClass house2 = new(houseEllGrid);
        readonly static BuildingClass house3 = new(Rotate90(houseEllGrid));
        readonly static BuildingClass house4 = new(Rotate180(houseEllGrid));
        readonly static BuildingClass house5 = new(Rotate270(houseEllGrid));

        //15x25 house w four rooms
        static readonly tile[,] houseFourRooms = new tile[,]{
            { wall, wall, wall, wall, wall, wall, wall, wall, wall,wall,wall, wall, wall, wall,wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor, wall,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor, wall,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor,door,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor, wall,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor, wall,wall,wall,wall,wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor,floor,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor,floor,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor,floor,floor, floor, floor, wall},
            { wall, floor, floor,floor, floor, floor, wall, floor, floor, floor,floor,floor, floor, floor, wall},
            { wall, wall, wall, door, wall, wall, wall, floor, floor, floor,floor,floor, floor, floor, wall},
            { wall,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,wall},
            { wall,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,wall},
            { wall,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,wall},
            { wall,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,wall},
            { wall,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,wall},
            { wall,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,floor,wall},
            { wall, wall, wall,door, wall, wall, wall, wall, wall,wall,wall, wall, wall, wall,wall},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,floor,floor,floor,floor,floor,floor, wall,none,none,none,none,none,none,none},
            { wall,wall, wall, wall, wall, wall, wall, wall,none,none,none,none,none,none,none},
        };
        readonly static BuildingClass houseFourRooms0 = new(houseFourRooms);
        readonly static BuildingClass houseFourRooms1 = new(Rotate90(houseFourRooms));
        readonly static BuildingClass houseFourRooms2 = new(Rotate180(houseFourRooms));
        readonly static BuildingClass houseFourRooms3 = new(Rotate270(houseFourRooms));

        //long thin collections of shops
        //5x15
        static readonly tile[,] threeShops = new tile[,]{
            { wall, wall, wall, wall,wall},
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall, wall, wall, wall,wall},
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall, wall, wall, wall,wall},
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall,floor,floor,floor,wall },
            { wall, wall, wall, wall,wall},
        };
        readonly static BuildingClass threeShops0 = new(threeShops);
        readonly static BuildingClass threeShops1 = new(Rotate90(threeShops));

    }
}
