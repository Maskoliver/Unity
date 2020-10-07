using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeggFollow : MonoBehaviour
{
    private Transform target = null;
    private float speed = 0;
    private float targetDist = 1f;
    void Update()
    {
        if (target != null)


            if (Vector3.Distance(transform.position, target.position) > 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .03f);
            }

    }

    public void SetTarget(GameObject newTarget, float chaseSpeed)
    {
        target = newTarget.transform;
        speed = chaseSpeed;
    }
}
