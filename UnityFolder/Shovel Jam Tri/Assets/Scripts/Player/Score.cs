using UnityEngine;
using UnityEngine.UI;
using MaxScores =  SaveManager.MaxScores;
using TMPro;

public class Score : MonoBehaviour {
    
    public int score;
    [HideInInspector] public int combo = 1;

    public GameObject gameFeel; //cool particle effect to spawn when collectable is grabbed
    public GameObject comboGameFeel;

    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI comboCounter;

    public AudioSource collectNoise;

    public Health hScript;

    private MaxScores maxScores; //maxscores to get from Savemanager. 

	public GameObject pointEffect; //spawns when collectible is hit

    private void Start()
    {
        //hScript = GetComponent<Health>();
        if (scoreCounter)
            scoreCounter.text = "" + score.ToString();
        if (comboCounter)
            comboCounter.text = "" + combo.ToString();

        maxScores = SaveManager.GetMaxScores(); //retrieve in Start to avoid method race. 
    }


    private void OnTriggerEnter(Collider other)
    {
        //if collide with collectable then add to score and combo, spawn the game feel particles, and destroy the collectable
        if (other.CompareTag("Collectable"))
        {
            Collectable collect = other.GetComponent<Collectable>();
            if (!collect.tutorialObj)
            {
                TextMeshPro effectText = Instantiate(pointEffect, other.transform.position, other.transform.rotation).GetComponent<TextMeshPro>();

                if (collect.fishOfLife)
                {
                    effectText.text = "X" + combo.ToString();
                    hScript.GetHealed(1);
                }
                else
                {
                    int adjustedCombo = 1 * combo;
                    effectText.text = adjustedCombo.ToString();
                    score += adjustedCombo; //give points plus combo multiplier
                    scoreCounter.text = "" + score.ToString();
                    if (score > maxScores.score) {
                        maxScores.score = score;
                    }

                }

                // Screenshake increases with higher combos
                //float screenshakeIntensity = Mathf.Clamp(0.075f + 0.035f * combo, 0.0f, 0.35f);
                //CameraFollow.instance.Screenshake(screenshakeIntensity);



                combo++;


                comboCounter.text = "" + combo.ToString();


                if (combo > maxScores.combo) {
                    maxScores.combo = combo;
                }


                collectNoise.Play();
                Instantiate(gameFeel, other.gameObject.transform.position, other.gameObject.transform.rotation);

            }
               
            Destroy(other.gameObject); //kill the krill :)
            
        }
    }

    public void ResetCombo()
    {
        combo = 1;
        comboCounter.text = "" + combo.ToString();
    }
}
