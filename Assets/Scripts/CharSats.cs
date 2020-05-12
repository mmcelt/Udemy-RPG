using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSats : MonoBehaviour
{
	#region Fields

	public Sprite _charImage;
	public string _charName;
	public int _charLevel = 1;
	public int _currentEXP;
	public int[] _expToNextLevel;
	public int _maxLevel = 100;
	public int _baseEXP = 1000;
	public int _currentHP;
	public int _maxHP = 100;
	public int _currentMP;
	public int _maxMP = 30;

	public float _nextLvlMultiplier = 1.03f;

	public int[] _mpLvlBonus;

	public int _strength;
	public int _defense;
	public int _weaponPwr;
	public int _armorPwr;
	public string _equippedWpn, _equippedArm;
	public bool _isDead;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_expToNextLevel = new int[_maxLevel];
		_expToNextLevel[1] = _baseEXP;

		CalculateExpToNextLevels();
		//set initial HP/MP to max
		_currentHP = _maxHP;
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
				_currentHP = _maxHP;

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
