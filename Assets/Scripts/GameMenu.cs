using System.Collections;
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
	[SerializeField] GameObject[] _charStatPanels;

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
			//toggle the menu panel...
			bool isOpen = _theMenu.activeSelf;
			isOpen = !isOpen;
			_theMenu.SetActive(isOpen);
			GameManager.Instance._gameMenuOpen = isOpen;

			if (isOpen)
				UpdateMainStats();
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
			}
			else
			{
				_charStatPanels[i].SetActive(false);
			}
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
