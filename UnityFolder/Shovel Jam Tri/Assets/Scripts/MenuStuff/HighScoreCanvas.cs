using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Score))]
public class HighScoreCanvas : MonoBehaviour
{
    public Text scoreText;
    public Text comboText;

    // Use this for initialization
    void Start ()
    {
        var score = GetComponent<Score>();
        scoreText.text = "Score: " + score.maxScores.score;
        comboText.text = "Combo: " + score.maxScores.combo;
    }
}
