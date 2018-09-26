using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    float score;
    [HideInInspector] public float combo = 1;

    public GameObject gameFeel; //cool particle effect to spawn when collectable is grabbed
    public GameObject comboGameFeel;

    public Text scoreCounter;
    public Text comboCounter;

    private void Start()
    {
        scoreCounter.text = "Score: " + score.ToString();
        comboCounter.text = "Combo: " + combo.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if collide with collectable then add to score and combo, spawn the game feel particles, and destroy the collectable
        if (other.CompareTag("Collectable"))
        {
            score += 1*combo; //give points plus combo multiplier
            combo++;

            scoreCounter.text = "Score: " + score.ToString();
            comboCounter.text = "Combo: " + combo.ToString();


            if (combo > 1)
                Instantiate(comboGameFeel, other.gameObject.transform.position, other.gameObject.transform.rotation);
            else
                Instantiate(gameFeel, other.gameObject.transform.position, other.gameObject.transform.rotation);


            Destroy(other.gameObject); //kill the krill :)
        }
    }

    public void ResetCombo()
    {
        combo = 1;
        comboCounter.text = "Combo: " + combo.ToString();
    }
}
