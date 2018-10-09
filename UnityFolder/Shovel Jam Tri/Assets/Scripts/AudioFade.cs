using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour {

  
    public AudioSource[] music;//the audio sources to fade between

    public float fadeTime =20f, //time fade takes
        seconds = 3f; //how smooth the fade is, time between volume steps

    
    int lastChosen = 0; //the audio source to fade out


    public void FadeAudio(int chosenOne)
    {
        StartCoroutine(ActuallyFadeAudio(chosenOne));
    }

    //comments comming soon... yell at josh if they are needed
     IEnumerator ActuallyFadeAudio(int chosenOne)
    {
        music[chosenOne].Play();

        float stepInterval = seconds / fadeTime;
        float volumeInterval = 1 / fadeTime;

        for(int i = 0; i< fadeTime; i++)
        {
            music[lastChosen].volume -= volumeInterval;
            music[chosenOne].volume += volumeInterval;

            yield return new WaitForSeconds(stepInterval);
        }
        
        music[lastChosen].Stop();
        lastChosen = chosenOne;
    }
}
