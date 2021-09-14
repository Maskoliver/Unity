using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField][Range(0.01f ,1f)] private float smoothSpeed = 0.125f;
    [SerializeField] public Vector3 offset;
    private Func<float> GetCameraZoomFunc;
    private Camera myCamera;


    public void Setup( Func<float> GetCameraZoomFunc)
    {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }

    private void Start()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void Update()
    {
      
        HandleZoom();
    }

    private void HandleZoom()
    {
        float cameraZoom = GetCameraZoomFunc();

        float cameraZoomDifference = cameraZoom - myCamera.orthographicSize;
        float cameraZoomSpeed = 2f;

        myCamera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;

        if (cameraZoomDifference > 0)
        {
            if (myCamera.orthographicSize > cameraZoom)
            {
                myCamera.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (myCamera.orthographicSize < cameraZoom)
            {
                myCamera.orthographicSize = cameraZoom;
            }
        }
    }
}
