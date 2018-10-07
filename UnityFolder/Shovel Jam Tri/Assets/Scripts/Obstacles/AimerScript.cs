using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimerScript : MonoBehaviour {

    Transform target;
    public string targetTag = "Player";

	
	void Update () {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
        transform.LookAt(target.position);
	}
}
