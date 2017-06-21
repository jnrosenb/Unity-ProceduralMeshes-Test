using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDeformer : MonoBehaviour {

	DeformableMesh planeDeformer;

	void Start()
	{
		planeDeformer = GetComponent<DeformableMesh> ();
	}

	void OnCollisionStay(Collision col)
	{
		if (planeDeformer)
			planeDeformer.addDepression(col.contacts[0].point, 0.2f);
		else
			Debug.LogWarning("Plane deformer was not correctly referenced");
	}
}
