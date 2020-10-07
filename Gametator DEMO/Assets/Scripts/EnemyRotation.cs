using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    float myTimer = 12.0f;
    bool done = false;
    public bool isActivated = false;
    void Start()
    {

    }

    public void setActivated(bool status)
    {
        isActivated = status;
    }

    void Update()
    {

        myTimer -= Time.deltaTime;
        if (myTimer <= 0.0f)
        {
            myTimer += 12.0f;
        }
        else if (myTimer <= 1.0f && !done)
        {
            Rotate(90, false);
            done = true;
        }
        else if (myTimer <= 2.0f && !done)
        {

            Rotate(20, true);
            done = true;
        }
        else if (myTimer <= 3.0f && !done)
        {

            Rotate(40, false);
            done = true;

        }
        else if (myTimer <= 4.0f && !done)
        {

            Rotate(20, true);
            done = true;
        }
        else if (myTimer <= 5.0f && !done)
        {

            Rotate(180, false);
            done = true;

        }
        else if (myTimer <= 6.0f && !done)
        {

            Rotate(20, true);
            done = true;
        }
        else if (myTimer <= 7.0f && !done)
        {

            Rotate(40, false);
            done = true;

        }
        else if (myTimer <= 8.0f && !done)
        {

            Rotate(20, true);
            done = true;

        }
        else if (myTimer <= 9.0f && !done)
        {

            Rotate(90, false);
            done = true;

        }
        else if (myTimer <= 10.0f && !done)
        {
            done = true;
            Rotate(20, true);

        }
        else if (myTimer <= 11.0f && !done)
        {
            done = true;
            Rotate(40, false);
        }
        else if (myTimer <= 12.0f && !done)
        {

            done = true;
            Rotate(20, true);
        }
        else { }



    }



    public void Rotate(int rot, bool isLeft)
    {
        if (isLeft)
        {
            StartCoroutine(Rotates(Vector3.up, -rot));
        }
        else
        {
            StartCoroutine(Rotates(Vector3.up, rot));
        }

    }

    IEnumerator Rotates(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        //Debug.Log("1 " + from);
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);
        //Debug.Log("2 " + to);
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
        done = false;
    }


}
