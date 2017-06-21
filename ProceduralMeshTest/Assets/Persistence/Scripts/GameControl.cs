using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public int health;
	public int experience;

	public static GameControl control;

	void Awake()
	{
		if (control == null) 
		{
			control = this;
			DontDestroyOnLoad (gameObject);
		} 
		else if (control != this) 
		{
			Destroy (gameObject);
		}
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 200, 30), "Health:     " + health);
		GUI.Label (new Rect (10, 30, 200, 30), "Experience: " + experience);
	}

	public void save()
	{
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream fileStream = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.OpenOrCreate);

		PlayerData playerData = new PlayerData ();
		playerData.health = health;
		playerData.experience = experience;

		formatter.Serialize (fileStream, playerData);

		fileStream.Close ();
	}

	public void load()
	{
		string path = Application.persistentDataPath + "/PlayerInfo.dat";
		FileStream fileStream;

		if (File.Exists (path)) 
		{
			BinaryFormatter formatter = new BinaryFormatter ();
			fileStream = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.OpenOrCreate);

			PlayerData playerData = (PlayerData)formatter.Deserialize(fileStream);
			this.health = playerData.health;
			this.experience = playerData.experience;

			fileStream.Close ();
		}
	}
}

[Serializable]
public class PlayerData
{
	public int health;
	public int experience;
}