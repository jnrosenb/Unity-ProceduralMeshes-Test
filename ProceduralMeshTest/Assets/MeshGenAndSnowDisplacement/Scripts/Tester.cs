using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour 
{
	private RaycastHit Hit;
	private LineRenderer line;

	private Texture2D displacementMap;//******************************************
	private generatePlaneMesh planeMesh;//******************************************
	private Material material;//******************************************

	private int dispTexID;//******************************************
	private int mainTexID;//******************************************
	private int size;

	// Use this for initialization
	void Start () 
	{
		this.Hit = new RaycastHit ();
		//********************************************************************************************************************************************
		planeMesh = generatePlaneMesh.current;

		material = PhysicsDisplacementMapDeformer.current.material;
		displacementMap = PhysicsDisplacementMapDeformer.current.displacementMap;
		size = PhysicsDisplacementMapDeformer.current.size;

		dispTexID = Shader.PropertyToID ("_DispTex");
		mainTexID = Shader.PropertyToID ("_MainTex");
		//********************************************************************************************************************************************

		line = GetComponentInChildren<LineRenderer> ();
		line.gameObject.SetActive (false);
	}

	void Update()
	{
		if (Input.GetMouseButton (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			line.SetPosition (0, transform.position);
			line.SetPosition (1, transform.position + ray.direction * 100);
			line.gameObject.SetActive (true);

			if (Physics.Raycast (ray, out Hit)) 
			{
				//DeformableMesh.current.addDepression (Hit.point, 0.2f); (Restaurar al borrar todos los ***)

				//********************************************************************************************************************************************
				float planeWidth = planeMesh.gameObject.GetComponent<MeshRenderer>().bounds.size.x;
				float planeLength = planeMesh.gameObject.GetComponent<MeshRenderer>().bounds.size.z;

				Vector3 contact = new Vector3(Hit.point.x - planeWidth/2, 0f, Hit.point.z - planeLength/2);

				Vector3 nContact = new Vector3 (contact.x / planeWidth, 0f, contact.z / planeLength);

				Vector2 textureContact = new Vector2 ((int)(nContact.x * size), (int)(nContact.z * size));

				//displacementMap.SetPixel ((int)textureContact.x, (int)textureContact.y, Color.black);
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
				//********************************************************************************************************************************************
			}
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			line.gameObject.SetActive (false);
		}
	}
}
