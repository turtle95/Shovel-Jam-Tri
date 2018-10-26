using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour {

    public short launchSpeed = 30;
	
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * launchSpeed * Time.deltaTime);
	}
}
