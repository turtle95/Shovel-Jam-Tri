using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public int xOffset = -16;
    public int zOffset = 19;
    public Transform target;
    Vector3 targetPos;
	// Update is called once per frame
	void Update () {
        targetPos = new Vector3(target.position.x - xOffset, target.position.y, zOffset);
        transform.position = targetPos;
	}
}
