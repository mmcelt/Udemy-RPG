﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtton : MonoBehaviour
{
	#region Fields

	public Image _buttonImage;
	public Text _amountText;
	public int _buttonValue;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void OnItemButtonClicked()
	{
		if(GameManager.Instance._itemsHeld[_buttonValue] != "")
		{
			GameMenu.Instance.SelectItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));
			//I ADDED THIS TO USE THE _SELECTEDiTEM FIELD IN GAMEMENU...
			//GameMenu.Instance._selectedItem = GameManager.Instance._itemsHeld[_buttonValue];
			GameMenu.Instance._useButton.interactable = true;
			GameMenu.Instance._dropButton.interactable = true;
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
