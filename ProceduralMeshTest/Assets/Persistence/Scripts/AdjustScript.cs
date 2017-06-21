using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour {

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 100, 100, 30), "Health Up"))
		{
			GameControl.control.health += 10;
		}
		if (GUI.Button(new Rect(20, 140, 100, 30), "Health Down"))
		{
			GameControl.control.health -= 10;
		}
		if (GUI.Button(new Rect(20, 180, 100, 30), "Experience Up"))
		{
			GameControl.control.experience += 10;
		}
		if (GUI.Button(new Rect(20, 220, 100, 30), "Experience Down"))
		{
			GameControl.control.experience -= 10;
		}


		if (GUI.Button(new Rect(20, 300, 100, 30), "SAVE"))
		{
			GameControl.control.save ();
		}
		if (GUI.Button(new Rect(20, 340, 100, 30), "LOAD"))
		{
			GameControl.control.load ();
		}
	}
}
