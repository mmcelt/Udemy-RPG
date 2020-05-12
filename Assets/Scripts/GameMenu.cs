using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	#region Fields

	public static GameMenu Instance;

	public GameObject _theMenu;
	[Header("Character Status Panel")]
	[SerializeField] GameObject[] _charStatPanels;
	[SerializeField] Text[] _nameTexts, _hpTexts, _mpTexts, _lvlTexts, _expTexts;
	[SerializeField] Slider[] _expSliders;
	[SerializeField] Image[] _charImages;
	[SerializeField] GameObject[] _windows;
	[Header("Player Status Panels")]
	[SerializeField] GameObject[] _statusButtons;
	[SerializeField] Image _playerImage;
	[SerializeField] Text _nameText, _hpText, _mpText, _strText, _defText, _wpnText, _wpnPwrText, _armText, _armPwrText, _expText;
	[Header("Item Panel")]
	[SerializeField] ItemButtton[] _itemButtons;
	string _selectedItem;
	Item _activeItem;
	public Button _useButton, _dropButton;
	[SerializeField] Text _itemName, _itemDescription, _useButtonText;
	[SerializeField] GameObject _selectCharacterPamel;
	[SerializeField] Text[] _selectCharacterButtonTexts;
	[SerializeField] Text _goldAmountText;

	CharSats[] _playerStats;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	void Start() 
	{
		//UpdateMainStats();
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

			AudioManager.Instance.PlaySFX(5);
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

		_goldAmountText.text = GameManager.Instance._currentGold + "g";
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
		_selectCharacterPamel.SetActive(false);
	}

	public void CloseMenu()
	{
		foreach (GameObject window in _windows)
			window.SetActive(false);

		_theMenu.SetActive(false);
		GameManager.Instance._gameMenuOpen = false;
		_selectCharacterPamel.SetActive(false);
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
		UpdateCharStatus(0);
	}

	public void UpdateCharStatus(int selectedChar)
	{
		UpdateMainStats();

		_playerImage.sprite = _playerStats[selectedChar]._charImage;
		_nameText.text = _playerStats[selectedChar]._charName;
		_hpText.text = _playerStats[selectedChar]._currentHP + "/" + _playerStats[selectedChar]._maxHP;
		_mpText.text = _playerStats[selectedChar]._currentMP + "/" + _playerStats[selectedChar]._maxMP;
		_strText.text = _playerStats[selectedChar]._strength.ToString();
		_defText.text = _playerStats[selectedChar]._defense.ToString();

		if (_playerStats[selectedChar]._equippedWpn != "")
			_wpnText.text = _playerStats[selectedChar]._equippedWpn;
		else
			_wpnText.text = "None";

		_wpnPwrText.text = _playerStats[selectedChar]._weaponPwr.ToString();

		if (_playerStats[selectedChar]._equippedArm != "")
			_armText.text = _playerStats[selectedChar]._equippedArm;
		else
			_armText.text = "None";

		_armPwrText.text = _playerStats[selectedChar]._armorPwr.ToString();
		_expText.text = (_playerStats[selectedChar]._expToNextLevel[_playerStats[selectedChar]._charLevel] - _playerStats[selectedChar]._currentEXP).ToString();
	}

	public void ShowItems()
	{
		GameManager.Instance.SortItems();

		for(int i=0; i<_itemButtons.Length; i++)
		{
			_itemButtons[i]._buttonValue = i;

			if(GameManager.Instance._itemsHeld[i] != "")
			{
				_itemButtons[i]._buttonImage.gameObject.SetActive(true);
				_itemButtons[i]._buttonImage.sprite = GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[i])._itemSprite;
				_itemButtons[i]._amountText.text = GameManager.Instance._numberHeldOfItem[i].ToString();
			}
			else
			{
				_itemButtons[i]._buttonImage.gameObject.SetActive(false);
				_itemButtons[i]._amountText.text = "";
			}
		}
	}

	public void SelectItem(Item theItem)
	{
		_activeItem = theItem;

		_itemName.text = _activeItem._itemName;
		_itemDescription.text = _activeItem._itemDesc;

		if (_activeItem._isItem)
		{
			_useButtonText.text = "Use";
		}
		if(_activeItem._isArmor || _activeItem._isWeapon)
		{
			_useButtonText.text = "Equip";
		}
	}

	public void UseItem(int selectedChar)
	{
		_activeItem.Use(selectedChar);
		CloseItemCharacterChoice();
	}

	public void OnDropButtonClicked()
	{
		if(_activeItem != null)
			GameManager.Instance.RemoveItem(_activeItem._itemName);
	}

	public void OpenItemCharacterChoice()
	{
		if (_activeItem == null) return;

		_selectCharacterPamel.SetActive(true);

		for (int i = 0; i < _selectCharacterButtonTexts.Length; i++)
		{
			_selectCharacterButtonTexts[i].text = GameManager.Instance._playerStats[i]._charName;
			_selectCharacterButtonTexts[i].transform.parent.gameObject.SetActive(GameManager.Instance._playerStats[i].gameObject.activeSelf);
		}
	}

	public void CloseItemCharacterChoice()
	{
		_selectCharacterPamel.SetActive(false);
	}

	public void SaveGame()
	{
		GameManager.Instance.SaveData();
		QuestManager.Instance.SaveQuestData();
	}

	public void QuitGame()
	{
		SceneManager.LoadScene("MainMenu");
		Destroy(GameManager.Instance.gameObject);
		Destroy(FindObjectOfType<CameraController>().gameObject);
		Destroy(PlayerController.Instance.gameObject);
		Destroy(AudioManager.Instance.gameObject);
		Destroy(gameObject);
	}

	public void PlayButtonSound()
	{
		AudioManager.Instance.PlaySFX(4);
	}
#endregion

	#region Private Methods


	#endregion
}
