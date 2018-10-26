using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallEnemy : MonoBehaviour {

    public GameObject bullet;
    public float speed = 5;
    public Transform cannon;
    
    public GameObject explosion;


    public Transform playerTrans; //player transform, so that the ship can move towards the player
    
    float randNum = 0; //random numbner var, used for shooting

    //get shoot stuff started
    private void Start()
    {
        StartCoroutine(ShootStuff());
    }


    // Update is called once per frame
    void Update()
    {

        transform.LookAt(playerTrans.position); //look at the player

        transform.Translate(transform.forward * speed * Time.deltaTime);

    }

    //wait a random amount of time then shoot something
    IEnumerator ShootStuff()
    {
        randNum = Random.Range(0, 3);
        yield return new WaitForSeconds(randNum);
        Instantiate(bullet, cannon.position, cannon.rotation);
        StartCoroutine(ShootStuff());
    }

  
}
