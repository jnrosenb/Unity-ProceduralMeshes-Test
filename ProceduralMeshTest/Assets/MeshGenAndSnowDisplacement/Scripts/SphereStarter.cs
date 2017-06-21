using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereStarter : MonoBehaviour {

	private Rigidbody rbd;

	// Use this for initialization
	void Start () 
	{
		this.rbd = GetComponent<Rigidbody> ();

		this.rbd.AddForce (-Camera.main.gameObject.transform.forward * 20);
	}
}
