using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCharacterController : MonoBehaviour
{

    private Rigidbody2D body;
    float horizontalInput;
    float verticalInput;
    string lastInput;
    public float movementSpeed;
    bool switchDirectionCheck;

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
        lastInput = lastDirectionalKeyPress();
        switchDirectionCheck = switchDirection(horizontalInput, verticalInput);
    }

    private void FixedUpdate()
    {
        manageMovement(lastInput, switchDirectionCheck);
    }

    private void manageMovement(string input, bool shouldSwitchDirection)
    {
        if (input == "Horizontal" && !shouldSwitchDirection)
        {
            Debug.Log("Inside first if.");
            moveHorizontally();
        }

        else if (input == "Horizontal" && shouldSwitchDirection)
        {
            Debug.Log("Inside second if.");
            moveVertically();
        }

        else if (input == "Vertical" && !shouldSwitchDirection)
        {
            Debug.Log("Inside third if.");
            moveVertically();
        }

        else if (input == "Vertical" && shouldSwitchDirection)
        {
            Debug.Log("Inside fourth if.");
            moveHorizontally();
        }

        else
        {
            stopMovement();
        }
    }

    private string lastDirectionalKeyPress()
    {
        if (Input.GetButton("Vertical"))
        {
            return "Vertical";
        }
        else if (Input.GetButton("Horizontal"))
        {
            return "Horizontal";
        }
        else
        {
            return "Stationery";
        }
    }

    private bool switchDirection(float x, float y)
    {
        x = convertInputToPositiveValue(x);
        y = convertInputToPositiveValue(y);
        
        if (x + y == 2)
        {
 //           Debug.Log("switchDirection: True");
            return true;
        }
        else
        {
 //           Debug.Log("switchDirection: False");
            return false;
        }
    }

    private int convertInputToPositiveValue(float inputValue)
    {
        if(inputValue != 0)
        {
            return 1;
        }
        else
        {
            return 0;
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
