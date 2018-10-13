using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    /// <summary>
    /// The krill will face left on the screen and move in that direction so that they leave behind a particle trail
    /// </summary>

    public bool fishOfLife = false;
    public bool tutorialObj = false;

    Vector3 target;

    public float speed = 5f;

    Animation[] animsStore;

	// Use this for initialization
	void Start () {
        target = new Vector3(transform.position.x + 50, transform.position.y, 0);
        transform.LookAt(target);

        if (!fishOfLife)
        {
            animsStore = GetComponentsInChildren<Animation>();
            foreach(Animation anim in animsStore)
            {
                anim["Swim"].speed = Random.Range(1.5f, 3f);
            }
        }
            

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-transform.right * speed * Time.deltaTime);
	}

    
}
