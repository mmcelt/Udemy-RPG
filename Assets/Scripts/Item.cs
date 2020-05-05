using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	#region Fields

	[Header("Item Type")]
	public bool _isItem;
	public bool _isWeapon;
	public bool _isArmor;
	[Header("Item Info")]
	public Sprite _itemSprite;
	public string _itemName, _itemDesc;
	public int _itemValue;
	[Header("Item Details")]
	public int _amountToChange;
	public bool _affectHP, _affectMP, _affectSTR, _affectDEF;
	public int _weaponStr, _armorStr;

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

	public void Use(int charToUseOn)
	{
		CharSats selectedChar = GameManager.Instance._playerStats[charToUseOn];

		if (_isItem)
		{
			if (_affectHP)
			{
				selectedChar._currentHP = Mathf.Min(selectedChar._currentHP + _amountToChange, selectedChar._maxHP);
			}
			if (_affectMP)
			{
				selectedChar._currentMP = Mathf.Min(selectedChar._currentMP + _amountToChange, selectedChar._maxMP);
			}
			if (_affectSTR)
			{
				selectedChar._strength += _amountToChange;
			}
			if (_affectDEF)
			{
				selectedChar._defense += _amountToChange;
			}
		}
		if (_isWeapon)
		{
			if (selectedChar._equippedWpn != "")
			{
				//put the equipped weapon back in inventory
				GameManager.Instance.AddItem(selectedChar._equippedWpn);

				selectedChar._equippedWpn = _itemName;
				selectedChar._weaponPwr = _weaponStr;
			}
			else
			{
				selectedChar._equippedWpn = _itemName;
				selectedChar._weaponPwr = _weaponStr;
			}
		}
		if (_isArmor)
		{
			if(selectedChar._equippedArm != "")
			{
				//put the equipped armor back in inventory
				GameManager.Instance.AddItem(selectedChar._equippedArm);

				selectedChar._equippedArm = _itemName;
				selectedChar._armorPwr = _armorStr;
			}
			else
			{
				selectedChar._equippedArm = _itemName;
				selectedChar._armorPwr = _armorStr;
			}
		}
		//remove the item from the inventory...
		GameManager.Instance.RemoveItem(_itemName);

		GameMenu.Instance._useButton.interactable = false;
		GameMenu.Instance._dropButton.interactable = false;
	}
	#endregion

	#region Private Methods


	#endregion
}
