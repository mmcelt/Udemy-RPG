using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _player;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (!PlayerController.Instance)
		{
			Instantiate(_player);
		}
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
