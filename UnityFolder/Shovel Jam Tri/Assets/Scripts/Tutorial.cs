using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public bool viewedTutorial = false; //bool to determine if the tutorial has already been viewed

    public TouchMovement playerScript; //ref to the player's movement script

    int stageOfTut = 0;


    public GameObject[] stageItems;

    public Camera cam;
    public ObstacleSpawner obsSpawnScript;

    private void Start()
    {
        if (viewedTutorial)
            PlayTheDamnGame();
    }

    void Update()
    {
        Touch[] myTouches = Input.touches; //gets an array of all the touches going on


        for (int i = 0; i < Input.touchCount; i++)
        {
            if (myTouches[i].phase == TouchPhase.Began) //dash code
            {
                if(stageOfTut == 0)
                {
                    //stageItems[0].SetActive(false);
                    
                    StartCoroutine(WaitForSomething(1));
                }
                if(stageOfTut == 1)
                {
                    playerScript.maxSpeed = 10f;
                    StartCoroutine(WaitForSomething(2));
                }
                
            }
        }
        SkipTutorial();
    }

    IEnumerator WaitForSomething(int goal)
    {
        stageItems[stageOfTut].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        stageOfTut = goal;
        stageItems[goal].SetActive(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Collectable"))
        {
            if(stageOfTut == 2)
                StartCoroutine(WaitForSomething(3));
            if(stageOfTut == 3)
                StartCoroutine(WaitForSomething(4));
            if(stageOfTut == 4)
            {
                PlayTheDamnGame();
            }
            Destroy(col.gameObject);
        }
    }

    //turn off all tutorial objects, set viewed tutorial to true, turn on the obstacle spawner, and disable this script
    void PlayTheDamnGame()
    {
        viewedTutorial = true;
        obsSpawnScript.enabled = true;
        playerScript.maxSpeed = 10f;
        foreach (GameObject plop in stageItems)
        {
            plop.SetActive(false);
        }
        this.enabled = false;
    }

    void SkipTutorial()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase != TouchPhase.Ended) //stores the position of the touch
            {
                //sens ray from touch position and activates button that was touched
                Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.gameObject.CompareTag("Tutorial"))
                    {
                        PlayTheDamnGame();
                    }
                }
            }
        }
    }
}
