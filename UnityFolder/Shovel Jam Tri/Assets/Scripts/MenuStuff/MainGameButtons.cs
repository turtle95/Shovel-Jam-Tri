using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainGameButtons : MonoBehaviour {

    public bool phoneBuild = true;

    Vector3 touchPoint = Vector3.zero;
    public Camera cam;
    public Tutorial tutScript;


    /// <summary>
    /// This is a version of button main made specifically for the main scene ui. I switched everything to physical buttons so that the text and meshes would line up
    /// </summary>

    void Update()
    {


        if (phoneBuild)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase != TouchPhase.Ended) //stores the position of the touch
                {
                    //sens ray from touch position and activates button that was touched
                    Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        if (hit.collider.gameObject.CompareTag("Restart"))
                        {
                            Pressed(0);

                        }
                        else if (hit.collider.gameObject.CompareTag("Menu"))
                        {
                            Pressed(1);
                        }
                        else if (hit.collider.gameObject.CompareTag("SkipTutorial"))
                        {
                            Pressed(2);
                        }
                        

                    }
                    touchPoint = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5));

                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) //stores the position of the touch
            {
                //sens ray from touch position and activates button that was touched
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.gameObject.CompareTag("Restart"))
                    {
                        Pressed(0);

                    }
                    else if (hit.collider.gameObject.CompareTag("Menu"))
                    {
                        Pressed(1);
                    }
                    else if (hit.collider.gameObject.CompareTag("SkipTutorial"))
                    {
                        Pressed(2);
                    }
                    

                }
                touchPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            }
        }
    }

    void Pressed(int chosen)
    {
        switch (chosen)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
            case 2:
                tutScript.PlayTheDamnGame();
                break;
        }
    }
}
