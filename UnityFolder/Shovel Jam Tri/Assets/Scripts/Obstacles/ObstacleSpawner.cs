using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//class to hold obstacle items and their spawn rates
[System.Serializable]
public class Obstacle
{   
    public GameObject obstacle;
    public int spawnRate = 0;
}

//class to define areas
[System.Serializable]
public class Area
{
    [Header("All Spawn Rates can't go over 100%")]
    public Obstacle[] obstaclesToSpawn;
}






public class ObstacleSpawner : MonoBehaviour {

    
    //areas are lists of obstacles to be spawned, each containing their own spawn rate    
    public Area[] areas;

   
  
    Vector3 spawnPos; //temp var - position object is spawned at

    //the objects to determine where things should be spawned
    public Transform topScreen;
    public Transform bottomScreen;
    public Transform rightScreen;

    Vector3 chosenSpawner;

    public float widthRange;
    public float heightRange;

    //temp vars- random numbers for calculations
    float randNum;
    int randNumInt;

    //...stuff for controlling the spawning by time though distance makes more since since speed changes
    float spawnTimeMin = 0.5f;
    float spawnTimeMax = 2f;

    public int area = 0;
   
    public float curDist = 0;
    public float goalDist = 5;
    float distToSpawn = 5f;
    public float maxDist = 13f;
    public float minDist = 10f;


    public Transform player;

    private List<Transform> spawnedObjects;

    public bool backgroundSpawner = false;

    int weightSum = 0;




	void Start () {
        spawnedObjects = new List<Transform>();
        distToSpawn = Random.Range(minDist, maxDist);
      //  StartCoroutine(WhenToSpawn()); //spawns something based on time
    }




    private void Update()
    {
        //spawns based on distance
        curDist = player.position.x; 
        if(curDist < goalDist)
        {
            SpawnSomething();
            goalDist = curDist -(distToSpawn);
            distToSpawn = Random.Range(minDist, maxDist);
        }



        //remove stuff if it is behind and far enough from player
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
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




    ////spawns based on time
    //IEnumerator WhenToSpawn()
    //{
    //    randNum = Random.Range(spawnTimeMin, spawnTimeMax);
    //    yield return new WaitForSeconds(randNum);
    //    SpawnSomething();
       
    //}





    //spawns obstacles to avoid
    void SpawnSomething()
    {
        for (int i = 0; i < 3; i++)
        {
            //if using the background spawner then only spawn from one of the spawnpoints
            if (backgroundSpawner)
                i = Random.Range(0, 2);
            
            //sets the spawn position to either the top, right, or bottom spawner, runs through all three for regular obstacle spawns 
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



            //spawns an object from the current area's list based on thier spawn rates
            weightSum = 0;
            GameObject newObject = null;
            randNumInt = Random.Range(0, 101); //random number out of 100 to determine what object to spawn
            switch (area)
            {
                case 0:
				for(int j =0; j < areas[0].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[0].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum )
                        {
                            newObject = Instantiate(areas[0].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        } 
                    }
                    break;
                case 1:
				for (int j = 0; j < areas[1].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[1].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum)
                        {
                            newObject = Instantiate(areas[1].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        }
                    }
                    break;
                case 2:
				for (int j = 0; j < areas[2].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[2].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum)
                        {
                            newObject = Instantiate(areas[2].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        }
                    }
                    break;
                case 3:
				for (int j = 0; j < areas[3].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[3].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum)
                        {
                            newObject = Instantiate(areas[3].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        }
                    }
                    break;
                case 4:
				for (int j = 0; j < areas[4].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[4].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum)
                        {
                            newObject = Instantiate(areas[4].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        }
                    }
                    break;
                case 5:
				for (int j = 0; j < areas[5].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[5].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum)
                        {
                            newObject = Instantiate(areas[5].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        }
                    }
                    break;
                case 6:
				for (int j = 0; j < areas[6].obstaclesToSpawn.Length; j++)
                    {
                        weightSum += areas[6].obstaclesToSpawn[j].spawnRate;
                        if (randNumInt < weightSum)
                        {
                            newObject = Instantiate(areas[6].obstaclesToSpawn[j].obstacle, spawnPos, Random.rotation);
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (newObject != null)
                spawnedObjects.Add(newObject.transform);

            //if spawning background objects then break the loop and wait for the next spawn to be called
            if (backgroundSpawner)
                i = 4;
        }
    }
}
