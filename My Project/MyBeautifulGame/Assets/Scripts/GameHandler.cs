using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CameraController cameraFollow;
    private Vector3 cameraFollowPosition;
    private float zoom = 4f;
    void Start()
    {
        cameraFollow.Setup(() => cameraFollowPosition, () => zoom);    
    }

   
    void Update()
    {
        cameraFollow.Setup(() => cameraFollowPosition, () => zoom);
        float moveAmount = 10f;
        if (Input.GetKey(KeyCode.W))
        {
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ZoomOut();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ZoomIn();
        }
    }

   
    private void ZoomIn()
    {
        zoom -= 1f;
        if (zoom < 2f) zoom = 2f;
    }
    private void ZoomOut()
    {
        zoom += 1f;
        if (zoom > 10f) zoom = 10f;
    }
}
