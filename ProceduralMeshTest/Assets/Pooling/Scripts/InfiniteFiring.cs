using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteFiring : MonoBehaviour {

	public GameObject Bullet;
	public Transform SpawnPoint;
	public float delay = 2f;
	public float delta = 0.3f;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("createBullet", delay, delta);
	}

	void createBullet()
	{
		GameObject.Instantiate (Bullet, SpawnPoint.position, Quaternion.identity);
	}
}
