using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

   
    public CameraFollow camScript;
    public GameObject largeShip;
    public Transform spawnPoint;

    GameObject spawnedShip;

    float camMoveTime = 2;
	// Update is called once per frame
	public void SwitchArea (float area) {
		if (area == 3 || area == 5)
        {
            camScript.xOffset = Mathf.Lerp(camScript.xOffset, -2f, camMoveTime * Time.deltaTime);
            spawnedShip = Instantiate(largeShip, spawnPoint.position, spawnPoint.rotation);
        } else
        {
            if(spawnedShip != null)
            {
                StartCoroutine(spawnedShip.GetComponent<LargeEnemy>().DestroyShip());
            }
            camScript.xOffset = Mathf.Lerp(camScript.xOffset, 7, camMoveTime * Time.deltaTime);
        }
	}
}
