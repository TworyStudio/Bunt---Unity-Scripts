using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCharacterController_v3 : MonoBehaviour
{
    private Rigidbody2D body;
    float horizontalInput;
    float verticalInput;
    public float movementSpeed;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        manageMovement(horizontalInput, verticalInput);
    }

    private void manageMovement(float x, float y)
    {
        if (x != 0)
        {
            moveHorizontally();
        }

        if (y != 0)
        {
            moveVertically();
        }

        if (x == 0 && y == 0)
        {
            stopMovement();
        }
    }

    private Vector2 moveHorizontally()
    {
        return body.velocity = new Vector2((horizontalInput * movementSpeed), 0);
    }

    private Vector2 moveVertically()
    {
        return body.velocity = new Vector2(0, (verticalInput * movementSpeed));
    }

    private Vector2 stopMovement()
    {
        return body.velocity = new Vector2(0, 0);
    }
}
