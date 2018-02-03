using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingController : MonoBehaviour {

	BoardController boardController;
	Command command;

	// Use this for initialization
	void Start () 
	{
		boardController = this.GetComponent<BoardController> ();
		command = this.GetComponent<Command> ();
		TakeAction = SwapPieces;
	}

	public System.Action<GameObject, GameObject, PlayerAction> TakeAction;

	public void EndWait()
	{
		TakeAction = SwapPieces;
		boardController.EndSwap ();
	}

	public void SwapPieces(GameObject selectedPiece, GameObject otherPiece, PlayerAction action)
	{
		command.Setup (selectedPiece, otherPiece, EndWait);
		TakeAction = Static.BlankMethod;
	}
}

public enum PlayerAction
{
	MoveLeft,
	MoveRight,
	MoveUp, 	// HIGH STAKES
	MoveDown 	// Amador
}
