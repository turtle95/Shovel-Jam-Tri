using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrillMove : MonoBehaviour {

    /// <summary>
    /// The krill will face left on the screen and move in that direction so that they leave behind a particle trail
    /// </summary>


    Vector3 target;

    public float speed = 5f;

	// Use this for initialization
	void Start () {
        target = new Vector3(transform.position.x + 50, transform.position.y, 0);
        transform.LookAt(target);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-transform.right * speed * Time.deltaTime);
	}
}
