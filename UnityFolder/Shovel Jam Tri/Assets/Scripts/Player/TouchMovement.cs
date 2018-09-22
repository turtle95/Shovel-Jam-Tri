using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour {

    Vector3 touchPoint = new Vector3(5, 0, 0);
    public Camera cam;
    public float speed = 5;

    public Transform cubeThing;
    int xW, yW;

    bool dashing = false;
    public Rigidbody rb;
	// Update is called once per frame
	void Update () {

        xW = Screen.width / 2;
        yW = Screen.height / 2;
        Touch[] myTouches = Input.touches;
        //if(Input.GetTouch(0).position != null)
        //

        //  if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //   touchPoint = new Vector3(5,0,0);
        touchPoint = cam.ScreenToWorldPoint(new Vector3(xW, yW, 5));
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(myTouches[i].phase != TouchPhase.Ended)
            {
                touchPoint = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5));
                //dash
            }
            
            if (myTouches[i].phase == TouchPhase.Began)
            {
                //if(!dashing)
                // StartCoroutine(Dash());
                rb.AddForce(transform.forward * 6000 * Time.deltaTime, ForceMode.Impulse);
            }
        }
        touchPoint.z = 0;
        cubeThing.position = touchPoint;
        Move();
	}

    void Move()
    {
        if (rb.velocity.x < 5)
            rb.velocity = (new Vector3(6, rb.velocity.y, 0));
        transform.LookAt(cubeThing.position);
        //speed = 5;
        
    }

    IEnumerator Dash()
    {
        dashing = true;
        speed = 10;
        yield return new WaitForSeconds(1f);
        speed = 5;
        dashing = false;
    }
}
