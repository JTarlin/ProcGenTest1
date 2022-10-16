using System.Collections.Generic;
using UnityEngine;
using Generation.RoadBuilder;
using Generation.BuildingPlacer;
using Generation.TileRenderer;
using Generation.OpenSpaceHandler;
using Generation.RiftPlacer;
using Generation.GridRefiner;
using Generation.Buildings;
using TileType = Generation.TileUtilities.TileUtilities.TileType;
using System.Diagnostics;

public class CityGenerator : MonoBehaviour
{
    //get the tiletypes that are placed by renderer
    public GameObject baseTileObj;
    public GameObject riftTileObj;
    public GameObject cityTileObj;
    public GameObject roadTileObj;
    public GameObject wallTileObj;
    public GameObject floorTileObj;
    public GameObject doorTileObj;
    public GameObject openTileObj;

    //set up tile dictionary to get objects from names
    Dictionary<TileType, GameObject> tileDict;

    //get the buildings

    //get the generation properties from the editor
    public int sizeX, sizeY;
    //road specific
    public int initialWalkerCount = 5;
    public float roadTurnChance = 0.03f; //odds a walker turns left or right on a given step
    public float walkerSpawnChance = 0.07f; //odds a walker spawns a new walker on given step
    public float passThroughChance = 0.7f; //chance a road passes through another road instead of despawning
    //open space specific
    public int openSizeThreshold = 120;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialize tileDict
        tileDict = new Dictionary<TileType, GameObject>() {
            { TileType.baseTile, baseTileObj},
            { TileType.riftTile, riftTileObj},
            { TileType.cityTile, cityTileObj},
            { TileType.roadTile, roadTileObj},
            { TileType.wallTile, wallTileObj},
            { TileType.floorTile, floorTileObj },
            { TileType.doorTile, doorTileObj },
            { TileType.openTile, openTileObj}
        };

        //begin map construction
        //time it
        Stopwatch stopwatch = new();
        stopwatch.Start();
        TileType[,] mapGrid = BuildMapGrid(sizeX,sizeY);
        UnityEngine.Debug.Log($"MapGrid built, time: {stopwatch.ElapsedMilliseconds}");
        mapGrid = RoadBuilder.BuildRoads(mapGrid,
                                         initialWalkerCount,
                                         passThroughChance,
                                         roadTurnChance,
                                         walkerSpawnChance);
        UnityEngine.Debug.Log($"Roads built, time: {stopwatch.ElapsedMilliseconds}");
        mapGrid = RiftPlacer.PlaceRift(mapGrid);
        UnityEngine.Debug.Log($"Rift Placed, time: {stopwatch.ElapsedMilliseconds}");
        mapGrid = OpenSpaceHandler.OpenSpaceFinder(mapGrid, openSizeThreshold);
        UnityEngine.Debug.Log($"Open space handled, time: {stopwatch.ElapsedMilliseconds}");
        TileType[,] fineMapGrid = GridRefiner.RefineGrid(mapGrid, 5);
        UnityEngine.Debug.Log($"Grid refined, time: {stopwatch.ElapsedMilliseconds}");
        fineMapGrid = BuildingPlacer.MapBuildingMaster(fineMapGrid, Buildings.GetBuildings());
        UnityEngine.Debug.Log($"Buildings placed, time: {stopwatch.ElapsedMilliseconds}");
        TileRenderer.TilePlacer(fineMapGrid, tileDict);
        UnityEngine.Debug.Log($"Tiles rendered, time: {stopwatch.ElapsedMilliseconds}");
    }

    TileType[,] BuildMapGrid(int sizeX, int sizeY)
    {
        TileType[,] mapGrid = new TileType[sizeX,sizeY];
        return mapGrid;
    }
}
