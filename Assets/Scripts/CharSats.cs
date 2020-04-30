using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSats : MonoBehaviour
{
	#region Fields

	[SerializeField] string _charName;
	[SerializeField] Sprite _charImage;

	[SerializeField] int _charLevel = 1;
	[SerializeField] int _currentEXP;

	[SerializeField] int _currenHP;
	[SerializeField] int _maxHP = 100;
	[SerializeField] int _currentMP;
	[SerializeField] int _maxMP = 30;

	[SerializeField] int _strength;
	[SerializeField] int _defence;
	[SerializeField] int _weaponPwr;
	[SerializeField] int _armorPwr;
	[SerializeField] string _equippedWpn, _equippedArm;

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
