using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToWorldPos : MonoBehaviour {

    public Camera cam;
    public RectTransform UiObject;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(UiObject.position.x, UiObject.position.y, cam.nearClipPlane+2));
		//transform.position = new Vector3 (0, transform.position.y, transform.position.z);
	}
}
