using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _player;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (!PlayerController.Instance)
		{
			Instantiate(_player);
		}
	}

	void Start() 
	{

	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
