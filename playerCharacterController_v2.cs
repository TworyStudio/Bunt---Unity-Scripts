using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCharacterController_v2 : MonoBehaviour {

    public Animator animator;
    public float movementSpeed;
    public float useActionDuration;

    private Rigidbody2D body;
    private bool isMovingHorizontally;
    private bool isMovingVertically;
    private bool canMove;
    private BoxCollider2D boxCollider;

    int horizontalInput;
    int verticalInput;
    Vector2 movementVector;
    string direction;

    public Transform useActionTarget;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        isMovingHorizontally = false;
        isMovingVertically = false;
        canMove = true;
        useActionDuration = 0.35f; 
    }

    void Update() {
        if (Input.GetButtonDown("Use"))
        {
            StartCoroutine(Use());
            PerformUseAction();
        }
        else if (canMove == true)
        {
            horizontalInput = (int)(Input.GetAxisRaw("Horizontal"));
            verticalInput = (int)(Input.GetAxisRaw("Vertical"));
            movementVector = body.velocity;
            manageMovement(horizontalInput, verticalInput);
        }
        else
        {
            stopMovement();
            movementVector = body.velocity;
        }
        manageAnimation(movementVector);
	}

    IEnumerator Use()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isUsing", true);
        canMove = false;
        stopMovement();
        yield return new WaitForSeconds(useActionDuration);
        canMove = true;
        animator.SetBool("isUsing", false);
        Debug.Log("Use button pressed");
    }

    private void PerformUseAction()
    {
        setOwnBoxColliderStateTo(false);

        float directionX = animator.GetFloat("horizontalDirection");
        float directionY = animator.GetFloat("verticalDirection");

        Vector2 startingPosition = transform.position;

        Vector2 useDirection = new Vector2(0, 0);

        if (directionX < 0)
        {
            useDirection = Vector2.left;
        }
        else if (directionX > 0)
        {
            useDirection = Vector2.right;
        }
        else if (directionY < 0)
        {
            useDirection = Vector2.down;
        }
        else useDirection = Vector2.up;

        RaycastHit2D hit = Physics2D.Raycast(startingPosition, useDirection);
        string hitObjectName = hit.collider.gameObject.name;

        Debug.Log("useDirection = " + useDirection);
        Debug.Log("hitObjectName = " + hitObjectName);

        setOwnBoxColliderStateTo(true);
    }

    private void setOwnBoxColliderStateTo(bool state)
    {
        //Flip the collider on or off to avoid raycasting problems
        boxCollider.enabled = state;
    }

    private void manageMovement(float x, float y)
    {
        direction = determineDirection(x, y);
        move(direction);
        setMovementFlags(direction);
    }

    private string determineDirection(float x, float y)
    //This function allows switching axis while any axis button is already held down.
    //Without it it's possible to switch axis only one way (eg. from vertical to horizontal,
    //but not the other way around) when opposing axis buttons are pressed at the same time
    //This solution is specific for keyboards.
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
                moveVertically();
            }
            else if (isMovingVertically)
            {
                moveHorizontally();
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

    private void setMovementFlags(string movementDirection)
    {
        if(movementDirection == "Horizontal")
        {
            isMovingHorizontally = true;
            isMovingVertically = false;
        }
        else if(movementDirection == "Vertical")
        {
            isMovingHorizontally = false;
            isMovingVertically = true;
        }
        else if(movementDirection == "Stationary")
        {
            isMovingHorizontally = false;
            isMovingVertically = false;
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
