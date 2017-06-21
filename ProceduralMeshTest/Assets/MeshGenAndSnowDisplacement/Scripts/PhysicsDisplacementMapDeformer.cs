using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDisplacementMapDeformer : MonoBehaviour 
{
	private generatePlaneMesh planeMesh;
	public Texture2D displacementMap;
	public Material material;

	public static PhysicsDisplacementMapDeformer current;

	[Range(64, 1024)]
	public int size = 256;
	public int pixelRadius = 5;
	private int dispTexID;
	private int mainTexID;

	void Awake()
	{
		if (!current) 
		{
			current = this;
		} 
		else
			Destroy (gameObject);

		planeMesh = GetComponent<generatePlaneMesh> ();

		displacementMap = new Texture2D (size, size, TextureFormat.ARGB32, true);
		for(int x = 0; x < size; x++)
		{
			for(int y = 0; y < size; y++)
			{
				displacementMap.SetPixel(x, y, Color.white);
			}
		}
		//displacementMap.wrapMode = UnityEngine.TextureWrapMode.Clamp;
		displacementMap.Apply ();

		dispTexID = Shader.PropertyToID ("_DispTex");
		mainTexID = Shader.PropertyToID ("_MainTex");

		material.SetTexture (dispTexID, displacementMap);
		material.SetTexture (mainTexID, displacementMap);
	}

	void Start()
	{
//		planeMesh = GetComponent<generatePlaneMesh> ();
//
//		displacementMap = new Texture2D (size, size, TextureFormat.ARGB32, true);
//		for(int x = 0; x < size; x++)
//		{
//			for(int y = 0; y < size; y++)
//			{
//				displacementMap.SetPixel(x, y, Color.black);
//			}
//		}
//		displacementMap.Apply ();
//
//		dispTexID = Shader.PropertyToID ("_DispTex");
//		mainTexID = Shader.PropertyToID ("_MainTex");
//
//		material.SetTexture (dispTexID, displacementMap);
//		material.SetTexture (mainTexID, displacementMap);
	}

	void OnCollisionStay(Collision col)
	{
		if (planeMesh) 
		{
			float planeWidth = planeMesh.gameObject.GetComponent<MeshRenderer>().bounds.size.x;
			float planeLength = planeMesh.gameObject.GetComponent<MeshRenderer>().bounds.size.z;

			Vector3 contact = col.contacts [0].point;
			Vector3 contactCorrected = new Vector3(contact.x - planeWidth/2, 0f, contact.z - planeLength/2);

			Vector3 nContact = new Vector3 (contactCorrected.x / planeWidth, 0f, contactCorrected.z / planeLength);

			Vector2 textureContact = new Vector2 ((int)(nContact.x * size), (int)(nContact.z * size));

			//-------------REEMPLAZAR POR CODIGO INTELIGENTE CON RADIO DINAMICO:
			Color pixelColor = displacementMap.GetPixel ((int)textureContact.x, (int)textureContact.y);
			if (pixelColor.r > 0) 
			{
				Color c1 = new Color (pixelColor.r - 30, pixelColor.g - 30, pixelColor.b - 30);
				Color c2 = new Color (pixelColor.r - 10, pixelColor.g - 10, pixelColor.b - 10);

				if (c1.r < 0)
					c1 = Color.black;
				if (c2.r < 0)
					c2 = Color.black;
				
				displacementMap.SetPixel ((int)textureContact.x, (int)textureContact.y, c1);

				displacementMap.SetPixel ((int)textureContact.x + 1, (int)textureContact.y + 1, c2);
				displacementMap.SetPixel ((int)textureContact.x + 1, (int)textureContact.y - 1, c2);
				displacementMap.SetPixel ((int)textureContact.x + 1, (int)textureContact.y + 0, c2);
				displacementMap.SetPixel ((int)textureContact.x + 0, (int)textureContact.y + 1, c2);
				displacementMap.SetPixel ((int)textureContact.x + 0, (int)textureContact.y - 1, c2);
				displacementMap.SetPixel ((int)textureContact.x - 1, (int)textureContact.y + 1, c2);
				displacementMap.SetPixel ((int)textureContact.x - 1, (int)textureContact.y - 1, c2);
				displacementMap.SetPixel ((int)textureContact.x - 1, (int)textureContact.y + 0, c2);
			}
			//-------------

			displacementMap.Apply ();
			material.SetTexture (dispTexID, displacementMap);
			material.SetTexture (mainTexID, displacementMap);
		}
		else
			Debug.LogWarning("Plane deformer was not correctly referenced");
	}
}
