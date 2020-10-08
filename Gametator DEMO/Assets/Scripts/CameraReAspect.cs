using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReAspect : MonoBehaviour
{

    public float targetRatio = 9f / 16f; //The aspect ratio of your game
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.aspect = targetRatio;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
