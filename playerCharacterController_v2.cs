using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCharacterController_v2 : MonoBehaviour {

    private Rigidbody2D body;
    public Animator animator;
    float horizontalInput;
    float verticalInput;
    Vector2 movementVector;
    string direction;
    public float movementSpeed;
    private bool isMovingHorizontally;
    private bool isMovingVertically;



    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        isMovingHorizontally = false;
        isMovingVertically = false;
    }

    // Update is called once per frame
    void Update () {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementVector = body.velocity;

        manageMovement(horizontalInput, verticalInput);
        manageAnimation(movementVector);
	}

    private void manageMovement(float x, float y)
    {
        direction = determineDirection(x, y);
        move(direction);
    }

    private string determineDirection(float x, float y)
    {
        x = convertInputToPositiveValue(x)*2;
        y = convertInputToPositiveValue(y)*3;

        if (x + y == 2)
        {
            return "Horizontal";
        }

        else if (x + y == 3)
        {
            return "Vertical";
        }
        else if (x + y == 5)
        {
            return "SwitchAxis";
        }
        else
        {
            return "Stationary";
        }
    }

    private void move(string thisWay)
    {
        if (thisWay == "Horizontal") {
            moveHorizontally();
        }

        else if (thisWay == "Vertical")
        {
            moveVertically();
        }

        else if (thisWay == "SwitchAxis")
        {
            if (isMovingHorizontally)
            {
                moveVerticallyWithoutFlagging();
            }
            else if (isMovingVertically)
            {
                moveHorizontallyWithoutFlagging();
            }
        }

        else
        {
            stopMovement();
        }

    }

    private int convertInputToPositiveValue(float inputValue)
    {
        if (inputValue != 0)
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
        isMovingHorizontally = true;
        isMovingVertically = false;
        return body.velocity = new Vector2((horizontalInput * movementSpeed), 0);
    }

    private Vector2 moveVertically()
    {
        isMovingVertically = true;
        isMovingHorizontally = false;
        return body.velocity = new Vector2(0, (verticalInput * movementSpeed));
    }

    private Vector2 stopMovement()
    {
        isMovingVertically = false;
        isMovingHorizontally = false;
        return body.velocity = new Vector2(0, 0);
    }

    private Vector2 moveHorizontallyWithoutFlagging()
    {
        return body.velocity = new Vector2((horizontalInput * movementSpeed), 0);
    }

    private Vector2 moveVerticallyWithoutFlagging()
    {
        return body.velocity = new Vector2(0, (verticalInput * movementSpeed));
    }

    private void manageAnimation(Vector2 movement)
    {
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);

        if(movement.magnitude != 0)
        {
            animator.SetBool("isMoving", true);
            saveLastDirectionFaced(movement);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void saveLastDirectionFaced(Vector2 directionFaced)
    {
        if(directionFaced.x != 0)
        {
            animator.SetFloat("horizontalDirection", directionFaced.x);
            animator.SetFloat("verticalDirection", 0);
        }
        else {
            animator.SetFloat("verticalDirection", directionFaced.y);
            animator.SetFloat("horizontalDirection", 0);
        }
    }
}
