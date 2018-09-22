using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToEnd : MonoBehaviour {

	public GameStateManager gameStateManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		gameStateManager.GoToMainMenu();
	}
}
