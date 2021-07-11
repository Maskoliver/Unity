using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tile Grass_Base = null;
    public Tile Sand_Base = null;
    public Tile Water_Base = null;
    public Tile Mountain_1 = null;

    public Tile HighLight = null;
    public Tile Debug_Road = null;

    public AnimatedTile Grass_Flowers;
    public AnimatedTile Grass_Rock;


    public Tilemap Sea_Map;
    public Tilemap Ground_Map;
    public Tilemap Collision_Map;
    public Tilemap Interactables_Map;
    public Tilemap Debug_Map;
}
