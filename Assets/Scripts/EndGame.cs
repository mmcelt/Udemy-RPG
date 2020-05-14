using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{	
	#region Fields

	
	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		Invoke("EndOfGame", 2f);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void EndOfGame()
	{
		Debug.Log("END OF GAME...");
		PlayerController.Instance._canMove = false;
	}
	#endregion
}
