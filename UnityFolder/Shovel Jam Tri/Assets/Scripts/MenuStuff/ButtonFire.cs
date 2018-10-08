using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFire : MonoBehaviour {

    public GameObject[] active;
    public GameObject[] inactive;
    public GameObject bulletRock;

    public Transform aimer;

    Rigidbody rb;
    //turns things on and off depending on which state is called
    public void Pressed()
    {
        for(int i =0; i< active.Length; i++)
        {
            active[i].SetActive(true);
        }

        for (int i = 0; i < inactive.Length; i++)
        {
            inactive[i].SetActive(false);
        }

        Instantiate(bulletRock, aimer.position, aimer.rotation).GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.forward, ForceMode.Impulse);
    }

    public void notPressed()
    {
        for (int i = 0; i < active.Length; i++)
        {
            active[i].SetActive(false);
        }

        for (int i = 0; i < inactive.Length; i++)
        {
            inactive[i].SetActive(true);
        }
    }
}
