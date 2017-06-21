using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteFiring2 : MonoBehaviour 
{
	public Transform SpawnPoint;
	public float delay;
	public float delta;

	public ObjectPooler pooler;

	void Start () 
	{
		InvokeRepeating ("createBullet", delay, delta);
	}

	void createBullet()
	{
		//GameObject obj = ObjectPooler.ObjectPoolerScript.getPooledObj ();
		GameObject obj = pooler.getPooledObj ();

		if (obj == null)
			return;

		obj.transform.position = SpawnPoint.transform.position;
		obj.transform.rotation = Quaternion.identity;
		obj.SetActive (true);
	}
}
