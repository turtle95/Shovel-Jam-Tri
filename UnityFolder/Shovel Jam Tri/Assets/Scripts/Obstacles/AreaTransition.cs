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
    public ObstacleSpawner backdropSpawner;
    public AudioFade fadeScript;

    public float minDist = 100f;
    public float maxDist = 250f;

    SkyboxChanges skyChangeScript; //script to fade between skyboxes

    float goalDist = 0f; //distance at which the area will change

   
    public int area = 0;

	private SaveManager.MaxScores maxScores; 


	void Start () {
        goalDist -= Random.Range(minDist, maxDist);
		maxScores = SaveManager.GetMaxScores();
        skyChangeScript = GetComponent<SkyboxChanges>();
    }


    void Update () {

        //if (Input.GetKeyDown(KeyCode.P))
        //{
            
        //    Debug.LogError("SkippedLevel");
        //    spawnScript.curDist = goalDist;
        //}
            

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


            fadeScript.FadeAudio(area); //fades audio based on area
            spawnScript.area = area; //swaps out obstacles being spawned
            backdropSpawner.area = area; //swaps out backdrops being spawned
            skyChangeScript.SwitchSkybox(area); //fades between skyboxes

        
            goalDist = spawnScript.curDist - Random.Range(minDist, maxDist); //calculates a new distance goal

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
