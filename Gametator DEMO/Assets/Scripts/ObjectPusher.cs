using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    public Rigidbody rb;
    public Material myMaterial;



    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {


            if (other.gameObject.transform.position.z - gameObject.transform.position.z <= 0)
            {

                gameObject.transform.Translate((new Vector3(0, 0, 1) * Time.deltaTime));
            }
            else if (other.gameObject.transform.position.x - gameObject.transform.position.x <= 0)
            {
                gameObject.transform.Translate((new Vector3(1, 0, 0) * Time.deltaTime));
            }
            else if (other.gameObject.transform.position.x - gameObject.transform.position.x > 0)
            {
                gameObject.transform.Translate((new Vector3(-1, 0, 0) * Time.deltaTime));
            }
            else
            {
                gameObject.transform.Translate((new Vector3(0, 0, -1) * Time.deltaTime));
            }

        }
    }

}
