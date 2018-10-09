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
    public int area = 1;

   
    public float curDist = 0;
    public float goalDist = 5;

    public Transform player;

    private List<Transform> spawnedObjects;

	void Start () {
        spawnedObjects = new List<Transform>();

        StartCoroutine(WhenToSpawn());
        
    }

    private void Update()
    {
        curDist = player.position.x; 
        if(curDist < goalDist)
        {
            SpawnSomething();
            goalDist = curDist -15;
          
        }

        //remove stuff if it is behind and far enough from player
        for (int i = spawnedObjects.Count - 1; i >= 0; i --)
        {
            var objTransform = spawnedObjects[i];
            if (objTransform == null)
            {
                spawnedObjects.RemoveAt(i);
                continue;
            }

            if (objTransform.position.x > player.position.x
            && Mathf.Abs(objTransform.position.x - player.position.x) > 50f)
            {
                Destroy(objTransform.gameObject);
                spawnedObjects.RemoveAt(i);
            }
        }
    }

    IEnumerator WhenToSpawn()
    {
        randNum = Random.Range(spawnTimeMin, spawnTimeMax);
        yield return new WaitForSeconds(1);
        SpawnSomething();
       
    }


    //spawns obstacles to avoid
    void SpawnSomething()
    {
        for (int i = 0; i < 3; i++)
        {
            //spawn something at both top, bottom, and right of screen
           
            switch (i)
            {
                case 0:
                    chosenSpawner = topScreen.position;
                   
                    randNum = Random.Range(-widthRange, 0);
                    spawnPos = new Vector3(chosenSpawner.x + randNum, chosenSpawner.y, chosenSpawner.z);
                    break;
                case 1:
                    chosenSpawner = bottomScreen.position;
                    randNum = Random.Range(-widthRange, 0);
                    spawnPos = new Vector3(chosenSpawner.x + randNum, chosenSpawner.y, chosenSpawner.z);
                    break;
                case 2:
                    chosenSpawner = rightScreen.position;
                    randNum = Random.Range(-heightRange, heightRange);
                    spawnPos = new Vector3(chosenSpawner.x, chosenSpawner.y + randNum, chosenSpawner.z);
                    break;
                default:                  
                    break;
            }

            //changes which list an obstacle is chosen from
            GameObject newObject = null;
            switch (area)
            {
                case 1:
                    randNumInt = Random.Range(0, obsListOne.Length);
                    newObject = Instantiate(obsListOne[randNumInt], spawnPos, transform.rotation);
                    break;
                case 2:
                    randNumInt = Random.Range(0, obsListTwo.Length);
                    newObject = Instantiate(obsListTwo[randNumInt], spawnPos, transform.rotation);
                    break;
                case 3:
                    randNumInt = Random.Range(0, obsListThree.Length);
                    newObject = Instantiate(obsListThree[randNumInt], spawnPos, transform.rotation);
                    break;
                default:
                    break;
            }

            if (newObject != null)
                spawnedObjects.Add(newObject.transform);
        }
    }
}
