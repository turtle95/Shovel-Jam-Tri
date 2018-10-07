using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody rb;
    public float launchSpeed = 10;
    public string target;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(target))
        {
            //maybe do damage?
            collision.gameObject.BroadcastMessage("ResetCombo");
            Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
