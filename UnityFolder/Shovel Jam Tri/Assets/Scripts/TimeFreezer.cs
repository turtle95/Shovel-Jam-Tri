using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezer : MonoBehaviour {

    private float _scale = 1f;

    public void FreezeGame(bool s)
    {
        if (s)
        {
            _scale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = _scale;
        }
    }

    private void OnDestroy()
    {
        FreezeGame(false);
    }
}
