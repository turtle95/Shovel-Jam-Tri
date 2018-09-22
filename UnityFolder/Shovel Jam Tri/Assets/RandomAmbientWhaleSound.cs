using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAmbientWhaleSound : MonoBehaviour {

	public AudioSource whaleSound;

	public float intervalMax, intervalMin;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(RandomWait());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator RandomWait()
	{
		while (true)
		{
			whaleSound.Play();
			yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
		}
	}
}
