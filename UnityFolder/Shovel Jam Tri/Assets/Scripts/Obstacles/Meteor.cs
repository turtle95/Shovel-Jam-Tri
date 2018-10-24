using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Meteor : MonoBehaviour {

    public string targetTag;
    public float minSpeed = 1f;
    public float maxSpeed = 1f;

    private Rigidbody rigid;

	void Start () {

        if (string.IsNullOrEmpty(targetTag))
            return;

        GameObject target = GameObject.FindGameObjectWithTag(targetTag);

        if (target && target.activeSelf)
        {
            //shift actually target a bit so the meteor can get in the way of player
            Vector3 direction = (target.transform.position - Vector3.right*50f) - transform.position;

            rigid = GetComponent<Rigidbody>();
            rigid.velocity = direction.normalized * Random.Range(minSpeed, maxSpeed);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
