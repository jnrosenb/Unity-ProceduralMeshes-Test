using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Test1 : MonoBehaviour 
{
	private UnityAction action;

	void Awake()
	{
		action = new UnityAction(action01);
	}

	void OnEnable()
	{
		EventManager.StartListening ("wea1", action);
	}

	void OnDisable()
	{
		EventManager.StopListening ("wea1", action);
	}

	private void action01()
	{
		Debug.Log ("Hello world!");
	}
}
