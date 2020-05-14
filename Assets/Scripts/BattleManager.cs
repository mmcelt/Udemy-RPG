using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
	#region Fields

	public static BattleManager Instance;
	[Header("General")]
	[SerializeField] GameObject _battleScene;
	[SerializeField] Transform[] _playerPositions;
	[SerializeField] Transform[] _enemyPositions;
	[SerializeField] BattleChar[] _playerPrefabs;
	[SerializeField] BattleChar[] _enemyPrefabs;
	[SerializeField] string _gameOverScene;

	public List<BattleChar> _activeBattlers = new List<BattleChar>();

	[Header("Turn System")]
	public int _currentTurn;
	public bool _turnWaiting;
	[SerializeField] GameObject _uiButtonsHolder;

	[Header("Battling")]
	[SerializeField] GameObject _enemyAttackFX;
	[SerializeField] float _enemyTurnDelay = 1f;
	public BattleMove[] _moveList;
	[SerializeField] DamageNumber _theDamageNumber;
	[SerializeField] int _chanceToFlee = 35;

	bool _canRetreat;
	bool _retreating;

	[Header("UI")]
	[SerializeField] Text[] _playerName, _playerHP, _playerMP;
	public GameObject _targetMenu, _magicMenu, _useItemMenu;
	[SerializeField] BattleTargetButton[] _targetButtons;
	[SerializeField] BattleMagicButton[] _magicButtons;
	public BattleNotification _battleNotice;
	[SerializeField] ItemButtton[] _itemButtons;
	[SerializeField] Text _itemNameText, _itemDescriptionText, _useButtonText;
	[SerializeField] GameObject _itemCharChoicePanel;
	[SerializeField] Text[] _itemCharSelectButtonNames;
	public Button _useButton;
	//Slider[] _enemyHealthbars;

	[Header("Rewards")]
	public int _rewardXP;
	public string[] _rewardItems;
	//TODO: ADD GOLD

	Item _activeItem;

	bool _battleActive;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			BattleStart(new string[] { "Eyeball" }, false);
		}

		if (_battleActive)
		{
			if (_turnWaiting)
			{
				if (_activeBattlers[_currentTurn]._isPlayer)
				{
					_uiButtonsHolder.SetActive(true);
				}
				else
				{
					_uiButtonsHolder.SetActive(false);
					//enemy should attack...
					StartCoroutine(EnemyMoveRoutine());
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			NextTurn();
		}

	}
	#endregion

	#region Public Methods

	public void BattleStart(string[] enemiesToSpawn, bool canRetreat)
	{
		if (!_battleActive)
		{
			_canRetreat = canRetreat;

			_battleActive = true;
			GameManager.Instance._battleActive = true;
			_activeBattlers.Clear();

			//move the BattleManager to the camera's position...
			transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

			_battleScene.SetActive(true);
			AudioManager.Instance.PlayMusic(0);

			for (int i = 0; i < _playerPositions.Length; i++)
			{
				if (GameManager.Instance._playerStats[i].gameObject.activeSelf)
				{
					foreach (BattleChar player in _playerPrefabs)
					{
						if (player._charName == GameManager.Instance._playerStats[i]._charName)
						{
							BattleChar newPlayer = Instantiate(player, _playerPositions[i].transform.position, Quaternion.identity);
							newPlayer.transform.SetParent(_playerPositions[i]);
							_activeBattlers.Add(newPlayer);

							CharSats thePlayer = GameManager.Instance._playerStats[i];

							_activeBattlers[i]._currentHP = thePlayer._currentHP;
							_activeBattlers[i]._maxHP = thePlayer._maxHP;
							_activeBattlers[i]._currentMP = thePlayer._currentMP;
							_activeBattlers[i]._maxMP = thePlayer._maxMP;
							_activeBattlers[i]._STR = thePlayer._strength;
							_activeBattlers[i]._DEF = thePlayer._defense;
							_activeBattlers[i]._wpnPwr = thePlayer._weaponPwr;
							_activeBattlers[i]._armPwr = thePlayer._armorPwr;
						}
					}
				}
			}
			//enemies
			for (int i = 0; i < enemiesToSpawn.Length; i++)
			{
				if (enemiesToSpawn[i] != "")
				{
					foreach (BattleChar enemy in _enemyPrefabs)
					{
						if (enemy._charName == enemiesToSpawn[i])
						{
							BattleChar newEnemy = Instantiate(enemy, _enemyPositions[i].transform.position, Quaternion.identity);
							newEnemy.transform.SetParent(_enemyPositions[i]);
							_activeBattlers.Add(newEnemy);
						}
					}
				}
			}
			_turnWaiting = true;
			_currentTurn = Random.Range(0, _activeBattlers.Count);
			UpdateUIStats();
		}
	}

	public void NextTurn()
	{
		_currentTurn++;
		if (_currentTurn >= _activeBattlers.Count)
			_currentTurn = 0;

		_turnWaiting = true;

		UpdateBattle();
		UpdateUIStats();
	}

	public IEnumerator EnemyMoveRoutine()
	{
		_turnWaiting = false;
		yield return new WaitForSeconds(_enemyTurnDelay);
		EnemyAttack();
		yield return new WaitForSeconds(_enemyTurnDelay);
		NextTurn();
	}

	public void EnemyAttack()
	{
		List<int> players = new List<int>();

		for (int i = 0; i < _activeBattlers.Count; i++)
		{
			//get the potential player targets...
			if (_activeBattlers[i]._isPlayer && _activeBattlers[i]._currentHP > 0)
			{
				players.Add(i);
			}
		}
		int selectedTarget = players[Random.Range(0, players.Count)];

		//_activeBattlers[selectedTarget]._currentHP -= 30;
		int selectedAttack = Random.Range(0, _activeBattlers[_currentTurn]._movesAvailable.Length);
		int movePower = 0;

		for (int i = 0; i < _moveList.Length; i++)
		{
			if (_moveList[i]._moveName == _activeBattlers[_currentTurn]._movesAvailable[selectedAttack])
			{
				Instantiate(_moveList[i]._thEffect, _activeBattlers[selectedTarget].transform.position, Quaternion.identity);
				movePower = _moveList[i]._movePower;
			}
		}
		Instantiate(_enemyAttackFX, _activeBattlers[_currentTurn].transform.position, Quaternion.identity);
		DealDamage(selectedTarget, movePower);
	}

	public void DealDamage(int target, int movePower)
	{
		float attackPower = _activeBattlers[_currentTurn]._STR + _activeBattlers[_currentTurn]._wpnPwr;
		float defensePower = _activeBattlers[target]._DEF + _activeBattlers[target]._armPwr;

		float calcDamage = (attackPower / defensePower) * movePower * Random.Range(.8f, 1.2f);
		int damageToGive = Mathf.RoundToInt(calcDamage);

		Debug.Log(_activeBattlers[_currentTurn]._charName + " is dealing " + calcDamage + "(" + damageToGive + ") damage to " + _activeBattlers[target]._charName);

		_activeBattlers[target]._currentHP -= damageToGive;

		Instantiate(_theDamageNumber, _activeBattlers[target].transform.position, Quaternion.identity).SetDamage(damageToGive);

		//if(_activeBattlers[target]._isPlayer)
		UpdateUIStats();
	}

	public void UpdateUIStats()
	{
		for (int i = 0; i < _playerName.Length; i++)
		{
			if (_activeBattlers.Count > i)
			{
				if (_activeBattlers[i]._isPlayer)
				{
					BattleChar playerData = _activeBattlers[i];

					_playerName[i].gameObject.SetActive(true);
					_playerName[i].text = playerData._charName;

					_playerHP[i].text = Mathf.Clamp(playerData._currentHP, 0, playerData._maxHP) + "/" + playerData._maxHP;
					_playerMP[i].text = Mathf.Clamp(playerData._currentMP, 0, playerData._maxMP) + "/" + playerData._maxMP;

					//update who is up indication
					if (i == _currentTurn)
					{
						_playerName[i].color = Color.green;
					}
					else
					{
						_playerName[i].color = Color.white;
					}
				}
				else
				{
					_playerName[i].gameObject.SetActive(false);
				}
			}
			else
			{
				_playerName[i].gameObject.SetActive(false);
			}
		}
	}

	public void PlayerAttack(string move, int target)
	{
		int movePower = 0;

		for (int i = 0; i < _moveList.Length; i++)
		{
			if (_moveList[i]._moveName == move)
			{
				Instantiate(_moveList[i]._thEffect, _activeBattlers[target].transform.position, Quaternion.identity);
				movePower = _moveList[i]._movePower;
			}
		}

		Instantiate(_enemyAttackFX, _activeBattlers[_currentTurn].transform.position, Quaternion.identity);

		DealDamage(target, movePower);
		_uiButtonsHolder.SetActive(false);
		_targetMenu.SetActive(false);
		NextTurn();
	}

	public void OpenTargetMenu(string moveName)
	{
		_targetMenu.SetActive(true);

		List<int> enemies = new List<int>();
		for (int i = 0; i < _activeBattlers.Count; i++)
		{
			//check active battlers for enemies...
			if (!_activeBattlers[i]._isPlayer)
			{
				//this will add the enemy's index in the active battlers list
				enemies.Add(i);
			}
		}

		for (int i = 0; i < _targetButtons.Length; i++)
		{
			if (enemies.Count > i && _activeBattlers[enemies[i]]._currentHP > 0)
			{
				//these are active enemies
				_targetButtons[i].gameObject.SetActive(true);
				_targetButtons[i]._moveName = moveName;
				_targetButtons[i]._activeBattlerTarget = enemies[i];
				_targetButtons[i]._targetName.text = _activeBattlers[enemies[i]]._charName;
			}
			else
			{
				//these are not active enemies
				_targetButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void OpenMagicMenu()
	{
		_magicMenu.SetActive(true);

		for (int i = 0; i < _magicButtons.Length; i++)
		{
			if (_activeBattlers[_currentTurn]._movesAvailable.Length > i)
			{
				_magicButtons[i].gameObject.SetActive(true);
				_magicButtons[i]._spellName = _activeBattlers[_currentTurn]._movesAvailable[i];
				_magicButtons[i]._nameText.text = _magicButtons[i]._spellName;
				foreach (BattleMove move in _moveList)
				{
					if (move._moveName == _magicButtons[i]._spellName)
					{
						_magicButtons[i]._spellCost = move._moveCost;
						_magicButtons[i]._costText.text = _magicButtons[i]._spellCost.ToString();
					}
				}
			}
			else
			{
				_magicButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void Retreat()
	{
		if (_canRetreat)
		{
			int retreatSucess = Random.Range(0, 100);

			if (retreatSucess <= _chanceToFlee)
			{
				//end battle
				//_battleActive = false;
				//_battleScene.SetActive(false);
				_retreating = true;
				StartCoroutine(EndBattleRoutine());
			}
			else
			{
				_battleNotice.Activate("You couldn't Escape!!");
				NextTurn();
			}
		}
		else
		{
			_battleNotice.Activate("You are not allowed to retreat from this battle!");
		}
	}

	public void OpenUseItemPanel()
	{
		_useItemMenu.SetActive(true);
		ShowItems();
	}

	public void SelectItem(Item newItem)
	{
		_activeItem = newItem;

		if (_activeItem._isItem)
			_useButtonText.text = "Use";

		if (_activeItem._isWeapon || _activeItem._isArmor)
			_useButtonText.text = "Equip";

		_itemNameText.text = _activeItem._itemName;
		_itemDescriptionText.text = _activeItem._itemDesc;
	}

	public void useItem(int selectChar)
	{
		_activeItem.UseForBattle(selectChar);
		_useItemMenu.SetActive(false);
		ShowItems();
		//UpdateBattleStats();
		UpdateUIStats();
		UpdateBattle();
		NextTurn();
		_activeItem = null;
	}

	public void OpenItemCharChoice()
	{
		if (_activeItem == null) return;

		_itemCharChoicePanel.SetActive(true);

		for (int i = 0; i < _itemCharSelectButtonNames.Length; i++)
		{
			if (_activeBattlers[i]._isPlayer)
			{
				_itemCharSelectButtonNames[i].text = _activeBattlers[i]._charName;

				_itemCharSelectButtonNames[i].transform.parent.gameObject.SetActive(true);
			}
			else
				_itemCharSelectButtonNames[i].transform.parent.gameObject.SetActive(false);
		}
	}
	#endregion

	#region Private Methods

	void UpdateBattle()
	{
		bool allEnemiesDead = true;
		bool allPlayersDead = true;

		for (int i = 0; i < _activeBattlers.Count; i++)
		{
			//Debug.Log(_activeBattlers[i]._charName + " " + _activeBattlers[i]._currentHP);

			if (_activeBattlers[i]._currentHP <= 0)
			{
				_activeBattlers[i]._currentHP = 0;
				//handle dead Battler...
				if (_activeBattlers[i]._isPlayer)
				{
					_activeBattlers[i]._theSprite.sprite = _activeBattlers[i]._deadSprite;
					_activeBattlers[i]._hasDied = true;
				}
				else
				{
					_activeBattlers[i].EnemyFade();
					//_activeBattlers.RemoveAt(i);
				}
			}
			else
			{
				if (_activeBattlers[i]._isPlayer)
				{
					allPlayersDead = false;
					_activeBattlers[i]._theSprite.sprite = _activeBattlers[i]._aliveSprite;
					_activeBattlers[i]._hasDied = false;
				}
				else
				{
					allEnemiesDead = false;
				}
			}
		}

		//Debug.Log("All Players: " + allPlayersDead);
		//Debug.Log("All Enemies: " + allEnemiesDead);

		if (allEnemiesDead || allPlayersDead)
		{
			//end battle...
			if (allEnemiesDead)
			{
				//end battle in victory..
				StartCoroutine(EndBattleRoutine());
			}
			else
			{
				//end battle in defeat...
				StartCoroutine(GameOverRoutine());
			}

			//UpdatePlayerStats();
			//_battleScene.SetActive(false);
			//_battleActive = false;
			//GameManager.Instance._battleActive = false;

		}
		else
		{
			while (_activeBattlers[_currentTurn]._currentHP == 0)
			{
				_currentTurn++;
				if (_currentTurn >= _activeBattlers.Count)
				{
					_currentTurn = 0;
				}
			}
		}
	}

	void ShowItems()
	{
		GameManager.Instance.SortItems();

		for (int i = 0; i < _itemButtons.Length; i++)
		{
			//assign the button values to each item button
			_itemButtons[i]._buttonValue = i;

			//cache the string of the item name for each iteration
			string selectedItemName = GameManager.Instance._itemsHeld[i];

			if (selectedItemName != "")
			{
				_itemButtons[i]._buttonImage.gameObject.SetActive(true);
				_itemButtons[i]._buttonImage.sprite = GameManager.Instance.GetItemDetails(selectedItemName)._itemSprite;
				_itemButtons[i]._amountText.text = GameManager.Instance._numberHeldOfItem[i].ToString();
			}
			else
			{
				_itemButtons[i]._buttonImage.gameObject.SetActive(false);
				_itemButtons[i]._amountText.text = "";
			}
		}
	}

	void UpdatePlayerStats()	//transfer the players stats to the GameManager
	{
		for (int i = 0; i < _activeBattlers.Count; i++)
		{
			if (_activeBattlers[i]._isPlayer)
			{
				for (int j = 0; j < GameManager.Instance._playerStats.Length; j++)
				{
					if (_activeBattlers[i]._charName == GameManager.Instance._playerStats[j]._charName)
					{
						GameManager.Instance._playerStats[j]._currentHP = _activeBattlers[i]._currentHP;
						GameManager.Instance._playerStats[j]._currentMP = _activeBattlers[i]._currentMP;
						GameManager.Instance._playerStats[j]._armorPwr = _activeBattlers[i]._armPwr;
						GameManager.Instance._playerStats[j]._equippedArm = _activeBattlers[i]._equippedArm;
						GameManager.Instance._playerStats[j]._weaponPwr = _activeBattlers[i]._wpnPwr;
						GameManager.Instance._playerStats[j]._equippedWpn = _activeBattlers[i]._equippedWpn;
						GameManager.Instance._playerStats[j]._isDead = _activeBattlers[i]._hasDied;
					}
				}
			}

			Destroy(_activeBattlers[i].gameObject);
		}
	}

	IEnumerator EndBattleRoutine()
	{
		_battleActive = false;
		_uiButtonsHolder.SetActive(false);
		_targetMenu.SetActive(false);
		_magicMenu.SetActive(false);
		_useItemMenu.SetActive(false);

		yield return new WaitForSeconds(0.5f);

		UIFade.Instance.FadeToBlack();

		yield return new WaitForSeconds(1.5f);

		UpdatePlayerStats();
		UIFade.Instance.FadeFromBlack();
		_battleScene.SetActive(false);
		_activeBattlers.Clear();
		_currentTurn = 0;

		if (_retreating)
		{
			GameManager.Instance._battleActive = false;
			_retreating = false;
		}
		else
		{
			BattleRewards.Instance.OpenRewardScreen(_rewardXP, _rewardItems);
		}

		AudioManager.Instance.PlayMusic(Camera.main.GetComponent<CameraController>()._musicToPlay);
	}

	IEnumerator GameOverRoutine()
	{
		_battleActive = false;
		_uiButtonsHolder.SetActive(false);
		_targetMenu.SetActive(false);
		_magicMenu.SetActive(false);
		_useItemMenu.SetActive(false);
		_battleScene.SetActive(false);
		_activeBattlers.Clear();
		_currentTurn = 0;
		GameManager.Instance._battleActive = false;
		UIFade.Instance.FadeToBlack();

		yield return new WaitForSeconds(1.5f);

		SceneManager.LoadScene(_gameOverScene);
	}
	#endregion
}
