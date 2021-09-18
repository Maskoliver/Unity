using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromInventory;
    }

    private void EquipFromInventory(Item item)
    {
        if(item is EquipableItems)
        {
            Equip((EquipableItems)item);
        }
    }
    private void UnequipFromInventory(Item item)
    {
        Debug.Log("here");
        if (item is EquipableItems)
        {
            Unequip((EquipableItems)item);
        }
    }
    public void Equip(EquipableItems item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipableItems previousItem;
            if(equipmentPanel.AddItem(item , out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquipableItems item)
    {
        Debug.Log("Entered");
        if(!inventory.isFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
}
