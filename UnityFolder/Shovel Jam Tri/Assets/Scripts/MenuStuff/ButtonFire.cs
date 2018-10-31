using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFire : MonoBehaviour {

    /// <summary>
    /// If credits or highscore are pressed then switch things on and off
    /// if play or playtutorial pressed then load the next scene
    /// </summary>

    public GameObject[] active;
    public GameObject[] inactive;
    public GameObject innerGlass;

    
    public TexturePainterCopy paintScript;
    public RenderTexture hsRend;


    public bool playButton = false; //bool to tell if this button should load the next scene
    public AudioSource pressSound;


    //play and play Tutorial stuff
    public GameObject cam;
    bool loading = false;
    public float speed = 2f;
    public Transform seagull;
    public AudioSource seagullCall;

    public Vector3 vel;
    public Vector3 angVel;
    public Animator seagullAnim;
    public float flySpeed = 10.0f;
    
    public float rotSpeed = 10.0f;
    public GameObject titleRef;

    private void Start()
    {
        //if (!playButton)
        //{
        //    ClearHighScore();
        //}
    }

    private void Update()
    {
        if (loading)
        {
            cam.transform.Translate(-transform.forward * Time.deltaTime * speed);
            seagull.position += vel * Time.fixedDeltaTime;
            seagull.rotation *= Quaternion.Euler(angVel * Time.fixedDeltaTime);
        }
            
    }


    //turns things on and off depending on which state is called
    public void Pressed()
    {
        

        if (playButton)
        {
            //scene transition animations and whatnot
            //load level while keeping loading scene set up
            seagullAnim.speed = 60;
            seagullCall.Play();
            vel = vel.normalized * flySpeed;
            angVel = Random.insideUnitSphere * rotSpeed;
            
            loading = true;
            StartCoroutine(LoadTheGame());
        }
        else
        {
            titleRef.SetActive(false);
            paintScript.ClearTexture();
            ClearHighScore();
        }

        innerGlass.SetActive(false);

        pressSound.Play();
        for (int i = 0; i < active.Length; i++)
        {
            active[i].SetActive(true);
        }

        for (int i = 0; i < inactive.Length; i++)
        {
            inactive[i].SetActive(false);
        }

    }

    public void notPressed()
    {
        for (int i = 0; i < active.Length; i++)
        {
            active[i].SetActive(false);
        }

        for (int i = 0; i < inactive.Length; i++)
        {
            inactive[i].SetActive(true);
        }
    }

    IEnumerator LoadTheGame()
    {
        yield return new WaitForSeconds(2);
        AsyncOperation async = SceneManager.LoadSceneAsync(1);

        while (!async.isDone)
        {
            yield return null;
        }
    }


    void ClearHighScore()
    {
        hsRend.Release();
    }

}
