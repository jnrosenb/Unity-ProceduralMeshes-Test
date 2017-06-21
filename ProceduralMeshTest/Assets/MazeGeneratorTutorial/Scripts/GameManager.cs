using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	public Maze mazeTemplate;
	private Maze mazeInstance;

	// Use this for initialization
	void Start () 
	{
		gameStart ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp (KeyCode.Escape))
		{
			gameReset ();
		}		
	}

	private void gameStart()
	{
		mazeInstance = GameObject.Instantiate (mazeTemplate);

		mazeInstance.generateMaze ();
	}

	private void gameReset()
	{
		StopAllCoroutines ();
		Destroy (mazeInstance.gameObject);

		gameStart ();
	}
}
