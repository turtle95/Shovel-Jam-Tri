using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchMovement : MonoBehaviour {

    Vector3 touchPoint = new Vector3(5, 0, 0); //the spot where you touched
    public Camera cam; //reference to the camera
    public float speed = 5; //the normal speed you move at
    public float dashSpeed = 600;
    Transform target; //a gameObject for the player to look at
    public GameObject targetObj;
    int w, h; //the width and height of the screen

   // bool dashing = false; //bool to tell when player is currently dashing
    public Rigidbody rb; //reference to the player's Rigidbody
    public ParticleSystem dashParticles;
    public ParticleSystem dashParticles2;
    private bool touching = false;

    public float maxSpeed = 10;
    public float maxRegSpeed = 5;


    private void Start()
    {
        target = targetObj.GetComponent<Transform>();
    }


    void Update () {

        w = Screen.width ; //finds the height and width of the screen and halves it
        h = Screen.height ;

      
        Touch[] myTouches = Input.touches; //gets an array of all the touches going on
        
        touchPoint = cam.ScreenToWorldPoint(new Vector3(w, h, 5)); //initializes the touchPoint to being straight in front of the player... probably don't need this anymore

        //
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (myTouches[i].phase != TouchPhase.Ended ) //stores the position of the touch
            {
               
                touchPoint = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5));
               
                touching = true;
            }
            else
                touching = false;
            
            if (myTouches[i].phase == TouchPhase.Began) //dash code
            {
                //touching = true;
                if (rb.velocity.magnitude < maxSpeed/dashSpeed)
                    rb.AddForce(transform.forward * dashSpeed *rb.velocity.magnitude, ForceMode.Impulse);
                else
                    rb.velocity = (transform.forward * maxSpeed);
                dashParticles.Play();
                dashParticles2.Play();
                //rb.velocity = transform.forward * dashSpeed * Time.deltaTime;
            }
        }
        touchPoint.z = 0; //sets the z value of the position to 0
        target.position = touchPoint; //sets the target position to the touchPoint
        touchPoint = new Vector3(transform.position.x - 3, touchPoint.y, 0); //this makes it so that you get the same angle of movement no matter where you touch on the screen
        Move();
	}

    void Move()
    {
        if (rb.velocity.magnitude < maxSpeed) //if rb's moving slower than 5, then add force
            rb.AddForce(transform.forward * speed);
        else
        {     //if you are above the speed limit then add force as normal but also add a force in the opposite direction that will keep you in the speed limit
            rb.AddForce(transform.forward * speed);
            rb.AddForce(-(rb.velocity.normalized * (rb.velocity.magnitude - maxSpeed)));
            
        }
      

            
            //rb.velocity = transform.forward * speed;
        if(transform.position.z  != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (touching == true)
        {
            //looks at the target position
            transform.LookAt(touchPoint);
            targetObj.SetActive(true);
        }

        else
        {
            //transform.LookAt(rb.velocity);
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            targetObj.SetActive(false);
        }
    }

    //IEnumerator Dash()
    //{
    //    dashing = true;
    //    speed = 10;
    //    yield return new WaitForSeconds(1f);
    //    speed = 5;
    //    dashing = false;
    //}
}
