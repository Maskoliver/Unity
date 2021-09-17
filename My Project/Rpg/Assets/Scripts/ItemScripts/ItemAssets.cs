using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Debug.Log("i am awake");
        Instance = this;
    }

    public Transform ItemWorldPf;

    public Sprite longSwordSprite;
    public Sprite diamondLongSwordSprite;
    public Sprite spearSprite;
    public Sprite shortSwordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite goldCoinSprite;

}
