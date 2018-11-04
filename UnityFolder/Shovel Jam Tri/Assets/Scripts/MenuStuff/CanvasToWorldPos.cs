using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToWorldPos : MonoBehaviour {

    public Camera cam;
    public RectTransform UiObject;
    public int zDist = 2; //how far the object is from the camera
    public int yDist = 0;
    //Vector3 pos;
	// Update is called once per frame
	void Update ()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(UiObject.position.x, UiObject.position.y + yDist, cam.nearClipPlane+zDist));
		//transform.position = new Vector3 (pos.x, pos.y, 0);
	}
}
