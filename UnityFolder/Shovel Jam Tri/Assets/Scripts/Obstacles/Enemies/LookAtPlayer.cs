using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    Transform playerTrans; //player transform, so that the ship can move towards the player

    // Use this for initialization
    void Start () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(playerTrans.position); //look at the player
    }
}
