using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour {

    /// <summary>
    /// This script controlls the different "areas"
    /// Pretty much, at random distance intervals the music will be changed
    /// and the types/amount of things spawned changes
    /// </summary>

    public ObstacleSpawner spawnScript;
    public AudioFade fadeScript;

    public float minDist = 100f;
    public float maxDist = 250f;

    float goalDist = 0f;

	void Start () {
        goalDist -= Random.Range(minDist, maxDist);
    }


    void Update () {
		if(spawnScript.curDist < goalDist)
        {
            fadeScript.FadeAudio(Random.Range(1, 10));
            spawnScript.area = Random.Range(0, 2);
            goalDist = spawnScript.curDist - Random.Range(minDist, maxDist);

            //9 stages. at each stage decrease the distToSpawn so we see more obstacles
            //minimum is 3 ... for now.
            spawnScript.distToSpawn -= 1.5f;
            if (spawnScript.distToSpawn < 3)
                spawnScript.distToSpawn = 3;
        }
	}
}
