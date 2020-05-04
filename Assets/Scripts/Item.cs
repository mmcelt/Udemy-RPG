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
	public bool _affectHp, _affectMP, _affectStr, _affectDef;
	public int _weaponStr, _armorStrength;

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


	#endregion

	#region Private Methods


	#endregion
}
