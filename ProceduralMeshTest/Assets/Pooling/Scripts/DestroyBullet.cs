using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

	public float destroyTime = 5;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, destroyTime);	
	}
}
