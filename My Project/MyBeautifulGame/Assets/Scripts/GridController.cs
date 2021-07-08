using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.UI;
public class GridController : MonoBehaviour
{
    private Grid grid;

    [SerializeField] private Tilemap[] Mapset = null;
    [SerializeField] private Tile[] Tileset = null;
    [SerializeField] private AnimatedTile flowers = null;
    [SerializeField] private AnimatedTile rocks = null;

    private bool isStart = true;

    public int mapWidth = 20;
    public int mapHeight = 20;
    public int seed = 0;
    public float scale = 10.0f;
    public float sealevel = 0.5f;

    public InputField widthField = null;
    public InputField heightField = null;
    public InputField seedField = null;
    public InputField scaleField = null;
    public InputField sealevelField = null;

    private List<PathNode> debugPath = null;

    public int[,] tileArray = null;
    private PathFinder pf = null;

    private Vector3Int previousMousePos = new Vector3Int();


    private Vector2 startPosition = new Vector2(1, 1);

    // Start is called before the first frame update
    void Start()
    {
        widthField.text = mapWidth.ToString();
        heightField.text = mapHeight.ToString();
        seedField.text = seed.ToString();
        scaleField.text = scale.ToString();
        sealevelField.text = sealevel.ToString();

        pf = this.GetComponent<PathFinder>();
        grid = gameObject.GetComponent<Grid>();

        GenerateMap();

    }

    public void GenerateMap()
    {
        if (!isStart)
        {

            mapWidth = int.Parse(widthField.text);
            mapHeight = int.Parse(heightField.text);
            seed = int.Parse(seedField.text);
            scale = float.Parse(scaleField.text);
            sealevel = float.Parse(sealevelField.text);

            tileArray = GenerateArray(int.Parse(widthField.text) , int.Parse(heightField.text), int.Parse(seedField.text), float.Parse(scaleField.text), float.Parse(sealevelField.text));
        }
        else { 
            tileArray = GenerateArray(mapWidth, mapHeight, seed, scale, sealevel);
            isStart = false;
        }
        tileArray = addSand(tileArray);
        RenderMap(tileArray, Mapset, Tileset,flowers,rocks);
        pf.setTileArray(tileArray);
        pf.prepareNodes();
    }

    public void clearMap()
    {
        Tilemap sea = (Tilemap)Mapset.GetValue(1);
        Tilemap ground = (Tilemap)Mapset.GetValue(0);
        Tilemap mountains = (Tilemap)Mapset.GetValue(2);
        ground.ClearAllTiles();
        sea.ClearAllTiles();
        mountains.ClearAllTiles();
    }

    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            Tilemap interactive = (Tilemap)Mapset.GetValue(3);          
            interactive.SetTile(new Vector3Int(previousMousePos.x, previousMousePos.y, 0), null); // Remove old hoverTile
            interactive.SetTile(new Vector3Int(mousePos.x, mousePos.y, 0), (Tile)Tileset.GetValue(3));
            previousMousePos = mousePos;
        }

        //Left mouse click->add path tile
        if (Input.GetMouseButton(0))
        {
            Tilemap pathmap = (Tilemap)Mapset.GetValue(0);
            //Debug.Log("Created tile at : " + mousePos);
            //pathmap.SetTile(new Vector3Int(mousePos.x,mousePos.y,0), (Tile)Tileset.GetValue(0));
            
            debugPath = pf.FindPath(pf.nodes, new PathNode((int)startPosition.x, (int)startPosition.y), pf.nodes[mousePos.x, mousePos.y]);
            //pf.printNodes(debugPath);
            makeDebugPath(debugPath);
        }

        // Right mouse click -> remove path tile
        if (Input.GetMouseButton(1))
        {
            Tilemap pathmap = (Tilemap)Mapset.GetValue(0);
            Tilemap debugmap = (Tilemap)Mapset.GetValue(4);
            debugmap.SetTile(new Vector3Int((int)startPosition.x, (int)startPosition.y, 0),null);
            startPosition.x = mousePos.x;
            startPosition.y = mousePos.y;
            debugmap.SetTile(new Vector3Int(mousePos.x, mousePos.y, 0), (Tile)Tileset.GetValue(3));
            //Debug.Log("Deleted tile");
            //pathmap.SetTile(new Vector3Int(mousePos.x, mousePos.y, 0), null);
        }
    }

    private void makeDebugPath(List<PathNode> debugpath)
    {
        
        Tilemap debugmap = (Tilemap)Mapset.GetValue(4); //Debug map
        debugmap.ClearAllTiles();
        foreach (PathNode node in debugPath)
        {
            debugmap.SetTile(new Vector3Int(node.xCor, node.yCor, 0), (Tile)Tileset.GetValue(6));
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }


    public static int[,] GenerateArray(int width, int height, int seed,float scale,float sealevel)
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
                
             
                if (sample > sealevel && sample <= sealevel+0.35f)
                {
                   
                    map[x, y] = 1; //GROUND
                    float random = Random.Range(0.0f, 1.0f); 
                   
                    if (random <= 0.2f)
                    {
                        map[x, y] = 4; //FLOWERS
                    }
                    else if (random >= 0.27f && random <= 0.3f)
                    {
                        map[x, y] = 5; //ROCKS
                    }

                }
                else if(sample > sealevel + 0.35f)
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

    public static int[,] addSand(int[,] map)
    {
        for (int x = 0; x < map.GetLength(0)-1; x++)
        {
            for (int y = 0; y < map.GetLength(1)-1; y++)
            {
                //&& x != map.GetUpperBound(0)-1 && y != map.GetUpperBound(1)-1
                if (x - 1 >= 0 && y-1 >= 0 && map[x,y] != 0 )
                {
                    if (map[x + 1, y] == 0)
                    {
                        map[x , y] = 3;
                    }
                    else if (map[x - 1, y] == 0)
                    {
                        map[x , y] = 3;
                    }
                    else if (map[x, y + 1] == 0)
                    {
                        map[x, y ] = 3;
                    }
                    else if (map[x, y - 1] == 0)
                    {
                        map[x, y ] = 3;
                    }
                    else if (map[x + 1, y + 1] == 0)
                    {
                        map[x, y] = 3;
                    }
                    else if (map[x +1 , y - 1] == 0)
                    {
                        map[x, y] = 3;
                    }
                    else if (map[x -1 , y - 1] == 0)
                    {
                        map[x, y] = 3;
                    }
                    else if (map[x - 1, y + 1] == 0)
                    {
                        map[x, y] = 3;
                    }
                }
            }
        }
        return map;
    }
  

    public static void RenderMap(int[,] map, Tilemap[] Mapset,Tile[] Tileset,AnimatedTile flower,AnimatedTile rock)
    {
        //Clear the map (ensures we dont overlap)
        Tilemap sea = (Tilemap)Mapset.GetValue(1);
        Tilemap ground = (Tilemap)Mapset.GetValue(0);
        Tilemap mountains = (Tilemap)Mapset.GetValue(2);
        ground.ClearAllTiles();
        sea.ClearAllTiles();
        mountains.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetLength(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetLength(1); y++)
            {
             
                if (map[x, y] == 1)
                {
                    ground.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(0));
                }
                else if(map[x,y] == 2)
                {
                    mountains.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(2));
                    ground.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(0));
                }
                else if (map[x, y] == 3)
                {
                   
                    ground.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(4));
                }
                else if (map[x, y] == 4)
                {
                    mountains.SetTile(new Vector3Int(x, y, 0), flower);
                    ground.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(0));
                }
                else if (map[x, y] == 5)
                {
                    mountains.SetTile(new Vector3Int(x, y, 0), rock);
                    ground.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(0));
                }
                else
                {
                    sea.SetTile(new Vector3Int(x, y, 0), (Tile)Tileset.GetValue(1));
                }
            }
        }
    }

 
}
