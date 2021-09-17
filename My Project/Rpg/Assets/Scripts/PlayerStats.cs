using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.setInventory(inventory);
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HeatlhPotion:
                Debug.Log("Drinked Health Potion");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HeatlhPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
                Debug.Log("Drinked Mana Potion");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
        }
    }
}

