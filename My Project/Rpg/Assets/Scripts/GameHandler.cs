using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CameraController cameraFollow;
    private float zoom = 5f;

    void Start()
    {
        cameraFollow.Setup(() => zoom);
    }

    // Update is called once per frame
    void Update()
    {
        cameraFollow.Setup(() => zoom);
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ZoomOut();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ZoomIn();
        }
    }

    private void ZoomIn()
    {
        zoom -= 1f;
        if (zoom < 4f) zoom = 4f;
    }
    private void ZoomOut()
    {
        zoom += 1f;
        if (zoom > 10f) zoom = 10f;
    }
}
