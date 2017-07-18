using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(generatePlaneMesh))]
public class DeformableMesh : MonoBehaviour 
{
	public float maxDepression;

	private generatePlaneMesh planeGenerator;

	private List<Vector3> originalVertices = new List<Vector3>();
	private List<Vector3> modifiedVertices = new List<Vector3>();

	public static DeformableMesh current;

	void Awake()
	{
		if (current == null) 
		{
			current = this;
		} 
		else 
		{
			Destroy (gameObject);
		}

		planeGenerator = GetComponent<generatePlaneMesh> ();
	}

	void Update()
	{
		//Experimento modificar collider en real time (EN PAUSA)-
		planeGenerator.mc.sharedMesh = null;
		planeGenerator.mc.sharedMesh = planeGenerator.mesh;

		planeGenerator.mesh.SetVertices (modifiedVertices);	
	}

	void planeRegeneration()
	{
		planeGenerator.mesh.MarkDynamic ();

		planeGenerator.mesh.vertices = (originalVertices).ToArray();
		planeGenerator.mesh.vertices = (modifiedVertices).ToArray();

		//planeGenerator.mesh.GetVertices(originalVertices); //Unity5.6
		//planeGenerator.mesh.GetVertices(modifiedVertices); //Unity5.6
	}
		
	public void addDepression(Vector3 depressionPoint, float radius)
	{
		for (int i = 0; i < originalVertices.Count; i++)
		{
			Vector3 currentVertex = originalVertices[i];
			float distance = Vector3.Distance (depressionPoint, currentVertex);

			if (distance <= radius) 
			{
				//This should go from zero to pi/2, to represent cos input in radians:
				float normDist = (Mathf.PI / 2f)* (distance / radius);

				float maxDisplacement = Mathf.Cos (normDist) * maxDepression;

				if ((originalVertices [i] + Vector3.down * maxDisplacement).y <= modifiedVertices [i].y)
				{
					Vector3 replacementVector = originalVertices [i] + Vector3.down * maxDisplacement;

					modifiedVertices.RemoveAt (i);
					modifiedVertices.Insert (i, replacementVector);
				}
			}
		}
	}
}
