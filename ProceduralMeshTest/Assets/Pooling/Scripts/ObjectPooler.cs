using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public GameObject poolObj;
	public static ObjectPooler ObjectPoolerScript;
	private List<GameObject> objects;

	public int numberOfObj;
	public bool willGrow = false;


	void awake()
	{
		ObjectPoolerScript = this;	
	}

	void Start () 
	{
		objects = new List<GameObject>();

		for (int i = 0; i < numberOfObj; i++) 
		{
			GameObject obj = (GameObject)Instantiate (poolObj);
			obj.SetActive (false);
			objects.Add (obj);
		}
	}

	public GameObject getPooledObj () 
	{
		for (int i = 0; i < objects.Count; i++) 
		{
			if (!objects [i].activeInHierarchy) 
			{
				return objects [i];
			}
		}

		if (willGrow) 
		{
			GameObject obj = (GameObject)Instantiate (poolObj);
			objects.Add (obj);
			return obj;
		}

		return null;
	}
}
