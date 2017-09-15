using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour 
{
	public int boardRows = 7;
	public int boardColumns = 8;


	public float boardXOffset = 150.0f;
	public float boardYOffset = 150.0f;
	public float stepSize = 5.0f;
	public GameObject baseObject;
	public GameObject[] baseObjects;

	public float[] probabilities;

	GameObject[,] board;

	// Use this for initialization
	void Start () 
	{
		board = new GameObject[boardRows, boardColumns];

		for (int i = 0; i < boardRows; i++)
		{
			for (int j = 0; j < boardColumns; j++)
			{
				float p = Random.value;
				float acc = probabilities [0];
				int k = 0;
				while (p > acc) {
					k++;
					acc += probabilities [k];
				}


				board [i, j] = (GameObject)(GameObject.Instantiate (baseObjects[k], new Vector3 (j * stepSize + boardXOffset, i * stepSize + boardYOffset, 0.0f), Quaternion.identity));
				board [i, j].transform.SetParent (this.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
