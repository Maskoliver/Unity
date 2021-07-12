using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapGenerationTool : MonoBehaviour
{
    public InputField widthField = null;
    public InputField heightField = null;
    public InputField seedField = null;
    public InputField scaleField = null;
    public InputField sealevelField = null;

    private GridController gc;
    private void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridController>();
    }
    private void Start()
    {
        widthField.text = gc.mapWidth.ToString();
        heightField.text = gc.mapHeight.ToString();
        seedField.text = gc.seed.ToString();
        scaleField.text = gc.scale.ToString();
        sealevelField.text = gc.sealevel.ToString();
    }
}
