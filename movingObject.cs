using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = .1f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private float step;
    private float frameTime;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

   
    private void setOwnBoxcolliderStateTo(bool state)
    {
        //Flip the collider on or off to avoid raycasting problems
        boxCollider.enabled = state;
    }

    private void MoveTo(Vector3 coordinates)
    {
        rb2D.MovePosition(coordinates);
    }

     protected virtual void AttemptMove <T> (int xDirection, int yDirection)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = checkIfCanMoveInDirection(xDirection, yDirection, out hit);

        if (PathIsClear(hit)) {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();
        
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected bool checkIfCanMoveInDirection(int movementX, int movementY, out RaycastHit2D hit)
    {
        Vector2 startingPosition = transform.position;
        Vector2 movementVector = new Vector2 (movementX, movementY);
        Vector2 endingPosition = startingPosition + movementVector;
        
        setOwnBoxcolliderStateTo(false);

        hit = Physics2D.Linecast(startingPosition, endingPosition, blockingLayer);

        setOwnBoxcolliderStateTo(true);

        if (PathIsClear(hit))
        {
            StartCoroutine(SmoothMovement(endingPosition));
            return true;
        }
        return false;
    }

    private bool PathIsClear(RaycastHit2D obstacle)
    {
        if (obstacle.transform == null)
        {
            return true;
        }
        else return false;
    }

    protected abstract void OnCantMove<T>(T component)
    where T : Component;

    protected IEnumerator SmoothMovement(Vector3 destination)
    {
        frameTime = Time.deltaTime;
        step = inverseMoveTime * frameTime;
        currentPosition = rb2D.position;
        float remainingDistanceSquared = GetSquareOfRemainingDistanceTo(destination);

        while(remainingDistanceSquared < float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, destination, step);
            MoveTo(newPosition);
            float squareOfRemainingDistance = (transform.position - destination).sqrMagnitude;
            yield return null; //wait for a frame before reeavaulating the loop
        }
    }

    private float GetSquareOfRemainingDistanceTo(Vector3 endpoint)
    {
        float squareOfRemainingDistance = (transform.position - endpoint).sqrMagnitude;
        return squareOfRemainingDistance;
    }
}
