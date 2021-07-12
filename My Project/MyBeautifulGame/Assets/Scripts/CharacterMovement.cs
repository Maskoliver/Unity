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

    private Vector3 endPos = new Vector3(0,0);


    public bool Move()
    {
        if(path != null) {
            endPos.x = path[path.Length - 1].xCor;
            endPos.y = path[path.Length - 1].yCor;
            Debug.Log(pathIndex + "," + (path.Length));
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
           
            else if(pathIndex == path.Length)
            {
                Debug.Log("entered");
                pathIndex = 0;
                path = null;
                return true;
            }
        }
        return false;
    }

    public void setPath(PathNode[] paths)
    {

        pathIndex = 0;
        path = null;
        path = paths;
       
    }
    public void Update()
    {
        bool isFinished = Move();
        if (isFinished)
        {
            Debug.Log("here");
            GridController gc = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridController>();
            gc.setStartPos((int)endPos.x, (int)endPos.y);
            Vector3 targetOnTilemap = pathmap.CellToWorld(new Vector3Int((int)endPos.x, (int)endPos.y, 0)) + new Vector3(0, 0.75f, 0);
            transform.position = targetOnTilemap;
        }
    }
}
