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

	public void UseForBattle(int battler)
	{
		BattleChar selectedBattler = BattleManager.Instance._activeBattlers[battler];
		if (_isItem)
		{
			if (_affectHP)
			{
				selectedBattler._currentHP += _amountToChange;
				if (selectedBattler._currentHP > selectedBattler._maxHP)
					selectedBattler._currentHP = selectedBattler._maxHP;
			}

			if (_affectMP)
			{
				selectedBattler._currentMP += _amountToChange;
				if (selectedBattler._currentMP > selectedBattler._maxMP)
					selectedBattler._currentMP = selectedBattler._maxMP;
			}

			if (_affectSTR)
			{
				selectedBattler._STR += _amountToChange;  //this should be a small amount
			}
		}

		if (_isWeapon)
		{
			if (selectedBattler._equippedWpn != "") //character already has a weapon equipped
			{
				//return the existing equipped weapon to the inventory
				GameManager.Instance.AddItem(selectedBattler._equippedWpn);
			}
			selectedBattler._equippedWpn = _itemName;
			selectedBattler._wpnPwr = _weaponStr;
		}

		if (_isArmor)
		{
			if (selectedBattler._equippedArm != "") //character already has armor equipped
			{
				//return the existing equipped armor to the inventory
				GameManager.Instance.AddItem(selectedBattler._equippedArm);
			}
			selectedBattler._equippedArm = _itemName;
			selectedBattler._armPwr = _armorStr;
		}
		GameManager.Instance.RemoveItem(_itemName);   //remove the item from the inventory
	}

	#endregion

	#region Private Methods


	#endregion
}
