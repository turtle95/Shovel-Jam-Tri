using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

   
    public CameraFollow camScript;
    public GameObject largeShip;
    public Transform spawnPoint;

    GameObject spawnedShip;

    float camMoveTime = 5;

    bool largeEnemy = false;

    float currentTime = 0;

    bool currentlyMoving = false;

    private void Update()
    {
        if (currentlyMoving)
        {
            if (largeEnemy)
            {

                if (currentTime <= camMoveTime)
                {
                    currentTime += Time.deltaTime / camMoveTime;
                    //Debug.Log("Current Time " + currentTime);
                    camScript.xOffset = Mathf.Lerp(7f, -3f, currentTime);
                }
                else
                    currentlyMoving = false;
            }
            else
            {

                if (currentTime <= camMoveTime)
                {
                    currentTime += Time.deltaTime;
                    camScript.xOffset = Mathf.Lerp(-3f, 7f, currentTime / camMoveTime);

                }
                else
                    currentlyMoving = false;
            }
        }
    }


    // Update is called once per frame
    public void SwitchArea (float area)
    {
       

		if (area == 3 || area == 5)
        {
            currentTime = 0;
            largeEnemy = true;
            currentlyMoving = true;
            spawnedShip = Instantiate(largeShip, spawnPoint.position, spawnPoint.rotation);
        } else
        {
            currentTime = 0;
            largeEnemy = false;
			spawnedShip = GameObject.FindGameObjectWithTag ("LargeEnemy");

            if(camScript.xOffset < 0)
            {
                currentlyMoving = true;
            }

            if(spawnedShip != null)
            {
                StartCoroutine(spawnedShip.GetComponent<LargeEnemy>().DestroyShip());
			}           
        }
	}
}
