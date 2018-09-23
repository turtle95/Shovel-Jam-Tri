using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    

    GameObject[] obsListOne;
    GameObject[] obsListTwo;
    GameObject[] obsListThree;

    public Vector3 spawnBox;
    Vector3 spawnPos;

    public Transform topScreen;
    public Transform bottomScreen;
    public Transform rightScreen;

    Vector3 chosenSpawner;
    public float widthRange;
    public float heightRange;
    float randNum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnSomething()
    {
        chosenSpawner = topScreen.position;
        randNum = Random.Range(-widthRange, widthRange);
        spawnPos = new Vector3(chosenSpawner.x + randNum, chosenSpawner.y, chosenSpawner.z);
        randNum = Random.Range(0, obsListOne.Length);
        Instantiate()
    }
}
