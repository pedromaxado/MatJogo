using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour 
{
	public static float PieceSizeChangeRatio = 0.01f;
	public static float PieceMaxSize = 1.5f;
	public static float PieceSpeed = 0.5f;

	public UnityEngine.UI.Image p1;
	public UnityEngine.UI.Image p2;

	private int stage;

	private Vector3 p01;
	private Vector3 p02;

	private Vector3 s01;
	private Vector3 s02;

	private Color c;

	// Use this for initialization
	void Start () 
	{
		stage = 1;

		p01 = p1.transform.position;
		p02 = p2.transform.position;

		s01 = p1.rectTransform.localScale;
		s02 = p2.rectTransform.localScale;

		c = Color.white;
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
			case 5:
				StageEnd ();
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

	private void StageEnd()
	{
		p1.rectTransform.localScale = s01;
		p2.rectTransform.localScale = s02;
		c.a = 1.0f;
		p1.color = c;
		p2.color = c;
	}
}
