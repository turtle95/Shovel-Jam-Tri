using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : MonoBehaviour
{

    public GameObject bullet;
    public float speed = 2;
    public Transform cannon;
    public GameObject player;
    public GameObject explosion;
    

    Transform playerTrans;
    Rigidbody playerRb;
    float randNum = 0;
    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.GetComponent<Transform>();
        playerRb = player.GetComponent<Rigidbody>();
        
        StartCoroutine(ShootStuff());
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(playerTrans.position); //look at the player
        rb.velocity = transform.forward * speed;
        Debug.Log("magnitude " + playerRb.velocity.magnitude);
        Debug.Log("normalized " + rb.velocity.normalized);
        //if you're going slower than the player than set your speed to the player's speed
        if (rb.velocity.magnitude < playerRb.velocity.magnitude)
            rb.velocity = new Vector3(playerRb.velocity.x, rb.velocity.y, 0);

    }

    //wait a random amount of time then shoot something
    IEnumerator ShootStuff()
    {
        randNum = Random.Range(4, 6);
        yield return new WaitForSeconds(randNum);
        Instantiate(bullet, cannon.position, cannon.rotation);
        StartCoroutine(ShootStuff());
    }

    //spawns an explosion and sends ship downwards, then waits and deletes it
    public IEnumerator DestroyShip()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        rb.AddForce(Vector3.down * speed, ForceMode.Impulse);
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }


    //if player collides with ship, player dies
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health healthComp = collision.gameObject.GetComponent<Health>();
            healthComp.TakeDamage(3, true);
        }
    }
}
