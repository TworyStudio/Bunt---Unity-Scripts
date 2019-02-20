using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen;
    public Animator animator;
    public float force;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Use()
    {
        isOpen = animator.GetBool("isOpen");
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
        boxCollider.isTrigger = doorState;
    }

    private bool SetDoorState(string state)
    {
        if (state == "open") return true;
        else if (state == "close") return false;
        else
        {
            Debug.Log("SetDoorState function received invalid door state name. Returning default value.");
            return false;
        }
    }
}
