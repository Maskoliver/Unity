using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{

    public bool isInRange;
    public KeyCode interactKey;
   
    private ChestController cc;
    // Start is called before the first frame update
    void Start()
    {
       cc = gameObject.GetComponentInParent<ChestController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            cc.setInteractable(true);
            if (Input.GetKeyDown(interactKey))
            {
                cc.openChest();
               
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                cc.closeChest();
            }
        }
        else
        {
            if (cc.isInteractable)
            {
                cc.setInteractable(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cc.closeChest();
            Debug.Log("Player leaved range");
            isInRange = false;
        }
    }
}
