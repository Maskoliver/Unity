using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    public List<Tile> TileList = new List<Tile>();

    public Tile Grass_Base = null;
    public Tile Grass_1 = null;
    public Tile Water_Edge = null;
    public Tile Water_Base = null;
    public Tile Water_Wave = null;
    

    public Tilemap Sea_Map;
    public Tilemap Ground_Map;
    public Tilemap Collision_Map;
    public Tilemap Interactables_Map;
    public Tilemap Debug_Map;
}
