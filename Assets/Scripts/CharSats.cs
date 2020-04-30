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
	[SerializeField] int[] _expToNextLevel;
	[SerializeField] int _maxLevel = 100;
	[SerializeField] int _baseEXP = 1000;
	[SerializeField] float _expToNextLvlMultiplier = 1.03f;

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
		_expToNextLevel = new int[_maxLevel];
		_expToNextLevel[1] = _baseEXP;

		CalculateExpToNextLevels();
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void CalculateExpToNextLevels()
	{
		for(int i=2; i<_expToNextLevel.Length; i++)
		{
			_expToNextLevel[i] = Mathf.FloorToInt(_expToNextLevel[i - 1] * _expToNextLvlMultiplier);
		}
	}
	#endregion
}
