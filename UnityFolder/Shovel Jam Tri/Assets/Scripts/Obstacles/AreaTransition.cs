using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public CameraSwitch camScript;

    public float minDist = 100f;
    public float maxDist = 250f;

    SkyboxChanges skyChangeScript; //script to fade between skyboxes

    public float goalDist = 0f; //distance at which the area will change
    public float curDist = 0;
    Vector3 lastSpot = Vector3.zero;

    public int area = -1;

	private SaveManager.MaxScores maxScores; 


    public void SkipArea()
    {
        goalDist = spawnScript.curDist + 1;

       
    }


	void Start () {
        goalDist -= Random.Range(minDist, maxDist);
		maxScores = SaveManager.GetMaxScores();
        skyChangeScript = GetComponent<SkyboxChanges>();
        SwitchAreas();
        maxScores.randAreas = false;       
    }


    void Update()
    {
        //calculate your own curDist with your own lastSpot, spawnScript was resetting stuff
        curDist = Vector3.Distance(spawnScript.player.position, lastSpot);


        if (curDist > goalDist)
        {

            SwitchAreas();

        }
    }
    void SwitchAreas()
    {

        //Debug.Log("Random areas bool = " + maxScores.randAreas);
        //switches randomly between the areas
        if (maxScores.randAreas)
        {
             area = Random.Range(0, 7);
        }
        else
        { //switches between the areas in order
              area++;
              if (area > 7)
                  maxScores.randAreas = true;
        }


            fadeScript.FadeAudio(area); //fades audio based on area
            spawnScript.area = area; //swaps out obstacles being spawned
            backdropSpawner.area = area; //swaps out backdrops being spawned
            skyChangeScript.SwitchSkybox(area); //fades between skyboxes
            camScript.SwitchArea(area);


            lastSpot = spawnScript.player.position; //resets the spot to calculate travel distance from
            goalDist = Random.Range(minDist, maxDist); //calculates a new distance goal

            //8 stages. at each stage decrease the distToSpawn so we see more obstacles
            //minimum is 3 ... for now.
            if (spawnScript.minDist > 3)
            {
                spawnScript.minDist -= 1.5f;
                spawnScript.maxDist -= 1.5f;
            }


        //----Remove this when done------
        var text = GameObject.Find("MainGame/Canvas/AreaID").GetComponent<TextMeshProUGUI>();
        text.text = area.ToString();

    }
}
