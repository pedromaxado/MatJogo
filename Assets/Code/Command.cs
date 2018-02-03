using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour 
{
	public static float PieceSizeChangeRatio = 0.1f;
	public static float PieceMaxSize = 1.5f;
	public static float PieceSpeed = 15.0f;
	public static float PieceMinDist = 5.0f;

	public UnityEngine.UI.Image p1;
	public UnityEngine.UI.Image p2;

	private int stage = 6;

	private Vector3 p01;
	private Vector3 p02;

	private Vector3 s01;
	private Vector3 s02;

	private Color c;

	private System.Action matchingControllerCallback;

	// Use this for initialization
	public void Setup (GameObject _p1, GameObject _p2, System.Action callback) 
	{
		matchingControllerCallback = callback;

		p1 = _p1.GetComponent<UnityEngine.UI.Image> ();
		p2 = _p2.GetComponent<UnityEngine.UI.Image> ();

		p01 = p1.transform.position;
		p02 = p2.transform.position;

		s01 = p1.rectTransform.localScale;
		s02 = p2.rectTransform.localScale;

		c = Color.white;

		stage = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (stage)
		{
			case 1:
				Stage1 ();
				break;
			case 2:
				Stage2 ();
				break;
			case 3:
				Stage3 ();
				break;
			case 4:
				Stage4 ();
				break;
			case 5:
				Stage5 ();
				break;
		}
	}


	private void Stage1()
	{
		if (p1.rectTransform.localScale.x < s01.x * Command.PieceMaxSize)
		{
			p1.rectTransform.localScale = p1.rectTransform.localScale * (1 + Command.PieceSizeChangeRatio);
			p2.rectTransform.localScale = p2.rectTransform.localScale * (1 + Command.PieceSizeChangeRatio);

			c.a = c.a * (1 - Command.PieceSizeChangeRatio);

			p1.color = c;
			p2.color = c;
		}
		else
		{
			stage = 2;
		}
	}

	private void Stage2()
	{
		p1.rectTransform.localScale = s01 * Command.PieceMaxSize;
		p2.rectTransform.localScale = s02 * Command.PieceMaxSize;
		stage = 3;
	}

	private void Stage3()
	{
		if (Vector3.Distance (p1.transform.position, p02) > Command.PieceMinDist)
		{
			p1.transform.position = Vector3.Lerp (p1.transform.position, p02, Time.deltaTime * Command.PieceSpeed);
			p2.transform.position = Vector3.Lerp (p2.transform.position, p01, Time.deltaTime * Command.PieceSpeed);
		}
		else
		{
			p1.transform.position = p02;
			p2.transform.position = p01;
			stage = 4;
		}
	}

	private void Stage4()
	{
		if (p1.rectTransform.localScale.x > s01.x)
		{
			p1.rectTransform.localScale = p1.rectTransform.localScale * (1 - Command.PieceSizeChangeRatio);
			p2.rectTransform.localScale = p2.rectTransform.localScale * (1 - Command.PieceSizeChangeRatio);

			c.a = c.a * (1 + Command.PieceSizeChangeRatio);

			p1.color = c;
			p2.color = c;
		}
		else
		{
			stage = 5;
		}
	}

	private void Stage5()
	{
		p1.rectTransform.localScale = s01;
		p2.rectTransform.localScale = s02;
		c.a = 1.0f;
		p1.color = c;
		p2.color = c;
		stage = 6;

		matchingControllerCallback ();
	}
}
