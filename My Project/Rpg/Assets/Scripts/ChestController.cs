using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public bool isInteractable;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Chest opened");
            animator.SetBool("IsOpen", isOpen);
        }
    }

    public void setInteractable(bool state)
    {
        Debug.Log("Entered anim place whit state : " + state);
        isInteractable = state;
        animator.SetBool("interact", state);
        
    }
}
