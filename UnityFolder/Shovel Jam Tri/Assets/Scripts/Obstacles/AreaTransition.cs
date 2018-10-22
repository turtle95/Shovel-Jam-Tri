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

    public bool randAreas = false;
    int area = 1;

	void Start () {
        goalDist -= Random.Range(minDist, maxDist);
    }


    void Update () {
		if(spawnScript.curDist < goalDist)
        {
            if (randAreas)
            {
                area = Random.Range(1, 7);
            } else
                area++;

            fadeScript.FadeAudio(area);
            spawnScript.area = area;

            goalDist = spawnScript.curDist - Random.Range(minDist, maxDist);

            //7 stages. at each stage decrease the distToSpawn so we see more obstacles
            //minimum is 3 ... for now.
            if(spawnScript.minDist > 3)
            {
                spawnScript.minDist -= 1.5f;
                spawnScript.maxDist -= 1.5f;
            }
        }
	}
}
