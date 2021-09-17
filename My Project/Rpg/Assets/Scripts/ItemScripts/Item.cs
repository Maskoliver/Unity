using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        ShortSword,
        LongSword,
        DiamondLongSword,
        Spear,
        HeatlhPotion,
        ManaPotion,
        GoldCoin,
    }

    public ItemType itemType;
    public int amount;


    public Sprite getSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.ShortSword: return ItemAssets.Instance.shortSwordSprite;
            case ItemType.LongSword: return ItemAssets.Instance.longSwordSprite;
            case ItemType.HeatlhPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion: return ItemAssets.Instance.manaPotionSprite;
            case ItemType.GoldCoin: return ItemAssets.Instance.goldCoinSprite;
            case ItemType.Spear: return ItemAssets.Instance.spearSprite;
            case ItemType.DiamondLongSword: return ItemAssets.Instance.diamondLongSwordSprite;
        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.ShortSword:
            case ItemType.DiamondLongSword:
            case ItemType.Spear:
            case ItemType.LongSword:
                return false;
            case ItemType.HeatlhPotion: 
            case ItemType.ManaPotion: 
            case ItemType.GoldCoin: 
                return true;
        }
    }
   
}
