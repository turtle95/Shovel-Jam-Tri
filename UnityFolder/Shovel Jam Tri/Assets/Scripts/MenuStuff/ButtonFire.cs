using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFire : MonoBehaviour {

    /// <summary>
    /// Comments Comming Soon~ish! 
    /// -Josh
    /// </summary>

    public GameObject[] active;
    public GameObject[] inactive;
  //  public GameObject bulletRock;

    //public Transform aimer;

    

    //public bool playButton = false;
    public AudioSource pressSound;


    //turns things on and off depending on which state is called
    public void Pressed()
    {

        //if (playButton)
        //   fadeScript.FadeAudio(1);
        pressSound.Play();
        for (int i =0; i< active.Length; i++)
        {
            active[i].SetActive(true);
        }

        for (int i = 0; i < inactive.Length; i++)
        {
            inactive[i].SetActive(false);
        }

        

       // Instantiate(bulletRock, aimer.position, aimer.rotation).GetComponent<Rigidbody>();
       
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
