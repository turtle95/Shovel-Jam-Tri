﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {

    public Text savedScore;
    public Text currentScore;
    public Score scoreScript;

    

    private void OnEnable()
    {
        savedScore.text = "High Score\nKrill: " + scoreScript.maxScores.score + "\nCombo: " + scoreScript.maxScores.combo;
        if (currentScore != null)
        {
            currentScore.text = "\n\nCurrent Score\nKrill: " + scoreScript.score + "\nCombo: " + scoreScript.combo;

            if (scoreScript.score > scoreScript.maxScores.score)
            {
                scoreScript.maxScores.score = scoreScript.score;
            }

            if (scoreScript.combo > scoreScript.maxScores.combo)
            {
                scoreScript.maxScores.combo = scoreScript.combo;
            }
            scoreScript.SaveScores();
        }
    }
}
