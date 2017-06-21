using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody rgdBdy;
	public float force = 5f;

	// Use this for initialization
	void Start () 
	{
		this.rgdBdy = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.W)) 
		{
			rgdBdy.AddForce (Vector3.forward * force);
		}
		if (Input.GetKey(KeyCode.D)) 
		{
			rgdBdy.AddForce (Vector3.right * force);
		}
		if (Input.GetKey(KeyCode.A)) 
		{
			rgdBdy.AddForce (Vector3.left * force);
		}
		if (Input.GetKey(KeyCode.S)) 
		{
			rgdBdy.AddForce (Vector3.back * force);
		}
		
	}
}
