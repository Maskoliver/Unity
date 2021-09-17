using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private PlayerStats player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemsParent");
        itemSlotTemplate = itemSlotContainer.Find("InventorySlot");
    }

    public void SetPlayer(PlayerStats player)
    {
        this.player = player;
    }

    public void setInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnOtemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender , System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {

        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 128f;
        foreach(Item item in inventory.getItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y*itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Item_Icon").GetComponent<Image>();
            image.sprite = item.getSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("ItemAmount-text").GetComponent<TextMeshProUGUI>();
                
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if (x > 5) {
                x = 0;
                y++;
            }
        }
    }
}
