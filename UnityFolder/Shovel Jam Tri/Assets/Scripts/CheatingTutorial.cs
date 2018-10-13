using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatingTutorial : MonoBehaviour {

    /// <summary>
    /// This script makes it impossible to avoid the tutorial pickups
    /// </summary>

    public Transform target;
    
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, target.position.y, 0);
	}
}
