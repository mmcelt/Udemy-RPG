using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	#region Fields

	public static BattleManager Instance;

	[SerializeField] GameObject _battleScene;
	[SerializeField] Transform[] _playerPositions;
	[SerializeField] Transform[] _enemyPositions;
	[SerializeField] BattleChar[] _playerPrefabs;
	[SerializeField] BattleChar[] _enemyPrefabs;

	public List<BattleChar> _activeBattlers = new List<BattleChar>();

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
							Instantiate(player, _playerPositions[i]);
							_activeBattlers.Add(player);

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
							Instantiate(enemy, _enemyPositions[i]);
							_activeBattlers.Add(enemy);
						}
					}
				}
			}
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
