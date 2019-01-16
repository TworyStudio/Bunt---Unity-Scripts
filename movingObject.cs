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

    protected IEnumerator SmoothMovement(Vector3 destination)
    {
        frameTime = Time.deltaTime;
        step = moveTime * frameTime;
        currentPosition = rb2D.position;
        float remainingDistanceSquared = GetSquareOfRemainingDistance(destination);

        while(remainingDistanceSquared < float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, destination, step);
            MoveTo(newPosition);
            float squareOfRemainingDistance = (transform.position - destination).sqrMagnitude;
            yield return null; //wait for a frame before reeavaulating the loop
        }
    }
    
    private float GetSquareOfRemainingDistance(Vector3 endpoint)
    {
        float squareOfRemainingDistance = (transform.position - endpoint).sqrMagnitude;
        return squareOfRemainingDistance;
    }

    private void MoveTo(Vector3 coordinates)
    {
        rb2D.MovePosition(coordinates);
    }
}
