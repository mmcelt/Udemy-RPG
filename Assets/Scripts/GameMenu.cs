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
	[SerializeField] GameObject[] _charStatHolders;
	[SerializeField] Text[] _nameTexts, _hpTexts, _mpTexts, _lvlTexts, _expTexts;
	[SerializeField] Slider[] _expSliders;
	[SerializeField] Image[] _charImages;
	[SerializeField] GameObject[] _windows;
	[Header("Player Status Panels")]
	[SerializeField] GameObject[] _statusButtons;
	[SerializeField] Image _statusImage;
	[SerializeField] Text _statusName, _statusHP, _statusMP, _statusStr, _statusDef, _statusWpnEqpd, _statusWpnPwr, _statusArmEqpd, _statusArmPwr, _statusExp;
	[Header("Item Panel")]
	[SerializeField] ItemButtton[] _itemButtons;
	//string _selectedItem;
	Item _activeItem;
	public Button _useButton, _dropButton;
	[SerializeField] Text _itemName, _itemDescription, _useButtonText;
	[SerializeField] GameObject _itemCharChoiceMenu;
	[SerializeField] Text[] _itemCharChoiceNames;
	[SerializeField] Text _goldText;

	[SerializeField] string _mainMenuName;

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
			if (_theMenu.activeInHierarchy)
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
		_playerStats = GameManager.Instance.playerStats;

		for(int i=0; i<_playerStats.Length; i++)
		{
			if (_playerStats[i].gameObject.activeInHierarchy)
			{
				_charStatHolders[i].SetActive(true);

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
				_charStatHolders[i].SetActive(false);
			}
		}

		_goldText.text = GameManager.Instance._currentGold + "g";
	}

	public void ToggleWindow(int windowIndex)
	{
		UpdateMainStats();

		for(int i=0; i<_windows.Length; i++)
		{
			if (i == windowIndex)
			{
				_windows[i].SetActive(!_windows[i].activeInHierarchy);
			}
			else
			{
				_windows[i].SetActive(false);
			}
		}
		_itemCharChoiceMenu.SetActive(false);
	}

	public void CloseMenu()
	{
		foreach (GameObject window in _windows)
			window.SetActive(false);

		_theMenu.SetActive(false);
		GameManager.Instance._gameMenuOpen = false;
		_itemCharChoiceMenu.SetActive(false);
	}

	public void OpenStatus()
	{
		UpdateMainStats();
		//update the player data...
		UpdateCharStatus(0);

		for (int i=0; i<_statusButtons.Length; i++)
		{
			_statusButtons[i].SetActive(_playerStats[i].gameObject.activeInHierarchy);
			_statusButtons[i].GetComponentInChildren<Text>().text = _playerStats[i]._charName;
		}
	}

	public void UpdateCharStatus(int selectedChar)
	{
		UpdateMainStats();

		_statusImage.sprite = _playerStats[selectedChar]._charImage;
		_statusName.text = _playerStats[selectedChar]._charName;
		_statusHP.text = _playerStats[selectedChar]._currentHP + "/" + _playerStats[selectedChar]._maxHP;
		_statusMP.text = _playerStats[selectedChar]._currentMP + "/" + _playerStats[selectedChar]._maxMP;
		_statusStr.text = _playerStats[selectedChar]._strength.ToString();
		_statusDef.text = _playerStats[selectedChar]._defense.ToString();

		if (_playerStats[selectedChar]._equippedWpn != "")
			_statusWpnEqpd.text = _playerStats[selectedChar]._equippedWpn;
		else
			_statusWpnEqpd.text = "None";

		_statusWpnPwr.text = _playerStats[selectedChar]._weaponPwr.ToString();

		if (_playerStats[selectedChar]._equippedArm != "")
			_statusArmEqpd.text = _playerStats[selectedChar]._equippedArm;
		else
			_statusArmEqpd.text = "None";

		_statusArmPwr.text = _playerStats[selectedChar]._armorPwr.ToString();
		_statusExp.text = (_playerStats[selectedChar]._expToNextLevel[_playerStats[selectedChar]._charLevel] - _playerStats[selectedChar]._currentEXP).ToString();
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

		_itemCharChoiceMenu.SetActive(true);

		for (int i = 0; i < _itemCharChoiceNames.Length; i++)
		{
			_itemCharChoiceNames[i].text = GameManager.Instance.playerStats[i]._charName;
			_itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.Instance.playerStats[i].gameObject.activeInHierarchy);
		}
	}

	public void CloseItemCharacterChoice()
	{
		_itemCharChoiceMenu.SetActive(false);
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
