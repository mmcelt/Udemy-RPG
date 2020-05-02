using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSats : MonoBehaviour
{
	#region Fields

	[SerializeField] Sprite _charImage;
	[SerializeField] string _charName;
	[SerializeField] int _charLevel = 1;
	[SerializeField] int _currentEXP;
	[SerializeField] int[] _expToNextLevel;
	[SerializeField] int _maxLevel = 100;
	[SerializeField] int _baseEXP = 1000;
	[SerializeField] int _currenHP;
	[SerializeField] int _maxHP = 100;
	[SerializeField] int _currentMP;
	[SerializeField] int _maxMP = 30;

	[SerializeField] float _nextLvlMultiplier = 1.03f;

	[SerializeField] int[] _mpLvlBonus;

	[SerializeField] int _strength;
	[SerializeField] int _defense;
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
		_currenHP = _maxHP;
		_currentMP = _maxMP;
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.K))
			AddExp(1000);
	}
	#endregion

	#region Public Methods

	public void AddExp(int amount)
	{
		_currentEXP += amount;

		if(_charLevel < _maxLevel)
		{
			if (_currentEXP > _expToNextLevel[_charLevel])
			{
				_currentEXP -= _expToNextLevel[_charLevel];
				_charLevel++;

				//determine whether to add to str or def based on even/odd
				if (_charLevel % 2 == 0)   //even
				{
					_strength++;
				}
				else  //odd
				{
					_defense++;
				}

				//determine maxHP increase on Level up
				_maxHP = Mathf.FloorToInt(_maxHP * _nextLvlMultiplier);
				_currenHP = _maxHP;

				//determine maxMP increase on level up
				_maxMP += _mpLvlBonus[_charLevel];
				_currentMP = _maxMP;
			}
		}
		
		if (_charLevel >= _maxLevel)
			_currentEXP = 0;

	}
	#endregion

	#region Private Methods

	void CalculateExpToNextLevels()
	{
		for(int i=2; i<_expToNextLevel.Length; i++)
		{
			_expToNextLevel[i] = Mathf.FloorToInt(_expToNextLevel[i - 1] * _nextLvlMultiplier);
		}
	}
	#endregion
}
