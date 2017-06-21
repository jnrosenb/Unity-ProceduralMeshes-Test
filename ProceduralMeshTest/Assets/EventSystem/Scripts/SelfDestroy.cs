using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

	private AudioSource source;
	public float shake;

	//Explosion animation as a gameObject*

	// Use this for initialization
	void Awake () 
	{
		source = gameObject.GetComponent<AudioSource>();
	}
	
	void onEnable()
	{
		EventManager.StartListening ("Destroy", Destroy);
	}

	void OnDisable()
	{
		EventManager.StopListening ("Destroy", Destroy);
	}

	void Destroy()
	{
		EventManager.StopListening ("Destroy", Destroy);

		//StartCoroutine (ShakeExplosion);
	}

//	IEnumerator ShakeExplosion()
//	{
//		yield return new WaitForSeconds ((float)Random.Range(0f, shake));
//
//		source.pitch = Random.Range (0.1f, 0.5f);
//		source.Play ();
//
//		float startTime = 0;
//		float endTime = Random.Range (0.1f, 0.5f);
//
//		while (startTime <= endTime) 
//		{
//			transform.Translate (new Vector3 (Random.Range(0.01f, 0.05f), 0f, Random.Range(0.01f, 0.05f)));
//			transform.Rotate (Quaternion.Euler (new Vector3 (Random.Range(0.01f, 0.05f), 0f, Random.Range(0.01f, 0.05f))));
//
//			startTime += shake;
//
//			yield return null;
//		}
//
//		//Explosion animation
//		Destroy(gameObject);
//	}
}
