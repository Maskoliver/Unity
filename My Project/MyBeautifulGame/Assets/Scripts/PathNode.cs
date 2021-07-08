using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    public int xCor;
    public int yCor;

    public int gCost;
    public int hCost;
    public float costMult;

    public PathNode parent;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    
    public PathNode(int x , int y)
    {
        xCor = x;
        yCor = y;
        costMult = 1.0f;
    }

    public PathNode(int x, int y , float cost)
    {
        xCor = x;
        yCor = y;
        costMult = cost;

    }


    

}
