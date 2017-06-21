using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed  = 100;

	// Update is called once per frame
	void Update ()
	{
		transform.position = transform.position + new Vector3 (0, 0, speed);		
	}
}
