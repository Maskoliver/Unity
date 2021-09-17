using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private static Transform itemWorldPrefab;
    private TextMeshPro textMeshPro;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item Item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorldPf, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.setItem(Item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        int xRandom = Random.Range(-1, 2);
        int yRandom = Random.Range(-1, 2);
    
        float distance = Random.Range(-1, 1);
        Vector3 dropCircle = new Vector3(xRandom * distance, yRandom * distance, dropPosition.z);
        while(dropCircle+dropPosition == dropPosition)
        {
            xRandom = Random.Range(-1, 2);
            yRandom = Random.Range(-1, 2);
            distance = Random.Range(-1, 1);
            dropCircle = new Vector3(xRandom * distance, yRandom * distance, dropPosition.z);
        }
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + dropCircle, item);
        //itemWorld.GetComponent<Rigidbody2D>().AddForce(new Vector3(xRandom * distance, yRandom * distance, dropPosition.z), ForceMode2D.Impulse);
        return itemWorld;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("AmountText").GetComponent<TextMeshPro>();
    }
    public void setItem(Item Item)
    {
        item = Item;
        spriteRenderer.sprite = Item.getSprite();
        if(item.amount> 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
        
    }

    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
