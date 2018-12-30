using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour {

    public GameObject cameraTarget;
    public Vector3 cameraOffset = new Vector3(0,0,-1);
    private Vector3 cameraTargetVector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        trackCameraTo(cameraTarget);
    }

    private void trackCameraTo(GameObject target)
    {
        Vector2 targetPosition = target.transform.position;
        cameraTargetVector = new Vector3(targetPosition.x, targetPosition.y, 0);

        transform.position = cameraTargetVector + cameraOffset;
    }
}
