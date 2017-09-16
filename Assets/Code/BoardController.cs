using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour 
{
	public int boardRows = 7;
	public int boardColumns = 8;
	public float firstRowPosition = 100.0f;

	public int selectorX = 0;
	public int selectorY = 0;
	System.Action selectorAction;


	public float boardXOffset = 150.0f;
	public float boardYOffset = 150.0f;
	public float stepSize = 5.0f;
	public GameObject selectorObject;
	public GameObject[] baseObjects;

	public float[] probabilities;

	GameObject[,] board;

	// Use this for initialization
	void Start () 
	{
		selectorAction = MoveSelector;
		board = new GameObject[boardRows, boardColumns];

		for (int i = 0; i < boardRows; i++)
		{
			for (int j = 0; j < boardColumns; j++)
			{
				float p = Random.value;
				float acc = probabilities [0];
				int k = 0;
				while (p > acc)
				{
					k++;
					acc += probabilities [k];
				}
					
				board [i, j] = (GameObject)(GameObject.Instantiate (baseObjects[k], new Vector3 (j * stepSize + boardXOffset, firstRowPosition - i * stepSize + boardYOffset, 0.0f), Quaternion.identity));
				board [i, j].transform.SetParent (this.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		selectorAction ();
		GetInput ();
		selectorAction = MoveSelector;
	}

	#region Input

	private void GetInput()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			selectorX = (selectorX > 0) ? --selectorX : selectorX;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			selectorX = (selectorX < boardColumns - 1) ? ++selectorX : selectorX;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			selectorY = (selectorY > 0) ? --selectorY : selectorY;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			selectorY = (selectorY < boardRows - 1) ? ++selectorY : selectorY;
		}
	}

	#endregion

	#region SelectorMovement

	private void SelectorStopped()
	{
	}

	private void MoveSelector()
	{
		GameObject go = board [selectorY, selectorX];
		if (Vector3.Distance (selectorObject.transform.position, go.transform.position) > 0.1f)
		{
			selectorObject.transform.position = Vector3.Lerp (selectorObject.transform.position, go.transform.position, 15.0f * Time.deltaTime);
		}
		else
		{
			selectorObject.transform.position = go.transform.position;
			selectorAction = SelectorStopped;
		}
	}

	#endregion
}
