using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			BattleStart(new string[] { "Eyeball", "Spider", "Skeleton" });
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

	public void BattleStart(string[] enemiesToSpawn)
	{
		if (!_battleActive)
		{
			_battleActive = true;
			GameManager.Instance._battleActive = true;
			//move the BattleManager to the camera's position...
			transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

			_battleScene.SetActive(true);
			AudioManager.Instance.PlayMusic(0);

			for(int i=0; i<_playerPositions.Length; i++)
			{
				if (GameManager.Instance._playerStats[i].gameObject.activeSelf)
				{
					foreach(BattleChar player in _playerPrefabs)
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
			for(int i=0; i<enemiesToSpawn.Length; i++)
			{
				if(enemiesToSpawn[i] != "")
				{
					foreach(BattleChar enemy in _enemyPrefabs)
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
		}
	}

	public void NextTurn()
	{
		_currentTurn++;
		if (_currentTurn >= _activeBattlers.Count)
			_currentTurn = 0;

		_turnWaiting = true;

		UpdateBattle();
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

		for(int i=0; i<_activeBattlers.Count; i++)
		{
			//get the potential player targets...
			if(_activeBattlers[i]._isPlayer && _activeBattlers[i]._currentHP > 0)
			{
				players.Add(i);
			}
		}
		int selectedTarget = players[Random.Range(0, players.Count)];

		//_activeBattlers[selectedTarget]._currentHP -= 30;
		int selectedAttack = Random.Range(0, _activeBattlers[_currentTurn]._movesAvailable.Length);
		int movePower = 0;

		for(int i=0; i<_moveList.Length; i++)
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
	}
	#endregion

	#region Private Methods

	void UpdateBattle()
	{
		bool allEnemiesDead = true;
		bool allPlayersDead = true;

		for (int i=0; i<_activeBattlers.Count; i++)
		{
			//Debug.Log(_activeBattlers[i]._charName + " " + _activeBattlers[i]._currentHP);

			if (_activeBattlers[i]._currentHP < 0)
			{
				_activeBattlers[i]._currentHP = 0;
			}

			if(_activeBattlers[i]._currentHP == 0)
			{
				//handle dead Battler...

			}
			else
			{
				if (_activeBattlers[i]._isPlayer)
				{
					allPlayersDead = false;
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
			}
			else
			{
				//end battle in defeat...
			}

			_battleScene.SetActive(false);
			_battleActive = false;
			GameManager.Instance._battleActive = false;

		}
	}
	#endregion
}
