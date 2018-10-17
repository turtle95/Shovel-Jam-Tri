using BayatGames.SaveGameFree;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [System.Serializable]
    public struct MaxScores
    {
        public const string identifier = "maxscore.dat";
        public int score;
        public int combo;
    }

    public MaxScores maxScores;

    public int score;
    [HideInInspector] public int combo = 1;

    public GameObject gameFeel; //cool particle effect to spawn when collectable is grabbed
    public GameObject comboGameFeel;

    public Text scoreCounter;
    public Text comboCounter;

    public AudioSource collectNoise;

    public Health hScript;

    private void Start()
    {
        //hScript = GetComponent<Health>();
        InitSaveGame();
        maxScores = LoadScores();
        if (scoreCounter)
            scoreCounter.text = "Score: " + score.ToString();
        if (comboCounter)
            comboCounter.text = "Combo: " + combo.ToString();
    }

    private void OnDisable()
    {
        SaveScores();
    }

    private void OnApplicationQuit()
    {
        SaveScores();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if collide with collectable then add to score and combo, spawn the game feel particles, and destroy the collectable
        if (other.CompareTag("Collectable"))
        {
            Collectable collect = other.GetComponent<Collectable>();
            if (!collect.fishOfLife)
            {
                score += 1 * combo; //give points plus combo multiplier
                combo++;

                scoreCounter.text = "Score: " + score.ToString();
                comboCounter.text = "Combo: " + combo.ToString();

                if (score > maxScores.score)
                    maxScores.score = score;
                if (combo > maxScores.combo)
                    maxScores.combo = combo;
            }
            else
            {
                hScript.TakeDamage(-1);
            }

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

    #region save/load MaxScores
    private void InitSaveGame()
    {
        if (Debug.isDebugBuild)
            SaveGame.SavePath = SaveGamePath.DataPath; //local path if debug build
        else
            SaveGame.SavePath = SaveGamePath.PersistentDataPath; //save to persistent location on release builds
    }

    private MaxScores LoadScores()
    {
        if (SaveGame.Exists(MaxScores.identifier))
        {
            try
            {
                MaxScores data;
                if (Debug.isDebugBuild)
                {
                    data = SaveGame.Load(MaxScores.identifier, new MaxScores(), false);
                }
                else
                {
                    data = SaveGame.Load(MaxScores.identifier, new MaxScores(), true);
                }
                return data;
            }
            catch (Exception)
            {
                SaveGame.Delete(MaxScores.identifier);
                return new MaxScores();
            }
        }
        else
            return new MaxScores();
    }

    public void SaveScores()
    {
        try
        {
            if (Debug.isDebugBuild)
                SaveGame.Save(MaxScores.identifier, maxScores, false);
            else
                SaveGame.Save(MaxScores.identifier, maxScores, true);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion
}
