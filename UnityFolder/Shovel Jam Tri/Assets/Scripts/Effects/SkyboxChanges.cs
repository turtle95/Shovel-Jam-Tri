using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanges : MonoBehaviour {

	/// <summary>
	/// This script rotates the skybox based on your movement and blends between different skyboxes when the area changes
	/// </summary>


	public float speedMult = 2;

	public Cubemap[] skyboxes;

    bool slotOne = true;
    bool blending = false;
    float blendValue =0;

	void Update () {
		
        //slowly rotates the skybox
		//RenderSettings.skybox.SetFloat ("_Rotation", Time.time  *speedMult);



        //blends between the two skybox slots, stops blending when they reach their max value
        if (blending)
        {
            if (slotOne)
            {
                blendValue += Time.deltaTime;

                if (blendValue >= 1.0f) {
                    blending = false;
                }

            }
            else
            {
                blendValue -= Time.deltaTime;

                if (blendValue <= 0.0f) {
                    blending = false;
                }

            }

            blendValue = Mathf.Clamp(blendValue, 0.0f, 1.0f);
            RenderSettings.skybox.SetFloat("_Blend", blendValue);
            CloudLayer.instance.SetSkyboxBlend(blendValue);

        }
	}

    //loads in the new skybox ans starts fading to it
	public void SwitchSkybox(int area)
	{
        
        //switches out the innactive slot with the new skybox texture
        if (slotOne)
        {
            RenderSettings.skybox.SetTexture("_Skybox1", skyboxes[area]);
            slotOne = false;
        }
        else
        {
            RenderSettings.skybox.SetTexture("_Skybox2", skyboxes[area]);
            slotOne = true;
        }


        blending = true; //starts the blending process
	}
}
