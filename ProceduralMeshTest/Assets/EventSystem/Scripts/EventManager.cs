using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour 
{

	private Dictionary<string, UnityEvent> eventDict;

//	ALTERNATIVA. A ver si funciona para usar esto como si fuera singleton (Despues)-.
//	public static EventManager Manager;
//	void Awake()
//	{
//		if (Manager == null) 
//		{
//			Manager = this;
//		} 
//		else 
//		{
//			Destroy (gameObject);
//		}
//	}
//
//	void Start()
//	{
//		this.eventDict = new Dictionary<string, UnityEvent> ();	
//	}

	private static EventManager manager;
	public static EventManager Manager
	{
		get
		{
			if (manager == null) 
			{
				manager = FindObjectOfType<EventManager> () as EventManager;

				if (manager == null) 
				{
					Debug.LogError ("A event manager gameObject holding the EventManager script needs to be on the scene!");
				} 
				else 
				{
					manager.initManager ();
				}
			}

			return manager;
		}
	}

	private void initManager()
	{
		if (eventDict == null)
			this.eventDict = new Dictionary<string, UnityEvent> ();
	}

	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent newEvent = null;

		if (Manager.eventDict.TryGetValue (eventName, out newEvent)) 
		{
			newEvent.AddListener (listener);		
		} 
		else 
		{
			newEvent = new UnityEvent ();
			newEvent.AddListener (listener);

			Manager.eventDict.Add (eventName, newEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if (Manager == null)
			return;

		UnityEvent newEvent = null;
		if (Manager.eventDict.TryGetValue (eventName, out newEvent)) 
		{
			newEvent.RemoveListener (listener);		
		} 
	}

	public static void TriggerEvent(string eventName)
	{
		UnityEvent newEvent = null;

		if (Manager.eventDict.TryGetValue (eventName, out newEvent)) 
		{
			newEvent.Invoke ();		
		} 
		else 
		{
			Debug.LogError ("The event does not exists in the dictionary.");
		}
	}
	
}
