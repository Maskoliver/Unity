using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public PathNode[,] nodes = null;
    public int[,] tileArray = null;
    public GridController gc = null;



    public void prepareNodes()
    {
        createNodeMap();
        //print();
    }

    public void print()
    {
        for (int x = 0; x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                Debug.Log(nodes[x, y].xCor + "," + nodes[x, y].yCor + "," + nodes[x, y].costMult);
            }
        }
    }

    public void setTileArray(int[,] ta)
    {
        tileArray = ta;
    }

    public void createNodeMap()
    {
        Debug.Log(gc.mapWidth + "," + gc.mapHeight);
        nodes = new PathNode[gc.mapWidth, gc.mapHeight];
        for (int x = 0; x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                
                if (tileArray[x, y] == 0)
                {
                    nodes[x, y] = new PathNode(x, y, 0.0f);
                }
                else if (tileArray[x, y] == 1)
                {
                    nodes[x, y] = new PathNode(x, y);
                }
                else if (tileArray[x, y] == 2)
                {
                    nodes[x, y] = new PathNode(x, y, 0.0f);
                }
                else if (tileArray[x, y] == 3)
                {
                    nodes[x, y] = new PathNode(x, y, 1.3f);
                }
                else if (tileArray[x, y] == 4)
                {
                    nodes[x, y] = new PathNode(x, y);
                }
                else if (tileArray[x, y] == 5)
                {
                    nodes[x, y] = new PathNode(x, y);
                }
                else
                {
                    nodes[x, y] = new PathNode(x, y);
                }

            }
        }
    }

    public List<PathNode> FindPath(PathNode[,] nodes, PathNode startPos, PathNode targetPos)
    {
        // find path
        List<PathNode> nodes_path = _ImpFindPath(nodes, startPos, targetPos);

        // convert to a list of points and return
        List<PathNode> ret = new List<PathNode>();
        if (nodes_path != null)
        {
            foreach (PathNode node in nodes_path)
            {
                ret.Add(new PathNode(node.xCor, node.yCor));
            }
        }
        return ret;
    }
    public void printNodes(List<PathNode> pn)
    {
       foreach(PathNode pathnode  in pn)
        {
            Debug.Log(pathnode.xCor + "," + pathnode.yCor + "," + pathnode.costMult);
        }
    }

    private List<PathNode> _ImpFindPath(PathNode[,] nodes, PathNode startNode, PathNode targetNode)
    {
        

        List<PathNode> openSet = new List<PathNode>();
        HashSet<PathNode> closedSet = new HashSet<PathNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            PathNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (PathNode neighbour in GetNeighbours(currentNode))
            {
                if (closedSet.Contains(neighbour) || neighbour.costMult == 0.0f)
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) * (int)(10.0f * neighbour.costMult);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour) )
                        openSet.Add(neighbour);
                }
            }
        }

        return null;
    }
    private static List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }
    private static int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        if(nodeB == null)
        {
            Debug.Log("Here");
        }
        int dstX = Mathf.Abs(nodeA.xCor - nodeB.xCor);
        int dstY = Mathf.Abs(nodeA.yCor - nodeB.yCor);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public List<PathNode> GetNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.xCor + x;
                int checkY = node.yCor + y;

                if (checkX >= 0 && checkX < gc.mapWidth && checkY >= 0 && checkY < gc.mapHeight)
                {
                    neighbours.Add(nodes[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
}
