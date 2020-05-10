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
			BattleStart(new string[] { "Eyeball", "Spider" });
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
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
