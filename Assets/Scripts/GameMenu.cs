﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _theMenu;
	[SerializeField] Text[] _nameTexts, _hpTexts, _mpTexts, _lvlTexts, _expTexts;
	[SerializeField] Slider[] _expSliders;
	[SerializeField] Image[] _charImages;
	[SerializeField] GameObject[] _charStatPanels, _windows;
	[SerializeField] GameObject[] _statusButtons;

	CharSats[] _playerStats;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (Input.GetButtonDown("Fire2"))
		{
			if (_theMenu.activeSelf)
			{
				//_theMenu.SetActive(false);
				//GameManager.Instance._gameMenuOpen = false;
				CloseMenu();
			}
			else
			{
				_theMenu.SetActive(true);
				UpdateMainStats();
				GameManager.Instance._gameMenuOpen = true;
			}
		}
	}
	#endregion

	#region Public Methods

	public void UpdateMainStats()
	{
		_playerStats = GameManager.Instance._playerStats;

		for(int i=0; i<_playerStats.Length; i++)
		{
			if (_playerStats[i].gameObject.activeSelf)
			{
				_charStatPanels[i].SetActive(true);

				_nameTexts[i].text = _playerStats[i]._charName;
				_hpTexts[i].text = "HP: " + _playerStats[i]._currentHP + "/" + _playerStats[i]._maxHP;
				_mpTexts[i].text = "MP: " + _playerStats[i]._currentMP + "/" + _playerStats[i]._maxMP;
				_lvlTexts[i].text = "Level: " + _playerStats[i]._charLevel;
				_expTexts[i].text = _playerStats[i]._currentEXP + "/" + _playerStats[i]._expToNextLevel[_playerStats[i]._charLevel];
				_expSliders[i].maxValue = _playerStats[i]._expToNextLevel[_playerStats[i]._charLevel];
				_expSliders[i].value = _playerStats[i]._currentEXP;
				_charImages[i].sprite = _playerStats[i]._charImage;
			}
			else
			{
				_charStatPanels[i].SetActive(false);
			}
		}
	}

	public void ToggleWindow(int windowIndex)
	{
		UpdateMainStats();

		for(int i=0; i<_windows.Length; i++)
		{
			if (i == windowIndex)
			{
				_windows[i].SetActive(!_windows[i].activeSelf);
			}
			else
			{
				_windows[i].SetActive(false);
			}
		}
	}

	public void CloseMenu()
	{
		foreach (GameObject window in _windows)
			window.SetActive(false);

		_theMenu.SetActive(false);
		GameManager.Instance._gameMenuOpen = false;
	}

	public void OpenStatus()
	{
		UpdateMainStats();

		for(int i=0; i<_statusButtons.Length; i++)
		{
			_statusButtons[i].SetActive(_playerStats[i].gameObject.activeSelf);
			_statusButtons[i].GetComponentInChildren<Text>().text = _playerStats[i]._charName;
		}

		//update the player data...

	}
	#endregion

	#region Private Methods


	#endregion
}
