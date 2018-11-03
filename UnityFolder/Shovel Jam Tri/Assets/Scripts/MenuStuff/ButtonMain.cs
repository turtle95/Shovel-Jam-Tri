using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMain : MonoBehaviour {

    public bool phoneBuild = true;

    Vector3 touchPoint = Vector3.zero;
    public Camera cam;

    public ButtonFire play;
    public ButtonFire credits;
    public ButtonFire highScore;
   // public SaveManager saveManage;

   // public GameObject tutButton;

   
    void Update ()
    {
        
        
        if (phoneBuild)
        {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase != TouchPhase.Ended) //stores the position of the touch
                {
                    //sens ray from touch position and activates button that was touched
                    Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        if (hit.collider.gameObject.CompareTag("Play"))
                        {
                            SaveManager.instance.maxScores.viewedTutorial = true;
                            play.Pressed();

                        }
                        else if (hit.collider.gameObject.CompareTag("Credits"))
                        {
                            credits.Pressed();
                            highScore.notPressed();
                        }
                        else if (hit.collider.gameObject.CompareTag("HighScore"))
                        {
                            highScore.Pressed();
                            credits.notPressed();
                        } else if (hit.collider.gameObject.CompareTag("Tutorial"))
                        {
                            SaveManager.instance.maxScores.viewedTutorial = false;
                            play.Pressed();
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
                    if (hit.collider.gameObject.CompareTag("Play"))
                    {
                        SaveManager.instance.maxScores.viewedTutorial = true;
                        play.Pressed();

                    }
                    else if (hit.collider.gameObject.CompareTag("Credits"))
                    {
                        credits.Pressed();
                        highScore.notPressed();
                    }
                    else if (hit.collider.gameObject.CompareTag("HighScore"))
                    {
                        highScore.Pressed();
                        credits.notPressed();
                    }
                    else if (hit.collider.gameObject.CompareTag("Tutorial"))
                    {
                        SaveManager.instance.maxScores.viewedTutorial = false;
                        play.Pressed();
                    }

                }
                touchPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            }
        }
    }

    
}
