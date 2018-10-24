using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanges : MonoBehaviour {

	/// <summary>
	/// This script rotates the skybox based on your movement and blends between different skyboxes when the area changes
	/// </summary>


	public float speedMult = 2;

	public Skybox[] skyboxes;

	void Update () {
		
		RenderSettings.skybox.SetFloat ("_Rotation", Time.time  *speedMult);
		
	}

	public void SwitchSkybox(int area)
	{
		switch (area) {
		case 0:
			//RenderSettings.skybox = skyboxes [area];
			break;
		}

	}
}
