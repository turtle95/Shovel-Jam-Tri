using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    public float speed = 5f;
    public Rigidbody playerRb;
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(-playerRb.velocity.y, -playerRb.velocity.x, 0) * Time.deltaTime /8);	
	}
}
