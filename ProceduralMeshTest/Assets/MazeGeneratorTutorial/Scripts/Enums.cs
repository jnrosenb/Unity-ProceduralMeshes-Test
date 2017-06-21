using UnityEngine;

//Enum for directions:
public enum Directions
{
	north,
	south,
	east,
	west,
};

public static class mazeDirections
{
	public static int numberDirections = 4;

	public static Directions RandomDirection
	{
		get
		{
			return (Directions) Random.Range (0, numberDirections);
		}
	}

	public static intVector2[] dirCoords = new intVector2[]
	{
		new intVector2(0, 1),
		new intVector2(0, -1),
		new intVector2(1, 0),
		new intVector2(-1, 0),
	};

	public static intVector2 toIntVector2(this Directions dir)
	{
		return dirCoords[(int)dir];
	}
}
