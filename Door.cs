using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    private BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Use()
    {
        if (isOpen == true)
        {
            MakeDoor("close");
        }
        else MakeDoor("open");
    }

    private void MakeDoor(string openOrClose)
    {
        bool doorState = SetDoorState(openOrClose);

        animator.SetTrigger(openOrClose);
        animator.SetBool("isOpen", doorState);
        boxCollider.enabled = !doorState;
    }

    private bool SetDoorState(string state)
    {
        if (state == "open") return true;
        else if (state == "close") return false;
        else {
            Debug.Log("SetDoorState function received invalid door state name. Returning default value.");
            return false;
        }
    }

    private void OpenDoor()
    {
        animator.SetTrigger("open");
        animator.SetBool("isOpen", true);
        boxCollider.enabled = false;
    }

    private void CloseDoor()
    {
        animator.SetTrigger("close");
        animator.SetBool("isOpen", false);
        boxCollider.enabled = true;
    }
}
