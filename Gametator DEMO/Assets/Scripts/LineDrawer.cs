using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class LineDrawer : MonoBehaviour
{

    public GameObject linePrefab;
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    public List<Vector3> fingerPositions = new List<Vector3>();
    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
    private PlayerController playerController;

    public GameObject EndPoint;
    public GameObject player;
    private GameObject[] boxes;



    public static bool isStart = false;
    public static bool isDrawed = false;
    public static bool isMenuOpen = true;

    void Start()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");

    }

    public void setIsDrawed(bool status)
    {
        isDrawed = status;
    }
    public void setisMenuOpen(bool status)
    {
        Debug.Log("Menu Set");
        isMenuOpen = status;
    }

    void Update()
    {
        if (isDrawed)
        {

        }
        else if (isMenuOpen)
        {

        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                playerController.setColliderStatus(false);
                setStatusForBoxes(false);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.tag == "StartPoint")
                    {
                        CreateLine();
                        isStart = true;

                    }
                }

            }
            if (Input.GetMouseButton(0))
            {
                if (isStart)
                {
                    UpdateLine();
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.tag == "EndPoint")
                    {
                        isDrawed = true;
                        fingerPositions.Add(new Vector3(EndPoint.transform.position.x, EndPoint.transform.position.y, EndPoint.transform.position.z));
                        OnNewPathCreated(fingerPositions);
                        playerController.setColliderStatus(true);
                        setStatusForBoxes(true);
                    }
                    else
                    {

                        fingerPositions.Clear();
                        lineRenderer.positionCount = fingerPositions.Count;
                        lineRenderer.SetPositions(fingerPositions.ToArray());

                    }
                    isStart = false;
                }
            }
        }
    }

    public void setStatusForBoxes(bool status)
    {
        if (boxes.Length > 0)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].GetComponent<Collider>().enabled = status;
                boxes[i].GetComponent<Rigidbody>().useGravity = status;
                Material myMaterial = boxes[i].GetComponent<MeshRenderer>().material;
                Color colorOrig = myMaterial.color;
                if (status)
                {
                    Color colorTrans = new Color(colorOrig.r, colorOrig.g, colorOrig.b, 1f);
                    myMaterial.color = colorTrans;

                }
                else
                {
                    Color colorTrans = new Color(colorOrig.r, colorOrig.g, colorOrig.b, 0.5f);
                    myMaterial.color = colorTrans;
                }
            }
        }
    }

    public void Awake()
    {
        playerController = player.GetComponent<PlayerController>();

        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
    }

    void CreateLine()
    {
        fingerPositions.Clear();
    }

    void UpdateLine()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.tag == "DrawingArea")
            {
                if (DistanceToLastPoint(hitInfo.point) > .07f)
                {
                    fingerPositions.Add(hitInfo.point);
                    lineRenderer.positionCount = fingerPositions.Count;
                    lineRenderer.SetPositions(fingerPositions.ToArray());
                }
            }
            else
            {
                //NOT in the drawing area
            }

        }

    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if (fingerPositions.Count == 0)
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(fingerPositions.Last(), point);
    }
}