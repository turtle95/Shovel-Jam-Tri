using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float xOffset = -16;
    public int zOffset = 19;
    public Transform target;
    Vector3 targetPos, smoothedPos;
	public float smoothSpeed = 0.125f;
	Vector3 velocity = Vector3.one;
	// Update is called once per frame
	void FixedUpdate () {
        targetPos = new Vector3(target.position.x - xOffset, target.position.y, zOffset);
        
		smoothedPos = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothSpeed);
		smoothedPos = new Vector3 (targetPos.x, smoothedPos.y, targetPos.z);
		transform.position = smoothedPos;
	}
}
