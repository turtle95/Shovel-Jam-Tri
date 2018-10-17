using UnityEngine;
using UnityEngine.UI;
using MaxScores =  SaveManager.MaxScores;

public class Score : MonoBehaviour {
    
    public int score;
    [HideInInspector] public int combo = 1;

    public GameObject gameFeel; //cool particle effect to spawn when collectable is grabbed
    public GameObject comboGameFeel;

    public Text scoreCounter;
    public Text comboCounter;

    public AudioSource collectNoise;

    public Health hScript;

    private MaxScores maxScores; //maxscores to get from Savemanager. 

    private void Start()
    {
        //hScript = GetComponent<Health>();
        if (scoreCounter)
            scoreCounter.text = "Score: " + score.ToString();
        if (comboCounter)
            comboCounter.text = "Combo: " + combo.ToString();

        maxScores = SaveManager.GetMaxScores(); //retrieve in Start to avoid method race. 
    }


    private void OnTriggerEnter(Collider other)
    {
        //if collide with collectable then add to score and combo, spawn the game feel particles, and destroy the collectable
        if (other.CompareTag("Collectable"))
        {
            Collectable collect = other.GetComponent<Collectable>();
            if (collect.fishOfLife)
            {
                hScript.TakeDamage(-1);
            }
            else
            {
                score += 1 * combo; //give points plus combo multiplier
                scoreCounter.text = "Score: " + score.ToString();
                if (score > maxScores.score)
                    maxScores.score = score;
            }


               
                combo++;

               
                comboCounter.text = "Combo: " + combo.ToString();

                
                if (combo > maxScores.combo)
                    maxScores.combo = combo;
           

            collectNoise.Play();
            Instantiate(gameFeel, other.gameObject.transform.position, other.gameObject.transform.rotation);

            if (!collect.tutorialObj)           
                Destroy(other.gameObject); //kill the krill :)
        }
    }

    public void ResetCombo()
    {
        combo = 1;
        comboCounter.text = "Combo: " + combo.ToString();
    }
}
