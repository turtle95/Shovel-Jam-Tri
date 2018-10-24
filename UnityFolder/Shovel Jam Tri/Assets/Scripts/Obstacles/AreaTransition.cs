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

   
    int area = 1;

	private SaveManager.MaxScores maxScores; 


	void Start () {
        goalDist -= Random.Range(minDist, maxDist);
		maxScores = SaveManager.GetMaxScores();
    }


    void Update () {
		if(spawnScript.curDist < goalDist)
        {
			//switches randomly between the areas
			if (maxScores.randAreas) 
			{
				area = Random.Range (0, 6);
			} else 
			{ //switches between the areas in order
				area++;
				if (area > 6)
					maxScores.randAreas = true;
			}


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
