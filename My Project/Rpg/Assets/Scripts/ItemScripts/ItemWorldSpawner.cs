using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        Debug.Log("Spawned " + transform.position + " and item is " + item.itemType);
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
