using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapGenerationTool : MonoBehaviour
{

    public int[,] tileArray = null;

    public int mapWidth = 100;
    public int mapHeight = 100;
    public int seed = 0;
    public float scale = 10.0f;
    public float sealevel = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateMap()
    {
        tileArray = GenerateArray(mapWidth, mapHeight, seed, scale, sealevel);

        RenderMap(tileArray);
    }

        public static int[,] GenerateArray(int width, int height, int seed, float scale, float sealevel)
    {

        float xCoord = 0;
        float yCoord = 0;
        int[,] map = new int[width, height];

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                xCoord = seed + x / scale;
                yCoord = seed + y / scale;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);


                if (sample > sealevel && sample <= sealevel + 0.35f)
                {

                    map[x, y] = 1; //GROUND
                    float random = Random.Range(0.0f, 1.0f);

                    if (random <= 0.25f)
                    {
                        map[x, y] = 4; //FLOWERS
                    }
                

                }
                else if (sample > sealevel + 0.35f)
                {
                    map[x, y] = 2; //MOUNTAINS
                }

                else
                {
                    map[x, y] = 0;//SEA
                }


            }
        }

        return map;
    }
    public static void RenderMap(int[,] map)
    {
        TileManager tm = GameObject.FindGameObjectWithTag("GameController").GetComponent<TileManager>();
        //Clear the map (ensures we dont overlap)
        Tilemap sea = tm.Sea_Map;
        Tilemap ground = tm.Ground_Map;
        ground.ClearAllTiles();
        sea.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetLength(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetLength(1); y++)
            {

                if (map[x, y] == 1)
                {
                    ground.SetTile(new Vector3Int(x, y, 0), tm.Grass_Base);
                }
                else if (map[x, y] == 2)
                {
                    ground.SetTile(new Vector3Int(x, y, 0), tm.Grass_Base);
                }
                else if (map[x, y] == 4)
                {
                    ground.SetTile(new Vector3Int(x, y, 0), tm.Grass_1);
                }
                else
                {
                    float random = Random.Range(0.0f, 1.0f);

                    if (random <= 0.49f)
                    {
                        sea.SetTile(new Vector3Int(x, y, 0), tm.Water_Base);
                    }
                    else 
                    {
                        sea.SetTile(new Vector3Int(x, y, 0), tm.Water_Wave);
                    }
                  
                }
            }
        }
    }
}
