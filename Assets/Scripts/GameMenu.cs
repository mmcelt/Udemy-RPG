using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _theMenu;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (Input.GetButtonDown("Fire2"))
		{
			//toggle the menu panel...
			bool isOpen = _theMenu.activeSelf;
			isOpen = !isOpen;
			_theMenu.SetActive(isOpen);
			GameManager.Instance._gameMenuOpen = isOpen;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
