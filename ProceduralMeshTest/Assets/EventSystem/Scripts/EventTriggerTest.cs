using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerTest : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.Space))
			EventManager.TriggerEvent ("");
		if (Input.GetKey (KeyCode.Return))
			EventManager.TriggerEvent ("");
	}
}
