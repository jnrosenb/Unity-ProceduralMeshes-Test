using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	public intVector2 size = new intVector2 (20, 20);

	public MazeCell cellTemplate;
	private MazeCell[,] mazeGrid;

	public float cellCreationDelay = 0.01f;

	public intVector2 RandomCoordinates
	{
		get
		{
			return new intVector2((int)Random.Range(0, size.x), (int)Random.Range(0, size.z));
		}
	}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	//Metodo que se llama desde GameManager:
	public void generateMaze()
	{
		StartCoroutine (generateCorroutine());
	}

	//Corroutine only to see the generation process in slow motion.
	public IEnumerator generateCorroutine()
	{
		mazeGrid = new MazeCell[size.z, size.x];

		intVector2 randomCoords = RandomCoordinates;
		while (containsCoords (randomCoords) && ReferenceEquals(mazeGrid[randomCoords.z, randomCoords.x], null))
		{
			createCell (randomCoords);

			randomCoords += mazeDirections.RandomDirection.toIntVector2 ();

			yield return new WaitForSeconds (cellCreationDelay);
		}
	}

	//Metodo innecesario, pero por ahora lo usare:
	private bool containsCoords(intVector2 coord)
	{
		return (coord.x >= 0 && coord.x < size.x && coord.z >= 0 && coord.z < size.z);
	}

	//coords.x corresponde a la j (la columna), coords.z corresponde a la i (la fila):
	private void createCell(intVector2 coords)
	{
		MazeCell cell = GameObject.Instantiate (cellTemplate) as MazeCell;
		float cellSize = cell.GetComponentInChildren<MeshRenderer> ().bounds.size.x;
		cell.transform.SetParent (transform);
		cell.name = "Cell" + coords.z + coords.x;

		cell.transform.localPosition = new Vector3(coords.x * cellSize, 0f, coords.z * cellSize);
		cell.transform.position -= new Vector3(size.x  * cellSize * 0.5f - cellSize * 0.5f, 0f, size.z * cellSize * 0.5f - cellSize * 0.5f);

		mazeGrid [coords.z, coords.x] = cell;
	}
}
