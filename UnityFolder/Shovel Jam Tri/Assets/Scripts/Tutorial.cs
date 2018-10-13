using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public bool viewedTutorial = false; //bool to determine if the tutorial has already been viewed

    public TouchMovement playerScript; //ref to the player's movement script

    int stageOfTut = 0;


    public GameObject[] stageItems;

	// Use this for initialization
	void Start () {
		
	}

    //
    void Update()
    {
        Touch[] myTouches = Input.touches; //gets an array of all the touches going on


        for (int i = 0; i < Input.touchCount; i++)
        {
            if (myTouches[i].phase == TouchPhase.Began) //dash code
            {
                if(stageOfTut == 0)
                {
                    stageItems[0].SetActive(false);
                    
                    StartCoroutine(WaitForSomething(1));
                }
                if(stageOfTut == 1)
                {
                    playerScript.maxSpeed = 10f;
                }
                
            }
        }
    }

    IEnumerator WaitForSomething(int goal)
    {
        stageItems[goal].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        stageOfTut = goal;
        stageItems[goal].SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
    }
}
