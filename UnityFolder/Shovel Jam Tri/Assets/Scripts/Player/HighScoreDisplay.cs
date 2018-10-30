using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {

    public Text savedScore;
    public Text currentScore;
    public Score scoreScript;

    //plays a sound based on whether your score beat the old high score
    public AudioSource looserSound;
    public AudioSource winnerSound;

    private SaveManager.MaxScores maxScores;

    public GameObject scoreComboBox; //hide the score/combo stuff when this pops up
    public GameObject pauseButton;


    private void OnEnable()
    {
        scoreComboBox.SetActive(false);
        pauseButton.SetActive(false);
        maxScores = SaveManager.GetMaxScores();

        savedScore.text = "High Score\nKrill: " + maxScores.score + "\nCombo: " + maxScores.combo;
        if (currentScore != null)
        {
            currentScore.text = "\n\nCurrent Score\nKrill: " + scoreScript.score + "\nCombo: " + scoreScript.combo;

            if (scoreScript.score > maxScores.score)
            {
                maxScores.score = scoreScript.score;
                winnerSound.Play();
            }

            if (scoreScript.combo > maxScores.combo)
            {
                maxScores.combo = scoreScript.combo;
                winnerSound.Play();
            }
            else if (scoreScript.score < maxScores.score)
                looserSound.Play();

            
            //SaveScores(); ?
        }
    }
}
