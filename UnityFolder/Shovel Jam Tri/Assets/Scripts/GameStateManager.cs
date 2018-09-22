using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public enum GameState
	{
		Init,
		MainMenu,
		Gameplay,
		EndScreen
	}

	[HideInInspector]
	public GameState gameState;

	public GameObject mainMenu;
	public GameObject mainCharacter;
	public GameObject endScreen;

	// Use this for initialization
	void Start () {
		gameState = GameState.Init;
		GoToMainMenu();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void GoToMainMenu()
	{
		gameState = GameState.MainMenu;
		endScreen.SetActive(false);
		mainCharacter.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void StartGame()
	{
		gameState = GameState.Gameplay;

		// turn off main menu and end screen just in case
		mainMenu.SetActive(false);
		endScreen.SetActive(false);

		// turn on main character
		mainCharacter.SetActive(true);
	}
}
