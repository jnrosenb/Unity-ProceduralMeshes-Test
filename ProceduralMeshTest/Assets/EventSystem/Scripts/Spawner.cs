using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject minion;
	private Vector3 spawnPosition;

	void OnEnable()
	{
		EventManager.StartListening ("Spawn", Spawn);
	}

	void OnDisable()
	{
		EventManager.StopListening ("Spawn", Spawn);
	}

	void Spawn()
	{
		
	}

	void FindSpawnLocation()
	{
		
	}
}
