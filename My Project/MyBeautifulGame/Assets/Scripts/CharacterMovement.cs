using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CharacterMovement : MonoBehaviour
{
    public int movementPoints = 100;
    public PathNode[] path;
    public Tilemap pathmap;
    [SerializeField]private float speed = 5f;
    private int pathIndex = 0;


    public void Move()
    {
        if(path != null) {
            if (pathIndex <= path.Length - 1)
            {

                Vector3 target = new Vector3(path[pathIndex].xCor, path[pathIndex].yCor, 0);
                Vector3 targetOnTilemap = pathmap.CellToWorld(new Vector3Int((int)target.x, (int)target.y, 0)) + new Vector3(0, 0.75f, 0);
                transform.position = Vector3.MoveTowards(transform.position, targetOnTilemap, speed * Time.deltaTime);
                if (transform.position == targetOnTilemap)
                {
                    pathIndex += 1;
                }
            }
            else if(pathIndex == path.Length-1)
            {
                pathIndex = 0;
                path = null;
            }
        }
    }

    public void setPath(PathNode[] paths)
    {
        pathIndex = 0;
        path = null;
        path = paths;
       
    }
    public void Update()
    {
        Move();
    }
}
