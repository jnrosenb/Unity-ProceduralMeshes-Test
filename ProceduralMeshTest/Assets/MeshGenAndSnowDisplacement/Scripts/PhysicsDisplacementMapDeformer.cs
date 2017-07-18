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
		/*displacementMap.wrapMode = UnityEngine.TextureWrapMode.Clamp;//*/
		displacementMap.Apply ();

		dispTexID = Shader.PropertyToID ("_DispTex");
		mainTexID = Shader.PropertyToID ("_MainTex");

		material.SetTexture (dispTexID, displacementMap);
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
			float amount = (2 * pixelRadius + 1);
			for (int i = 0; i < amount; i++)
			{
				for (int j = 0; j < amount; j++)
				{
					Vector2 chunkDictCoords = new Vector2 (textureContact.x + i - pixelRadius, textureContact.y + j - pixelRadius);
					float distanceToCenter = Vector2.Distance (chunkDictCoords, textureContact);
					if (distanceToCenter <= pixelRadius)
					{
						Color pixelColor = displacementMap.GetPixel ((int)chunkDictCoords.x, (int)chunkDictCoords.y);
						if (pixelColor.r > 0)
						{
							//Color defined by proximity to center:
							Color colorf = new Color();
							colorf.r = Mathf.Max(0f, pixelColor.r / distanceToCenter);
							colorf.g = Mathf.Max(0f, pixelColor.g / distanceToCenter);
							colorf.b = Mathf.Max(0f, pixelColor.b / distanceToCenter);

							displacementMap.SetPixel ((int)chunkDictCoords.x, (int)chunkDictCoords.y, colorf);
						}
					}
				}
			}

			displacementMap.Apply ();
			material.SetTexture (dispTexID, displacementMap);
			material.SetTexture (mainTexID, displacementMap);

//			this.planeMesh.mc.sharedMesh = null;
//			this.planeMesh.mc.sharedMesh = this.planeMesh.Mesh;
		}
		else
			Debug.LogWarning("Plane deformer was not correctly referenced");
	}
}
