using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class generatePlaneMesh : MonoBehaviour 
{
	private MeshFilter mf;
	public MeshCollider mc;
	public float size = 10f;
	public int gridSize = 16;
	public float totalMeshDisplacement = 0.2f;

	public static generatePlaneMesh current;

	void Awake()
	{
		if (!current) 
		{
			current = this;
		} 
		else 
		{
			Destroy (gameObject);
		}
	}

	//Property for getting the mesh out of the MeshFilter component:
	public Mesh mesh
	{
		get
		{
			if (!mf.mesh) 
			{
				Debug.LogWarning ("Your MeshFilter has no mesh atached!");
				return null;
			} 
			else 
			{
				return mf.mesh;
			}
		}
	}

	void Start () 
	{
		mf = gameObject.GetComponent<MeshFilter> ();
		mf.mesh = GenerateMesh ();

		mc = gameObject.GetComponent<MeshCollider> ();
		mc.sharedMesh = mf.mesh;

		//Solo activar si se agrega como componente a este gameobject el script planeDeformer.
		//gameObject.SendMessage ("planeRegeneration");
	}

	//Experimento:
	void Update()
	{
//		this.mc.sharedMesh = null;
//		this.mc.sharedMesh = mf.mesh;
	}

	Mesh GenerateMesh()
	{
		Mesh mesh = new Mesh ();

		Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
		Vector3[] normals = new Vector3[(gridSize + 1) * (gridSize + 1)];
		Vector2[] uvs = new Vector2[(gridSize + 1) * (gridSize + 1)];

		int index = 0;
		for (int z = 0; z <= gridSize; z++) 
		{
			for (int x = 0; x <= gridSize; x++) 
			{
				vertices [index] = new Vector3 (-size/2f + x * (size/gridSize), 0f, -size/2f + z * (size/gridSize));
				normals [index] = Vector3.up;
				uvs [index++] = new Vector2 (x/(float)gridSize, z/(float)gridSize);
			}
		}

		int triangleAmount = gridSize * gridSize * 2;
		int[] triangles = new int[triangleAmount*3];
		for (int i = 0; i < gridSize; i++) 
		{
			for (int j = 0; j < gridSize; j++) 
			{
				//	0 - 3 - 1
				//	1 - 3 - 2

				//Indice que se usa para moverse dentro del arreglo de ints de los triangulos:
				int startPos = i*(gridSize)*(6) + j*(6);

				//Indices de referencia para moverse en el arreglo de los vertices:
				int startPosForVertices1 = i * (gridSize+1) + j;
				int startPosForVertices2 = (i+1) * (gridSize+1) + j;

				triangles [startPos + 0] = startPosForVertices1 + 0;
				triangles [startPos + 1] = startPosForVertices2 + 0;
				triangles [startPos + 2] = startPosForVertices1 + 1;

				triangles [startPos + 3] = startPosForVertices1 + 1;
				triangles [startPos + 4] = startPosForVertices2 + 0;
				triangles [startPos + 5] = startPosForVertices2 + 1;
			}
		}

		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		return mesh;
	}
}
