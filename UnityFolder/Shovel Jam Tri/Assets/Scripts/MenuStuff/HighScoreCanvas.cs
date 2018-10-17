using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaxScores = SaveManager.MaxScores;

public class HighScoreCanvas : MonoBehaviour
{
    public Text scoreText;
    public Text comboText;

    private MaxScores maxScores;

    // Use this for initialization
    void Start ()
    {
        maxScores = SaveManager.GetMaxScores();

        scoreText.text = "Score: " + maxScores.score;
        comboText.text = "Combo: " + maxScores.combo;
    }
}
