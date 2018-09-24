using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    

    public GameObject[] obsListOne;
    public GameObject[] obsListTwo;
    public GameObject[] obsListThree;

    public Vector3 spawnBox;
    Vector3 spawnPos;

    public Transform topScreen;
    public Transform bottomScreen;
    public Transform rightScreen;

    Vector3 chosenSpawner;
    public float widthRange;
    public float heightRange;
    float randNum;
    int randNumInt;
    float spawnTimeMin = 0.5f;
    float spawnTimeMax = 2f;
    public int stage = 1;

    //float prevDist = 0;
    float curDist = 0;
    float goalDist = 5;

    public Transform player;

	void Start () {
        StartCoroutine(WhenToSpawn());
        //prevDist = player.position.x;
    }

    private void Update()
    {
        curDist = player.position.x; 
        if(curDist < goalDist)
        {
            SpawnSomething();
            goalDist = curDist -15;
          //  Debug.Log("Current Dist " + curDist + "goal Dist " + goalDist);
        }
    }

    IEnumerator WhenToSpawn()
    {
        randNum = Random.Range(spawnTimeMin, spawnTimeMax);
        yield return new WaitForSeconds(1);
        SpawnSomething();
        //StartCoroutine(WhenToSpawn());
    }


    //spawns obstacles to avoid
    void SpawnSomething()
    {


        for (int i = 0; i < 4; i++)
        {
            //spawn something at both top, bottom, and right of screen
           
            switch (i)
            {
                case 0:
                    chosenSpawner = topScreen.position;
                   // Debug.Log(topScreen.position);
                    randNum = Random.Range(-widthRange, 0);
                    spawnPos = new Vector3(chosenSpawner.x + randNum, chosenSpawner.y, chosenSpawner.z);
                    break;
                case 1:
                    chosenSpawner = bottomScreen.position;
                    randNum = Random.Range(-widthRange, 0);
                    spawnPos = new Vector3(chosenSpawner.x + randNum, chosenSpawner.y, chosenSpawner.z);
                    break;
                case 3:
                    chosenSpawner = rightScreen.position;
                    randNum = Random.Range(-heightRange, heightRange);
                    spawnPos = new Vector3(chosenSpawner.x, chosenSpawner.y + randNum, chosenSpawner.z);
                    break;
                default:                  
                    break;
            }
            
            

            //changes which list an obstacle is chosen from
            switch (stage)
            {
                case 1:
                    randNumInt = Random.Range(0, obsListOne.Length);
                    Instantiate(obsListOne[randNumInt], spawnPos, transform.rotation);
                    break;
                case 2:
                    randNumInt = Random.Range(0, obsListTwo.Length);
                    Instantiate(obsListTwo[randNumInt], spawnPos, transform.rotation);
                    break;
                case 3:
                    randNumInt = Random.Range(0, obsListThree.Length);
                    Instantiate(obsListThree[randNumInt], spawnPos, transform.rotation);
                    break;
                default:
                    break;
            }
        }
        
    }
}
