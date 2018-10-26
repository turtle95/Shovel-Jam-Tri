using BayatGames.SaveGameFree;
using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    public class MaxScores
    {
        public const string identifier = "maxscore.dat";
        public int score;
        public int combo;
        public bool viewedTutorial;
		public bool randAreas;
    }

    public bool dontDestroyedOnLoad;
    [HideInInspector] public MaxScores maxScores;

    public static SaveManager instance { get; private set; }

    #region UNITY FUNCS
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitSaveGame();
            maxScores = LoadScores();

            if (dontDestroyedOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        SaveScores();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveScores();
    }

    private void OnApplicationQuit()
    {
        SaveScores();
    }
    #endregion

    /// <summary>
    /// Return Maxscores. Throw exception if instance is null.
    /// Make sure a SaveManager object exists on scene.
    /// </summary>
    /// <returns></returns>
    public static MaxScores GetMaxScores()
    {
        return instance.maxScores;
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
            catch (Exception e)
            {
                Debug.Log(e.Message);
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
